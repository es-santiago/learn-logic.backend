using System;

namespace LearnLogic.Infra.Data.Entities
{
    public class QuestionAnswerEntity : BaseEntity
    {
        public Guid QuestionId { get; set; }
        public Guid UserId { get; set; }
        public Guid SelectedItemId { get; set; }
        public bool Correct { get; set; }
        public string Comment { get; set; }
    }
}
