using System.Collections.Generic;

namespace MVC_Buisness_Logic_Task.Abstarctions.Repositories
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Remove(int id);
        void Create(T item);
        void Update(T item);

        void Save();
    }
}
