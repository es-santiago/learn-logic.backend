using AutoMapper;
using LearnLogic.Domain.DTO;
using LearnLogic.Domain.Interfaces.Repository;
using LearnLogic.Domain.ViewModels;
using FluentValidation.Results;
using NetDevPack.Mediator;
using NetDevPack.Messaging;

namespace LearnLogic.Application.Services
{
    public abstract class BaseApplication<TVm, TDto, TRepository, CommandRegister, CommandUpdate, CommandChangeStatus> where TVm : BaseViewModel, new() where TDto : BaseDTO, new() where TRepository : IBaseRepository<TDto> where CommandRegister : Command where CommandUpdate : Command where CommandChangeStatus : Command
    {
        protected readonly IMapper _mapper;
        protected readonly TRepository _repository;
        protected readonly IMediatorHandler _mediator;

        public BaseApplication(TRepository repository, IMapper mapper, IMediatorHandler mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public virtual TDto Get(Guid id)
            => _repository.Get(id);

        public virtual IQueryable<TDto> List()
            => _repository.List();

        public virtual async Task<ValidationResult> Register(TVm vm)
        {
            var cmd = _mapper.Map<CommandRegister>(vm);
            return await _mediator.SendCommand(cmd);
        }

        public virtual async Task<ValidationResult> Update(TVm vm)
        {
            var cmd = _mapper.Map<CommandUpdate>(vm);
            return await _mediator.SendCommand(cmd);
        }

        public virtual async Task<ValidationResult> ChangeStatus(Guid id)
        {
            var cmd = _mapper.Map<CommandChangeStatus>(id);
            return await _mediator.SendCommand(cmd);
        }
    }
}
