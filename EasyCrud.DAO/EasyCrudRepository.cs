using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Exceptions;
using EasyCrud.Model.Info;

namespace EasyCrud.DAO
{
    public class EasyCrudRepository
    {
        private readonly DbSet _dbSet;
        private readonly Type _repositoryType;
        private readonly DbContextInfo _dbContextInfo;
        private readonly string _repositoryName;

        public EasyCrudRepository(DbContextInfo contextInfo, string repositoryName)
        {
            _dbContextInfo = contextInfo;
            _repositoryName = repositoryName;
            _repositoryType = contextInfo.Repositories[repositoryName].Type;
            _dbSet = contextInfo.DbContext.Set(_repositoryType);

            var list = List();
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

        public List<object> List()
        {
            return Task.Run(async () => await _dbSet.AsNoTracking().ToListAsync()).Result;
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
            _dbContextInfo.DbContext.SaveChanges();
        }
    }
}
