using MVC_Buisness_Logic_Task.Abstarctions.Repositories;
using MVC_Buisness_Logic_Task.DAL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MVC_Buisness_Logic_Task.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        AppDbContext _context;
        DbSet<T> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Create(T item)
        {
            _dbSet.Add(item);
            Save();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Remove(int id)
        {
            if (GetById(id) != null)
            {
                _dbSet.Remove(GetById(id));
                Save();
            }

        }

        public void Update(T item)
        {
            _dbSet.Update(item);
            Save();

        }



        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
