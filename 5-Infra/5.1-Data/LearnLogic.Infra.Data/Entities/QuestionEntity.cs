using System.Collections.Generic;
using System;
using LearnLogic.Domain.Enum;

namespace LearnLogic.Infra.Data.Entities
{
    public class QuestionEntity : BaseEntity
    {
        public string Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public QuestionLevel Level { get; set; }
        public QuestionStatus Status { get; set; }
        public List<string> Tags { get; set; }
        public string CodeSnippet { get; set; }
        public Guid SolutionIdentifier { get; set; }
        public int Points { get; set; }
        public Guid UserId { get; set; }
        public string ImageBase64 { get; set; }
        public string ImageName { get; set; }
        public virtual ICollection<QuestionItemsEntity> Items { get; set; }
    }
}
