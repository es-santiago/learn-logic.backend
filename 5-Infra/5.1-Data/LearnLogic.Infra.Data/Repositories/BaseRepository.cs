using AutoMapper;
using LearnLogic.Domain.DTO;
using LearnLogic.Domain.Interfaces.Repository;
using LearnLogic.Infra.Data.Entities;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LearnLogic.Infra.Data.Repositories
{
    public abstract class BaseRepository<TEntity, TDto>
        where TEntity : BaseEntity, new()
        where TDto : BaseDTO, new()
    {
        protected readonly IMapper _mapper;
        protected IDataContextSolution _context;
        private readonly IDataUnitOfWork _uow;
        private DbSet<TEntity> _dbSet;

        protected BaseRepository(IDataUnitOfWork uow, IMapper mapper)
        {
            _context = uow.Context;
            _uow = uow;
            _mapper = mapper;
        }

        protected DbSet<TEntity> DbSet => _dbSet ??= _context.Set<TEntity>();

        protected virtual IQueryable<TEntity> Query(bool tracking = false)
             => tracking ? DbSet : DbSet.AsNoTracking();

        public virtual IQueryable<TDto> List(bool tracking = false)
            => _mapper.ProjectTo<TDto>(Query(tracking));

        public virtual TDto Get(Guid id)
            => _mapper.Map<TDto>(Query(false).FirstOrDefault(x => x.Id == id));

        public async Task<TDto> GetAsync(Guid id)
            => _mapper.Map<TDto>(await Query(false).FirstOrDefaultAsync(x => x.Id == id));

        public virtual TDto Save(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            if (entity.Id.Equals(Guid.Empty))
            {
                entity.Id = Guid.NewGuid();
                entity.CreationDate = DateTime.Now;
                DbSet.Add(entity);
            }
            else
            {
                _context.Entry(entity).State = EntityState.Detached;
                entity.UpdateDate = DateTime.Now;
                DbSet.Update(entity);
            }

            _context.SaveChanges();
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto> SaveAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            if (entity.Id.Equals(Guid.Empty))
            {
                entity.Id = Guid.NewGuid();
                entity.CreationDate = DateTime.Now;
                await DbSet.AddAsync(entity);
            }
            else
            {
                _context.Entry(entity).State = EntityState.Detached;
                entity.UpdateDate = DateTime.Now;
                DbSet.Update(entity);
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<TDto>(entity);
        }

        public virtual void Update(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            entity.UpdateDate = DateTime.UtcNow;
            DbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public virtual void UpdateEspecificFields(TDto dto, List<string> propertiesToUpdate)
        {
            var entity = _mapper.Map<TEntity>(dto);

            // Atualize apenas as propriedades especificadas
            foreach (var propertyName in propertiesToUpdate)
            {
                var newValue = typeof(TDto).GetProperty(propertyName)?.GetValue(entity);

                if (newValue != null)
                {
                    var entityProperty = typeof(TEntity).GetProperty(propertyName);
                    if (entityProperty != null && entityProperty.CanWrite)
                    {
                        entityProperty.SetValue(entity, newValue);
                    }
                }
            }
            _context.Entry(entity).State = EntityState.Detached;
            entity.UpdateDate = DateTime.Now;
            DbSet.Update(entity);
            _context.SaveChanges();
        }


        public virtual void Delete(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            DbSet.Remove(entity);
            _context.SaveChanges();
        }

        public virtual void ChangeStatus(TDto dto)
        {
            dto.Activated = !dto.Activated;
            Update(dto);
        }
    }
}
