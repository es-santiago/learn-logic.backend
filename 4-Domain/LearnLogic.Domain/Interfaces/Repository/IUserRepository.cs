using LearnLogic.Domain.DTO;
using System.Linq;

namespace LearnLogic.Domain.Interfaces.Repository
{
    public interface IUserRepository : IBaseRepository<UserDTO>
    {
        UserDTO GetUserByEmail(string email);
        IQueryable<UserDTO> GetTopUsers();
    }
}
