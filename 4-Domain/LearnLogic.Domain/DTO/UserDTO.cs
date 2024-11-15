using LearnLogic.Domain.Enum;
using System;

namespace LearnLogic.Domain.DTO
{
    public class UserDTO : BaseDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public UserType Type { get; set; }
        public bool FirstAccess { get; set; } = true;
        public ExperienceLevel ExperienceLevel { get; set; }
        public decimal Punctuation { get; set; }

        public string FullName { get; set; }
    }
}
