using AutoMapper;
using FluentValidation.Results;
using LearnLogic.Application.Commands;
using LearnLogic.Domain.DTO;
using LearnLogic.Domain.Enum;
using LearnLogic.Domain.Exceptions;
using LearnLogic.Domain.Interfaces.Application;
using LearnLogic.Domain.Interfaces.Repository;
using LearnLogic.Domain.ViewModels;
using NetDevPack.Mediator;

namespace LearnLogic.Application.Services
{
    public class QuestionApplication : BaseApplication<QuestionViewModel, QuestionDTO, IQuestionRepository, RegisterNewQuestionCommand, UpdateQuestionCommand, ChangeStatusQuestionCommand>, IQuestionApplication
    {
        public readonly IUserClaimsAccessor _userClaimsAcessor;
        public readonly IUserRepository _userRepository;

        public QuestionApplication(IUserRepository userRepository, IQuestionRepository repository, IUserClaimsAccessor userClaimsAcessor, IMapper mapper, IMediatorHandler mediator) : base(repository, mapper, mediator)
        {
            _userClaimsAcessor = userClaimsAcessor;
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> UpdateStatus(QuestionViewModel vm)
        {
            var cmd = _mapper.Map<UpdateStatusQuestionCommand>(vm);
            return await _mediator.SendCommand(cmd);
        }

        public override QuestionDTO Get(Guid id)
        {
            var db = _repository.Get(id);
            var items = _repository.GetItemByQuestion(id).ToList();
            db.SolutionIdentifier = Guid.Empty;
            db.Items = items;
            return db;
        }

        public QuestionDTO SearchWithAnswer(Guid id)
        {
            var db = _repository.Get(id);
            db.Items = _repository.GetItemByQuestion(id).ToList();
            return db;
        }

        public override IQueryable<QuestionDTO> List()
        {
            var list = _repository.List().ToList();
            if (_userClaimsAcessor.Equals((int)UserType.Administrator))
                return list.AsQueryable();

            var answer = _repository.ListByUser(_userClaimsAcessor.UserId).ToList().Select(x => x.QuestionId).ToArray();
            return list.Where(x => !answer.Contains(x.Id)).AsQueryable();
        }

        public IQueryable<QuestionDTO> QuestionsResolved()
        {
            var list = _repository.List().ToList();
            var answer = ListByUser().ToList().Select(x => x.QuestionId).ToArray();
            return list.Where(x => answer.Contains(x.Id)).AsQueryable();
        }

        public IQueryable<QuestionAnswerDTO> ListByUser()
            => _repository.ListByUser(_userClaimsAcessor.UserId);


        public async Task<object> RegisterResponse(QuestionAnswerViewModel vm)
        {
            var question = _repository.Get(vm.QuestionId);

            if (question == null) throw new CustomException("Questão não encontrada!");

            try
            {
                var obj = _mapper.Map<QuestionAnswerDTO>(vm);
                obj.UserId = _userClaimsAcessor.UserId;
                obj.Correct = question.SolutionIdentifier == vm.SelectedItemId;

                var user = _userRepository.Get(obj.UserId);
                user.Punctuation += question.Points;
                _userRepository.Save(user);

                await _repository.AnswerSaveAsync(obj);
                return new { correct = obj.Correct, itemCorrect = question.SolutionIdentifier };
            }
            catch (Exception ex)
            {
                throw new CustomException("Erro ao salvar resposta!");
            }
        }

        public async Task<ValidationResult> Comment(QuestionAnswerViewModel vm)
        {
            var cmd = _mapper.Map<UpdateStatusQuestionCommand>(vm);
            return await _mediator.SendCommand(cmd);
        }
    }
}
