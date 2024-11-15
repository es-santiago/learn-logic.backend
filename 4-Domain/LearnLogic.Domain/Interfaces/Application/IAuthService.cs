using LearnLogic.Domain.DTO;
using LearnLogic.Domain.ViewModels;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace LearnLogic.Domain.Interfaces.Application
{
    public interface IAuthService
    {
        Task<ValidationResult> RegisterUser(UserViewModel vm);
        Task<ValidationResult> UpdateUser(UserViewModel vm);
        string GenerateToken(UserDTO user);
    }
}
