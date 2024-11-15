using LearnLogic.Infra.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnLogic.Infra.Data.Configuration
{
    public abstract class BaseConfig<TType> : IEntityTypeConfiguration<TType> where TType : BaseEntity
    {
        public BaseConfig(string tableName)
            => TableName = tableName;

        public string TableName { get; }

        public virtual void Configure(EntityTypeBuilder<TType> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Activated).HasDefaultValue(true);
            builder.Property(x => x.CreationDate).IsRequired();
        }
    }
}
