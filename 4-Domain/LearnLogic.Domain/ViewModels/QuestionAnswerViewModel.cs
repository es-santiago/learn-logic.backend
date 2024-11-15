using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnLogic.Domain.ViewModels
{
    public class QuestionAnswerViewModel : BaseViewModel
    {
        public Guid QuestionId { get; set; }
        public Guid UserId { get; set; }
        public Guid SelectedItemId { get; set; }
        public bool Correct { get; set; }
        public string Comment { get; set; }
    }
}
