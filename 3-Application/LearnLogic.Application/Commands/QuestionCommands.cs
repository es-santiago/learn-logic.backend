using LearnLogic.Domain.ViewModels;
using FluentValidation.Results;
using NetDevPack.Messaging;

namespace LearnLogic.Application.Commands
{

    public abstract class QuestionCommand : Command
    {
        public QuestionViewModel Obj { get; protected set; }
    }

    public class RegisterNewQuestionCommand : QuestionCommand
    {
        public RegisterNewQuestionCommand(QuestionViewModel QuestionCommands)
        {
            this.Obj = QuestionCommands;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewQuestionCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class UpdateQuestionCommand : QuestionCommand
    {
        public UpdateQuestionCommand(QuestionViewModel QuestionCommands)
        {
            this.Obj = QuestionCommands;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateQuestionCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ChangeStatusQuestionCommand : QuestionCommand
    {
        public Guid Id { get; protected set; }
        public ChangeStatusQuestionCommand(Guid Id)
        {
            this.Id = Id;
        }

        public override bool IsValid()
            => this.Id != Guid.Empty;
    }
    
    public class UpdateStatusQuestionCommand : QuestionCommand
    {
        public UpdateStatusQuestionCommand(QuestionViewModel QuestionCommands)
        {
            this.Obj = QuestionCommands;
        }

        public override bool IsValid()
            => this.Obj.Id != Guid.Empty;
    }
}
