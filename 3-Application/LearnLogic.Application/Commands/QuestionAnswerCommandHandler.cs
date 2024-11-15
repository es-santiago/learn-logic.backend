using AutoMapper;
using LearnLogic.Domain.Interfaces.Repository;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Mediator;
using NetDevPack.Messaging;
using LearnLogic.Domain.Interfaces.Application;

namespace LearnLogic.Application.Commands
{
    public class QuestionAnswerCommandHandler : CommandHandler,
         IRequestHandler<RegisterNewQuestionAnswerCommand, ValidationResult>,
         IRequestHandler<UpdateQuestionAnswerCommand, ValidationResult>,
         IRequestHandler<ChangeStatusQuestionAnswerCommand, ValidationResult>,
         IRequestHandler<DeleteQuestionAnswerCommand, ValidationResult>
    {
        private readonly IQuestionRepository _repository;
        private readonly IMediatorHandler _mediator;
        private readonly IMapper _mapper;
        private readonly IUserClaimsAccessor _userClaimsAccessor;

        public QuestionAnswerCommandHandler(IMediatorHandler mediator, IUserClaimsAccessor userClaimsAccessor, IQuestionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
            _userClaimsAccessor = userClaimsAccessor;
        }

        public async Task<ValidationResult> Handle(RegisterNewQuestionAnswerCommand message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ValidationResult> Handle(UpdateQuestionAnswerCommand message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ValidationResult> Handle(ChangeStatusQuestionAnswerCommand message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ValidationResult> Handle(DeleteQuestionAnswerCommand message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
