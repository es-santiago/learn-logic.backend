using AutoMapper;
using LearnLogic.Domain.DTO;
using LearnLogic.Domain.Interfaces.Application;
using LearnLogic.Domain.Interfaces.Repository;
using LearnLogic.Domain.ViewModels;
using LearnLogic.Application.Commands;
using NetDevPack.Mediator;
using LearnLogic.Domain.Exceptions;
using FluentValidation.Results;

namespace LearnLogic.Application.Services
{
    public class UserApplication : BaseApplication<UserViewModel, UserDTO, IUserRepository, RegisterNewUserCommand, UpdateUserCommand, ChangeStatusUserCommand>, IUserApplication
    {

        private readonly IUserClaimsAccessor _userClaimsAccessor;

        public UserApplication(IUserClaimsAccessor userClaimsAccessor, IUserRepository repository, IMapper mapper, IMediatorHandler mediator) : base(repository, mapper, mediator)
        {
            this._userClaimsAccessor = userClaimsAccessor;
        }

        public UserDTO GetLogged()
        {
            var user = Get(this._userClaimsAccessor.UserId);
            user.Password = "";
            user.ConfirmPassword = "";
            return user;
        }

        public UserDTO GetUserByEmail(string email)
        {
            var user = this._repository.GetUserByEmail(email.ToLower()) ?? throw new CustomException("Usuário não encontrado!");
            return user;
        }

        public override UserDTO Get(Guid id)
        {
            var user = _repository.Get(id);
            user.Password = "";
            user.ConfirmPassword = "";
            return user;
        }

        public async Task<ValidationResult> ChangePassword(ChangePasswordViewModel vm)
        {
            var cmd = _mapper.Map<ChangePasswordCommand>(vm);
            return await _mediator.SendCommand(cmd);
        }

        public async Task<ValidationResult> RegisterInitialPoint(UserViewModel vm)
        {
            var cmd = _mapper.Map<RegisterInitialPointCommand>(vm);
            return await _mediator.SendCommand(cmd);
        }
    }
}
