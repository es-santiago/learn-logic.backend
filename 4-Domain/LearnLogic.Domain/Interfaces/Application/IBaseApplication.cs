using LearnLogic.Domain.DTO;
using LearnLogic.Domain.ViewModels;
using FluentValidation.Results;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLogic.Domain.Interfaces.Application
{
    public interface IBaseApplication<TVm, TDto> where TVm : BaseViewModel, new() where TDto : BaseDTO, new()
    {
        Task<ValidationResult> Register(TVm vm);
        Task<ValidationResult> Update(TVm vm);
        Task<ValidationResult> ChangeStatus(Guid id);
        TDto Get(Guid id);
        IQueryable<TDto> List();
    }
}
