using LearnLogic.Domain.DTO;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace LearnLogic.Domain.Interfaces.Repository
{
    public interface IQuestionRepository : IBaseRepository<QuestionDTO>
    {
        int Count();
        IQueryable<QuestionItemsDTO> GetItemByQuestion(Guid questionId);
        IQueryable<QuestionAnswerDTO> ListByUser(Guid userId);
        IQueryable<QuestionAnswerDTO> GetQuestionAnswer();
        Task AnswerSaveAsync(QuestionAnswerDTO obj);
    }
}
