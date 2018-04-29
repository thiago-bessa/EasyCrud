using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Exceptions;

namespace EasyCrud.Model.Info
{
    public class DbContextInfo
    {
        public string Name { get; set; }
        public DbContext DbContext { get; set; }
        public Dictionary<string, RepositoryInfo> Repositories { get; set; }
        
        public RepositoryInfo GetRepository(string repositoryName)
        {
            if (!Repositories.ContainsKey(repositoryName))
            {
                throw new RepositoryNotFoundException(Name, repositoryName);
            }

            return Repositories[repositoryName];
        }
        
        public IEnumerable<RepositoryInfo> GeAlltRepositories()
        {
            return Repositories.Select(repositoryInfo => repositoryInfo.Value);
        }
    }
}
