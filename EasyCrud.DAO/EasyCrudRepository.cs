using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Exceptions;
using EasyCrud.Model.Info;
using EasyCrud.Model.Interfaces;
using EasyCrud.Model.ViewData;

namespace EasyCrud.DAO
{
    public class EasyCrudRepository : IEasyCrudRepository
    {
        private readonly DbSet _dbSet;
        private readonly DbContext _dbContext;
        private readonly Type _repositoryType;
        private readonly string _repositoryName;

        public EasyCrudRepository(DbContext context, RepositoryInfo repositoryInfo)
        {
            _dbContext = context;
            _repositoryName = repositoryInfo.Name;
            _repositoryType = repositoryInfo.Type;
            _dbSet = context.Set(_repositoryType);
        }

        public List<EntityViewData> List()
        {
            return Task.Run(async () =>
            {
                var entitiesViewData = new List<EntityViewData>();
                var entities = await _dbSet.AsNoTracking().ToListAsync();

                foreach (var entity in entities)
                {
                    entitiesViewData.Add(new EntityViewData
                    {
                        Data = _repositoryType
                            .GetProperties()
                            .ToDictionary(propertyInfo => propertyInfo.Name, propertyInfo => propertyInfo.GetValue(entity))
                    });
                }

                return entitiesViewData;

            }).Result;
        }

        public void Create(Dictionary<string, object> data)
        {
            var entity = Activator.CreateInstance(_dbSet.ElementType);
            SaveEntity(data, entity);
        }

        public Dictionary<string, object> Retrieve(object id)
        {
            var entity = GetEntity(id);

            return _repositoryType
                .GetProperties()
                .ToDictionary(propertyInfo => propertyInfo.Name, propertyInfo => propertyInfo.GetValue(entity));
        }

        public void Update(object id, Dictionary<string, object> data)
        {
            var entity = GetEntity(id);
            SaveEntity(data, entity);
        }

        public void Delete(object id)
        {
            var entity = GetEntity(id);
            _dbSet.Remove(entity);
        }

        private object GetEntity(object id)
        {
            var entity = _dbSet.Find(id);

            if (entity == null)
            {
                throw new EntityNotFoundException(_repositoryName, id);
            }

            return entity;
        }

        private void SaveEntity(Dictionary<string, object> data, object entity)
        {
            foreach (var pair in data)
            {
                var property = _repositoryType.GetProperty(pair.Key);

                if (property == null)
                {
                    throw new ColumnNotFoundException(pair.Key);
                }

                property.SetValue(entity, pair.Value);
            }

            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }
    }
}
