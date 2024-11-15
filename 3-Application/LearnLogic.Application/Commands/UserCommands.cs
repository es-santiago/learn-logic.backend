using LearnLogic.Domain.ViewModels;
using NetDevPack.Messaging;

namespace LearnLogic.Application.Commands
{
    public abstract class UserCommand : Command
    {
        public UserViewModel Obj { get; protected set; }
    }

    public class RegisterNewUserCommand : UserCommand
    {
        public RegisterNewUserCommand(UserViewModel UserCommands)
            => this.Obj = UserCommands;

        public override bool IsValid()
            => (ValidationResult = new RegisterNewUserCommandValidation().Validate(this)).IsValid;
    }

    public class UpdateUserCommand : UserCommand
    {
        public UpdateUserCommand(UserViewModel UserCommands)
            => this.Obj = UserCommands;


        public override bool IsValid()
            => (ValidationResult = new UpdateUserCommandValidation().Validate(this)).IsValid;
    }

    public class ChangeStatusUserCommand : UserCommand
    {
        public Guid Id { get; protected set; }
        public ChangeStatusUserCommand(Guid Id)
            => this.Id = Id;

        public override bool IsValid()
            => this.Id != Guid.Empty;
    }

    public class ChangePasswordCommand : UserCommand
    {
        public ChangePasswordViewModel ChangePassword { get; protected set; }
        public ChangePasswordCommand(ChangePasswordViewModel command)
            => this.ChangePassword = command;

        public override bool IsValid()
            => (ValidationResult = new ChangePasswordCommandValidation().Validate(this)).IsValid;
    }
    
    public class RegisterInitialPointCommand : UserCommand
    {
        public RegisterInitialPointCommand(UserViewModel command)
            => this.Obj = command;

        public override bool IsValid()
            => true;
    }
}
