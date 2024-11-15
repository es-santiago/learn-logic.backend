using System;

namespace LearnLogic.Infra.Data.Entities
{
    public class QuestionItemsEntity : BaseEntity
    {
        public Guid QuestionId { get; set; }
        public string Label { get; set; }
        public int Order { get; set; }
        public virtual QuestionEntity Question { get; set; }
    }
}
