using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace rentACar.Persistence.Extensions
{
    public static class ModelBuilderExtensions
    {
        private const string _entityNamespace = "rentACar.Domain.Entities";
        public static void RegisterAllEntities<T>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(a => a.GetExportedTypes())
                                  .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && c.Namespace.StartsWith(_entityNamespace) && typeof(T).IsAssignableFrom(c));
            foreach (var type in types)
                modelBuilder.Entity(type);
        }

        public static void RegisterAllConfigurations(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public static void ConfigureAllDecimalFields(this ModelBuilder modelBuilder)
        {
            var decimals = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(decimal));
            const string DecimalConfig = "decimal(18, 2)";
            foreach (var property in decimals)
                property.SetColumnType(DecimalConfig);
        }
    }
}
