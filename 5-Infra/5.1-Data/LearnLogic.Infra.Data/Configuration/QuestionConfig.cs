using LearnLogic.Infra.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnLogic.Infra.Data.Configuration
{
    public class QuestionConfig : BaseConfig<QuestionEntity>
    {
        public QuestionConfig() : base("Question") { }

        public override void Configure(EntityTypeBuilder<QuestionEntity> builder)
        {
            base.Configure(builder);

            builder.Property(obj => obj.Level).IsRequired();
            builder.Property(obj => obj.Title).IsRequired();
            builder.Property(obj => obj.Number).IsRequired();
            builder.Property(obj => obj.Points).IsRequired();
            builder.Property(obj => obj.SolutionIdentifier).IsRequired();
            builder.Property(obj => obj.UserId).IsRequired();

            builder.Property(obj => obj.Tags).HasDefaultValue(null);
            builder.Property(obj => obj.CodeSnippet).HasDefaultValue(null);
            builder.Property(obj => obj.Description).HasDefaultValue(null);

            builder.HasIndex(obj => obj.Title).IsUnique();
            builder.HasIndex(obj => obj.Number).IsUnique();

            builder.HasMany(q => q.Items)
                   .WithOne(qi => qi.Question)
                   .HasForeignKey(qi => qi.QuestionId);
        }
    }
}
