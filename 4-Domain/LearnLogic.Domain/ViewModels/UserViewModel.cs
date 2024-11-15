using LearnLogic.Domain.Enum;
using System;

namespace LearnLogic.Domain.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType Type { get; set; }
        public decimal Punctuation { get; set; }
    }
}
