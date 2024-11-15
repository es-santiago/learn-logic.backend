using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnLogic.Domain.ViewModels
{
    public class QuestionItemsViewModel : BaseViewModel
    {
        public Guid QuestionId { get; set; }
        public string Label { get; set; }
        public bool IsCorrect { get; set; }
        public int Order { get; set; }
        public virtual QuestionViewModel Question { get; set; }
    }
}
