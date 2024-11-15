using AutoMapper;
using LearnLogic.Domain.DTO;
using LearnLogic.Domain.Interfaces.Repository;
using LearnLogic.Infra.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLogic.Infra.Data.Repositories
{
    public class QuestionRepository : BaseRepository<QuestionEntity, QuestionDTO>, IQuestionRepository
    {
        public QuestionRepository(IDataUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }

        public int Count()
        {
            int totalCount = (from q in _context.Set<QuestionEntity>()
                              select q).Count();

            return totalCount;
        }

        public IQueryable<QuestionItemsDTO> GetItemByQuestion(Guid questionId)
        {
            var query = from q in _context.Set<QuestionItemsEntity>()
                        where q.QuestionId == questionId
                        select q;

            return _mapper.ProjectTo<QuestionItemsDTO>(query);
        }

        public IQueryable<QuestionAnswerDTO> ListByUser(Guid userId)
        {
            var query = from q in _context.Set<QuestionAnswerEntity>()
                        where q.UserId == userId
                        select q;

            return _mapper.ProjectTo<QuestionAnswerDTO>(query);
        }

        public IQueryable<QuestionAnswerDTO> GetQuestionAnswer()
        {
            var query = from q in _context.Set<QuestionAnswerEntity>()
                        select q;

            return _mapper.ProjectTo<QuestionAnswerDTO>(query);
        }

        public async Task AnswerSaveAsync(QuestionAnswerDTO obj)
        {
            var dbSet = this._context.Set<QuestionAnswerEntity>();
            var entity = this._mapper.Map<QuestionAnswerEntity>(obj);
            await dbSet.AddAsync(entity);
            await this._context.SaveChangesAsync();
        }
    }
}
