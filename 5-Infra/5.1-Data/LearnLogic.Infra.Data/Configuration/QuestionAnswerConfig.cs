using LearnLogic.Infra.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnLogic.Infra.Data.Configuration
{
    public class QuestionAnswerConfig : BaseConfig<QuestionAnswerEntity>
    {
        public QuestionAnswerConfig() : base("QuestionAnswer") { }

        public override void Configure(EntityTypeBuilder<QuestionAnswerEntity> builder)
        {
            base.Configure(builder);

            builder.Property(obj => obj.QuestionId).IsRequired();
            builder.Property(obj => obj.UserId).IsRequired();
            builder.Property(obj => obj.SelectedItemId).IsRequired();
        }
    }
}
