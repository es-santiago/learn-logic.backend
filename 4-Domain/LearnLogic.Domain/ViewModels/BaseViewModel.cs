using System;

namespace LearnLogic.Domain.ViewModels
{
    public class BaseViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Activated { get; set; }
    }
}
