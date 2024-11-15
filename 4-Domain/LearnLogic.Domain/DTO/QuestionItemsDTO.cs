using System;

namespace LearnLogic.Domain.DTO
{
    public class QuestionItemsDTO : BaseDTO
    {
        public Guid QuestionId { get; set; }
        public string Label { get; set; }
        public bool IsCorrect { get; set; }
        public int Order { get; set; }
        public virtual QuestionDTO Question { get; set; }
    }
}
