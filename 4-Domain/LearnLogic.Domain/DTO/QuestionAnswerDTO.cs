using System;

namespace LearnLogic.Domain.DTO
{
    public class QuestionAnswerDTO : BaseDTO
    {
        public Guid QuestionId { get; set; }
        public Guid UserId { get; set; }
        public Guid SelectedItemId { get; set; }
        public bool Correct { get; set; }
        public string Comment { get; set; }
    }
}
