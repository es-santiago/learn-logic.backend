using System;

namespace LearnLogic.Domain.ViewModels
{
    public class ChangePasswordViewModel
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
