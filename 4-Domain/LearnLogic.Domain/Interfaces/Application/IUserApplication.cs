using FluentValidation.Results;
using LearnLogic.Domain.DTO;
using LearnLogic.Domain.ViewModels;
using System.Threading.Tasks;

namespace LearnLogic.Domain.Interfaces.Application
{
    public interface IUserApplication : IBaseApplication<UserViewModel, UserDTO> 
    {
        UserDTO GetLogged();
        UserDTO GetUserByEmail(string email);
        Task<ValidationResult> ChangePassword(ChangePasswordViewModel vm);
        Task<ValidationResult> RegisterInitialPoint(UserViewModel vm);
    }
}
