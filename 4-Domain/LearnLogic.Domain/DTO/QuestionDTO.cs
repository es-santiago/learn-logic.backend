using LearnLogic.Domain.Enum;
using System;
using System.Collections.Generic;

namespace LearnLogic.Domain.DTO
{
    public class QuestionDTO : BaseDTO
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
        public virtual List<QuestionItemsDTO> Items { get; set; }
    }
}
