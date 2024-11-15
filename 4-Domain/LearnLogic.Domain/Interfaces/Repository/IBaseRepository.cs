using LearnLogic.Domain.DTO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLogic.Domain.Interfaces.Repository
{
    public interface IBaseRepository<TDto> where TDto : BaseDTO
    {
        Task<TDto> GetAsync(Guid id);
        TDto Get(Guid id);
        Task<TDto> SaveAsync(TDto dto);
        TDto Save(TDto dto);
        IQueryable<TDto> List(bool tracking = false);
        void ChangeStatus(TDto dto);
        void Update(TDto dto);
        void Delete(TDto dto);
    }
}
