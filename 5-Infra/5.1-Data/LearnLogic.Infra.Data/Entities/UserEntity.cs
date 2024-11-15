using LearnLogic.Domain.Enum;
using System;

namespace LearnLogic.Infra.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public bool FirstAccess { get; set; } = true;
        public UserType Type { get; set; }
        public ExperienceLevel ExperienceLevel { get; set; }
        public decimal Punctuation { get; set; }
    }
}
