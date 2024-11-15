using FluentValidation;

namespace LearnLogic.Application.Commands
{
    public class QuestionCommandValidation { }

    public class RegisterNewQuestionCommandValidation : QuestionValidation<RegisterNewQuestionCommand>
    {
        public RegisterNewQuestionCommandValidation()
        {
            ValidateId();
        }
    }

    public class UpdateQuestionCommandValidation : QuestionValidation<UpdateQuestionCommand>
    {
        public UpdateQuestionCommandValidation()
        {
            ValidateId();
            ValidateFields();
        }
    }

    public abstract class QuestionValidation<T> : AbstractValidator<T> where T : QuestionCommand 
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Obj.Id)
                .NotEqual(Guid.Empty);
        }


        protected void ValidateFields()
        {
            RuleFor(x => x.Obj.Title).NotEmpty().WithMessage("O t�tulo � obrigat�rio.");
            RuleFor(x => x.Obj.Level).IsInEnum().WithMessage("O n�vel da quest�o � obrigat�rio.");
            RuleFor(x => x.Obj.Status).IsInEnum().WithMessage("O status da quest�o � obrigat�rio.");
            RuleFor(x => x.Obj.Points).GreaterThan(0).WithMessage("A quantidade de pontos deve ser maior que zero.");
            RuleFor(x => x.Obj.Items).NotNull().NotEmpty().WithMessage("Os itens s�o obrigat�rios.");
        }
    }
}
