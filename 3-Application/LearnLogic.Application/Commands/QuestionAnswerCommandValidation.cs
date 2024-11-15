using FluentValidation;

namespace LearnLogic.Application.Commands
{
    public class QuestionAnswerCommandValidation { }

    public class RegisterNewQuestionAnswerCommandValidation : QuestionAnswerValidation<RegisterNewQuestionAnswerCommand>
    {
        public RegisterNewQuestionAnswerCommandValidation() { }
    }

    public class UpdateQuestionAnswerCommandValidation : QuestionAnswerValidation<UpdateQuestionAnswerCommand>
    {
        public UpdateQuestionAnswerCommandValidation() { }
    }

    public abstract class QuestionAnswerValidation<T> : AbstractValidator<T> where T : QuestionAnswerCommand { }
}
