using AutoMapper;
using LearnLogic.Domain.DTO;
using LearnLogic.Domain.Interfaces.Repository;
using LearnLogic.Infra.Data.Entities;
using System.Linq;

namespace LearnLogic.Infra.Data.Repositories
{
    public class UserRepository : BaseRepository<UserEntity, UserDTO>, IUserRepository
    {
        public UserRepository(IDataUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }

        public UserDTO GetUserByEmail(string email)
        {
            var user = this.Query().FirstOrDefault(x => x.Email.Equals(email));
            return _mapper.Map<UserDTO>(user);
        }

        public IQueryable<UserDTO> GetTopUsers()
        {
            var query = from u in _context.Set<UserEntity>()
                        orderby u.Punctuation descending
                        select new UserDTO
                        {
                            Email = u.Email,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Id = u.Id
                        };

            return query.Take(5);
        }
    }
}
