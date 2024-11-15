using FluentValidation.Results;
using LearnLogic.Domain.DTO;
using LearnLogic.Domain.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLogic.Domain.Interfaces.Application
{
    public interface IQuestionApplication : IBaseApplication<QuestionViewModel, QuestionDTO>
    {
        Task<ValidationResult> UpdateStatus(QuestionViewModel vm);
        Task<object> RegisterResponse(QuestionAnswerViewModel vm);
        Task<ValidationResult> Comment(QuestionAnswerViewModel vm);
        IQueryable<QuestionDTO> QuestionsResolved();
        QuestionDTO SearchWithAnswer(Guid id);
        IQueryable<QuestionAnswerDTO> ListByUser();
    }
}
