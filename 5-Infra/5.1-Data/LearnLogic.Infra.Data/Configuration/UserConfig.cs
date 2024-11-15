using LearnLogic.Infra.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnLogic.Infra.Data.Configuration
{
    public class UserConfig : BaseConfig<UserEntity>
    {
        public UserConfig() : base("User") { }

        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            base.Configure(builder);

            builder.Property(obj => obj.Email).IsRequired();
            builder.Property(obj => obj.Password).IsRequired();
            builder.Property(obj => obj.FirstName).IsRequired();
            builder.Property(obj => obj.LastName).IsRequired();
            builder.Property(obj => obj.FirstAccess).IsRequired();
            builder.Property(obj => obj.ExperienceLevel).IsRequired();

            builder.HasIndex(obj => obj.Email).IsUnique();
        }
    }
}
