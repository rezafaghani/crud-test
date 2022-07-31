using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace M2c.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyAllConfigurations(this ModelBuilder modelBuilder)
        {
            List<Type> typesToRegister = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces()
                    .Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .ToList();

            foreach (Type type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                if (configurationInstance != null) modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}