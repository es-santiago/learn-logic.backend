using AutoMapper;
using LearnLogic.Domain.Interfaces.Repository;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Mediator;
using NetDevPack.Messaging;
using LearnLogic.Domain.DTO;
using LearnLogic.Domain.Interfaces.Application;

namespace LearnLogic.Application.Commands
{
    public class QuestionCommandHandler : CommandHandler,
         IRequestHandler<RegisterNewQuestionCommand, ValidationResult>,
         IRequestHandler<UpdateQuestionCommand, ValidationResult>,
         IRequestHandler<ChangeStatusQuestionCommand, ValidationResult>,
         IRequestHandler<UpdateStatusQuestionCommand, ValidationResult>
    {
        private readonly IQuestionRepository _repository;
        private readonly IMediatorHandler _mediator;
        private readonly IMapper _mapper;
        private readonly IUserClaimsAccessor _userClaimsAccessor;

        public QuestionCommandHandler(IMediatorHandler mediator, IQuestionRepository repository, IMapper mapper, IUserClaimsAccessor userClaimsAccessor)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._mediator = mediator;
            this._userClaimsAccessor = userClaimsAccessor;
        }

        public async Task<ValidationResult> Handle(RegisterNewQuestionCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var obj = _mapper.Map<QuestionDTO>(message.Obj);

            foreach (var item in obj.Items)
            {
                item.Id = Guid.NewGuid();
            }

            var isCorrect = obj.Items.First(x => x.IsCorrect);
            obj.SolutionIdentifier = isCorrect.Id;

            obj.UserId = _userClaimsAccessor.UserId;
            obj.Number = (_repository.Count() + 1).ToString();

            await _repository.SaveAsync(obj);
            return new ValidationResult();
        }

        public async Task<ValidationResult> Handle(UpdateQuestionCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var db = await _repository.GetAsync(message.Obj.Id);
            if (db == null)
            {
                AddError("Questão não encontrada!");
                return ValidationResult;
            }

            var obj = _mapper.Map<QuestionDTO>(message.Obj);

            var isCorrect = obj.Items.First(x => x.IsCorrect);
            obj.SolutionIdentifier = isCorrect.Id;
            obj.UserId = db.UserId;
            obj.Number = db.Number;
            obj.CreationDate = db.CreationDate;
            obj.Activated = db.Activated;
            obj.UpdateDate = DateTime.Now;

            await _repository.SaveAsync(obj);
            return new ValidationResult();
        }

        public async Task<ValidationResult> Handle(ChangeStatusQuestionCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var user = await _repository.GetAsync(message.Id);
            if (user == null)
            {
                AddError("User not found.");
                return ValidationResult;
            }
            user.Activated = !user.Activated;

            await _repository.SaveAsync(user);
            return new ValidationResult();
        }

        public async Task<ValidationResult> Handle(UpdateStatusQuestionCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var db = await _repository.GetAsync(message.Obj.Id);
            if (db == null)
            {
                AddError("Questão não encontrada!");
                return ValidationResult;
            }

            db.Status = message.Obj.Status;


            await _repository.SaveAsync(db);
            return new ValidationResult();
        }
    }
}
