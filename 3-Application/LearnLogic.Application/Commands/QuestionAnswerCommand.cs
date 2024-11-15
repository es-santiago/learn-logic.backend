using LearnLogic.Domain.ViewModels;
using NetDevPack.Messaging;

namespace LearnLogic.Application.Commands
{
    public abstract class QuestionAnswerCommand : Command
    {
        public QuestionAnswerViewModel Obj { get; protected set; }
    }

    public class RegisterNewQuestionAnswerCommand : QuestionAnswerCommand
    {
        public RegisterNewQuestionAnswerCommand(QuestionAnswerViewModel QuestionAnswerCommands) => Obj = QuestionAnswerCommands;

        public override bool IsValid() =>
            (ValidationResult = new RegisterNewQuestionAnswerCommandValidation().Validate(this)).IsValid;
    }

    public class UpdateQuestionAnswerCommand : QuestionAnswerCommand
    {
        public UpdateQuestionAnswerCommand(QuestionAnswerViewModel QuestionAnswerCommands) => Obj = QuestionAnswerCommands;

        public override bool IsValid() =>
            (ValidationResult = new UpdateQuestionAnswerCommandValidation().Validate(this)).IsValid;
    }

    public class ChangeStatusQuestionAnswerCommand : QuestionAnswerCommand
    {
        public Guid Id { get; protected set; }
        public ChangeStatusQuestionAnswerCommand(Guid id) => Id = id;

        public override bool IsValid() => Id != Guid.Empty;
    }

    public class DeleteQuestionAnswerCommand : QuestionAnswerCommand
    {
        public Guid Id { get; protected set; }
        public DeleteQuestionAnswerCommand(Guid id) => Id = id;

        public override bool IsValid() => Id != Guid.Empty;
    }
}
