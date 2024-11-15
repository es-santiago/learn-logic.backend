using System.ComponentModel;

namespace LearnLogic.Domain.Enum
{
    public enum ExperienceLevel
    {
        [Description("Iniciante")]
        Beginner,

        [Description("Intermediário")]
        Intermediate,

        [Description("Avançado")]
        Advanced,

        [Description("Especialista")]
        Expert
    }
}
