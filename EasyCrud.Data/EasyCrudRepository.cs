using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EasyCrud.Model.Data;
using EasyCrud.Model.Exceptions;
using EasyCrud.Model.Info;
using EasyCrud.Model.Interfaces;
using EasyCrud.Model.ViewData;

namespace EasyCrud.Data
{
    public class EasyCrudRepository : IEasyCrudRepository
    {
        private readonly DbSet _dbSet;
        private readonly DbContext _dbContext;
        private readonly RepositoryInfo _repositoryInfo;

        public EasyCrudRepository(DbContext context, RepositoryInfo repositoryInfo)
        {
            _dbContext = context;
            _repositoryInfo = repositoryInfo;
            _dbSet = context.Set(repositoryInfo.Type);
        }

        public List<Entity> List(Criteria criteria)
        {
            return Task.Run(async () =>
            {
                var entitiesViewData = new List<Entity>();
                var dbEntities = await _dbSet.AsNoTracking().ToListAsync();

                foreach (var dbEntity in dbEntities)
                {
                    var entity = new Entity();

                    _repositoryInfo.Columns.ForEach(c => entity.Data.Add(c.Name, c.PropertyInfo.GetValue(dbEntity)));
                    entity.Id = _repositoryInfo.IdColumn.GetValue(dbEntity);

                    entitiesViewData.Add(entity);
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

            return _repositoryInfo.Type.GetProperties()
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
                throw new EntityNotFoundException(_repositoryInfo.Name, id);
            }

            return entity;
        }

        private void SaveEntity(Dictionary<string, object> data, object entity)
        {
            foreach (var pair in data)
            {
                var property = _repositoryInfo.Type.GetProperty(pair.Key);

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
