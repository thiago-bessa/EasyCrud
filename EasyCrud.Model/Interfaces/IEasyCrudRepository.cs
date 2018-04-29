using System.Collections.Generic;
using EasyCrud.Model.ViewData;

namespace EasyCrud.Model.Interfaces
{
    public interface IEasyCrudRepository
    {
        void Create(Dictionary<string, object> data);
        void Delete(object id);
        List<EntityViewData> List();
        Dictionary<string, object> Retrieve(object id);
        void Update(object id, Dictionary<string, object> data);
    }
}