using FluentValidation;

namespace LearnLogic.Application.Commands
{
    public class RegisterNewUserCommandValidation : UserValidation<RegisterNewUserCommand>
    {
        public RegisterNewUserCommandValidation()
        {
            ValidateFields();
        }
    }

    public class UpdateUserCommandValidation : UserValidation<UpdateUserCommand>
    {
        public UpdateUserCommandValidation()
        {
            ValidateId();
            ValidateFields();
        }
    }

    public class ChangePasswordCommandValidation : UserValidation<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidation()
        {
            RuleFor(c => c.ChangePassword.UserId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.ChangePassword.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");

            RuleFor(x => x.ChangePassword.ConfirmNewPassword)
                .NotEmpty().WithMessage("A confirmação de senha é obrigatória.")
                .MinimumLength(6).WithMessage("A confirmação de senha deve ter pelo menos 6 caracteres.");

            RuleFor(x => x.ChangePassword.NewPassword)
                .NotEmpty().WithMessage("A nova senha é obrigatória.")
                .MinimumLength(6).WithMessage("A nova senha deve ter pelo menos 6 caracteres.");
        }
    }

    public abstract class UserValidation<T> : AbstractValidator<T> where T : UserCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Obj.Id)
                .NotEqual(Guid.Empty);
        }


        protected void ValidateFields()
        {
            RuleFor(x => x.Obj.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.");

            RuleFor(x => x.Obj.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");

            RuleFor(x => x.Obj.FirstName)
                .NotEmpty().WithMessage("O primeiro nome é obrigatório.");

            RuleFor(x => x.Obj.LastName)
                .NotEmpty().WithMessage("O sobrenome é obrigatório.");

            RuleFor(x => x.Obj.Type)
                .IsInEnum().WithMessage("Tipo de usuário inválido.");
        }
    }
}
