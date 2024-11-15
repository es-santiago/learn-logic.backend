using LearnLogic.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace LearnLogic.Infra.Data.Context
{
    public sealed class DataContextSolution : DbContext, IDataContextSolution
    {
        public DataContextSolution(DbContextOptions<DataContextSolution> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureEntity(modelBuilder);
            CallSeeds(modelBuilder);
        }

        public static void ConfigureEntity(ModelBuilder modelBuilder)
        {
            var configurations = Assembly.GetExecutingAssembly().DefinedTypes
                .Where(t => t.ImplementedInterfaces.Any(i => i.Name == typeof(IEntityTypeConfiguration<>).Name))
                .Where(i => i.IsClass && !i.IsAbstract && !i.IsNested)
                .ToList();

            foreach (var config in configurations)
            {
                dynamic instance = Activator.CreateInstance(config);
                modelBuilder.ApplyConfiguration(instance);
            }
        }

        public static void CallSeeds(ModelBuilder modelBuilder) { }
    }
}
