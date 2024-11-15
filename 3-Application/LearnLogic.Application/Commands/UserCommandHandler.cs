using AutoMapper;
using LearnLogic.Domain.DTO;
using LearnLogic.Domain.Interfaces.Repository;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Mediator;
using NetDevPack.Messaging;
using LearnLogic.Infra.CrossCutting.Extensions;
using LearnLogic.Domain.Interfaces.Application;
using LearnLogic.Domain.Enum;

namespace LearnLogic.Application.Commands
{
    public class UserCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewUserCommand, ValidationResult>,
        IRequestHandler<UpdateUserCommand, ValidationResult>,
        IRequestHandler<ChangeStatusUserCommand, ValidationResult>,
        IRequestHandler<ChangePasswordCommand, ValidationResult>,
        IRequestHandler<RegisterInitialPointCommand, ValidationResult>
    {
        private readonly IUserRepository _repository;
        private readonly IMediatorHandler _mediator;
        private readonly IMapper _mapper;
        private readonly IUserClaimsAccessor _userClaimsAccessor;

        public UserCommandHandler(IMediatorHandler mediator, IUserClaimsAccessor userClaimsAccessor, IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
            _userClaimsAccessor = userClaimsAccessor;
        }

        public async Task<ValidationResult> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var userDto = _mapper.Map<UserDTO>(request.Obj);
            userDto.Email = userDto.Email.ToLower();

            if (UserExistsByEmail(userDto.Email))
            {
                AddError("Já existe um usuário com este e-mail.");
                return ValidationResult;
            }

            userDto.Password = userDto.Password.GenerateHash();

            await _repository.SaveAsync(userDto);
            return new ValidationResult();
        }

        public async Task<ValidationResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userDto = _mapper.Map<UserDTO>(request.Obj);
            userDto.Email = userDto.Email.ToLower();

            if (UserExistsByEmail(userDto.Email, userDto.Id))
            {
                AddError("Já existe um usuário com este e-mail.");
                return ValidationResult;
            }

            var existingUser = await _repository.GetAsync(userDto.Id);
            if (existingUser == null)
            {
                AddError("Usuário não encontrado.");
                return ValidationResult;
            }

            UpdateUser(existingUser, userDto);
            await _repository.SaveAsync(existingUser);

            return new ValidationResult();
        }

        public async Task<ValidationResult> Handle(ChangeStatusUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var user = await _repository.GetAsync(request.Id);
            if (user == null)
            {
                AddError("Usuário não encontrado.");
                return ValidationResult;
            }
            user.Activated = !user.Activated;

            await _repository.SaveAsync(user);
            return new ValidationResult();
        }

        public async Task<ValidationResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var user = await _repository.GetAsync(_userClaimsAccessor.UserId);
            if (user == null)
            {
                AddError("Usuário não encontrado.");
                return ValidationResult;
            }

            var passwordIsValid = user.Password.Equals(request.ChangePassword.Password.GenerateHash());

            if (!passwordIsValid) 
            {
                AddError("Senha incorreta.");
                return ValidationResult;
            }
            
            if (!request.ChangePassword.NewPassword.Equals(request.ChangePassword.ConfirmNewPassword))
            {
                AddError("As senhas não coinciden.");
                return ValidationResult;
            }

            var db = await _repository.GetAsync(request.ChangePassword.UserId);

            db.Password = request.ChangePassword.NewPassword.GenerateHash();

            await _repository.SaveAsync(db);
            return new ValidationResult();
        }
        
        public async Task<ValidationResult> Handle(RegisterInitialPointCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var user = await _repository.GetAsync(_userClaimsAccessor.UserId);
            if (user == null)
            {
                AddError("Usuário não encontrado.");
                return ValidationResult;
            }

            user.FirstAccess = false;
            user.Punctuation = request.Obj.Punctuation;

            if (request.Obj.Punctuation <= 5)
                user.ExperienceLevel = ExperienceLevel.Beginner;
            else if (request.Obj.Punctuation <= 10)
                user.ExperienceLevel = ExperienceLevel.Intermediate;
            else if (request.Obj.Punctuation <= 15)
                user.ExperienceLevel = ExperienceLevel.Beginner;
            else if (request.Obj.Punctuation > 15)
                user.ExperienceLevel = ExperienceLevel.Expert;


            await _repository.SaveAsync(user);
            return new ValidationResult();
        }

        private bool UserExistsByEmail(string email, Guid? excludeUserId = null)
        {
            var existingUser = _repository.GetUserByEmail(email);
            return existingUser != null && (!excludeUserId.HasValue || existingUser.Id != excludeUserId.Value);
        }

        private static void UpdateUser(UserDTO existingUser, UserDTO userDto)
        {
            existingUser.FirstName = userDto.FirstName;
            existingUser.LastName = userDto.LastName;
            existingUser.BirthDate = userDto.BirthDate;
            existingUser.Email = userDto.Email;
            existingUser.Type = userDto.Type;
        }
    }
}
