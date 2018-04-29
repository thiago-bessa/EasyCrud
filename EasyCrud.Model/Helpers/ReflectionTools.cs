using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using EasyCrud.Model.Attributes;
using EasyCrud.Model.Exceptions;
using EasyCrud.Model.Info;

namespace EasyCrud.Model.Helpers
{
    public static class ReflectionTools
    {
        public static RepositoryInfo GetRepositoryInfo(PropertyInfo repository)
        {
            var repositoryAttribute = repository.GetCustomAttribute<RepositoryAttribute>();

            return new RepositoryInfo
            {
                Name = repository.Name,
                Label = repositoryAttribute.Label,
                Order = repositoryAttribute.Order,
                Type = repository.PropertyType.GenericTypeArguments.First()
            };
        }

        public static DbContextInfo GetDbContextInfo(string assemblyName, string contextName)
        {
            var assembly = Assembly.Load(assemblyName);
            var dbContextType = assembly.GetType(contextName);

            if (!typeof(DbContext).IsAssignableFrom(dbContextType))
            {
                throw new TypeIsNotDbContextException(contextName);
            }

            var repositories = dbContextType
                .GetProperties()
                .Where(p => p.PropertyType.IsGenericType)
                .Where(p => p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) && p.CustomAttributes.Any(c => c.AttributeType == typeof(RepositoryAttribute)))
                .ToDictionary(p => p.Name, GetRepositoryInfo);

            return new DbContextInfo
            {
                DbContext = (DbContext)Activator.CreateInstance(dbContextType),
                Repositories = repositories
            };
        }
    }
}
