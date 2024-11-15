using LearnLogic.Infra.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnLogic.Infra.Data.Configuration
{
    public class QuestionItemsConfig : BaseConfig<QuestionItemsEntity>
    {
        public QuestionItemsConfig() : base("QuestionItems") { }

        public override void Configure(EntityTypeBuilder<QuestionItemsEntity> builder)
        {
            base.Configure(builder);

            builder.Property(obj => obj.QuestionId).IsRequired();
            builder.Property(obj => obj.Label).IsRequired();
            builder.Property(obj => obj.Order).IsRequired();
        }
    }
}
