using LearnLogic.Domain.DTO;
using LearnLogic.Domain.Enum;
using LearnLogic.Domain.Interfaces.Application;
using LearnLogic.Domain.Interfaces.Repository;

namespace LearnLogic.Application.Services
{
    public class HomeApplication : IHomeApplication
    {
        private readonly IUserClaimsAccessor _userClaimsAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IQuestionRepository _questionRepository;

        public HomeApplication(IUserClaimsAccessor userClaimsAccessor, IUserRepository userRepository, IQuestionRepository questionRepository)
        {
            _userClaimsAccessor = userClaimsAccessor;
            _userRepository = userRepository;
            _questionRepository = questionRepository;
        }

        public object Summary()
        {
            if (_userClaimsAccessor.UserType.Equals(UserType.Administrator))
            {
                var total_users = _userRepository.List().Where(x => x.Type.Equals(UserType.Student));

                var top_users = total_users.OrderByDescending(x => x.Punctuation).Take(5)
                                            .Select(u => new
                                            {
                                                Email = u.Email,
                                                FullName = $"{u.FirstName} {u.LastName}",
                                                Id = u.Id,
                                                Punctuation = u.Punctuation
                                            });

                var total_questions = _questionRepository.List();

                var total_answers = _questionRepository.GetQuestionAnswer().ToList().Count();

                var users_register_last_week = total_users.Count(x => x.CreationDate > DateTime.Now.AddDays(-7));

                return new
                {
                    total_users = total_users.Count(),
                    top_users,
                    total_questions = total_questions.Count(),
                    total_answers = total_answers,
                    users_register_last_week
                };
            }
            else
            {
                var list = _userRepository.List().Where(x => x.Type.Equals(UserType.Student)).OrderByDescending(x => x.Punctuation).ToList();
                var user = list.First(x => x.Id.Equals(_userClaimsAccessor.UserId));
                int position = list.IndexOf(user);
                var top_users = new List<UserDTO>();

                if (position > 5)
                {
                    top_users.AddRange(list.Take(5));
                    top_users.Add(user);
                }
                else
                {
                    top_users.AddRange(list.Take(5));
                }

                var filter = top_users.Select(u => new
                {
                    Email = u.Email,
                    FullName = $"{u.FirstName} {u.LastName}",
                    Id = u.Id,
                    Punctuation = u.Punctuation
                });

                return new
                {
                    total_users = 0,
                    top_users = filter,
                    total_questions = 0,
                    total_answers = 0,
                    users_register_last_week = 0
                };
            }
        }
    }
}
