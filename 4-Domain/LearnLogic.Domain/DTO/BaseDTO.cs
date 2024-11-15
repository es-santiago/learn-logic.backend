using System;

namespace LearnLogic.Domain.DTO
{
    public class BaseDTO
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Activated { get; set; }
    }
}
