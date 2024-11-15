using System;

namespace LearnLogic.Infra.Data.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Activated { get; set; }
    }
}
