using System;
using System.Collections.Generic;
using System.Text;

namespace School.DAL.Interfaces
{
   public interface IRepository<T> where T : class
    {
        void Create(T item);
        void Update(T item);
        T Get(int id);
        IEnumerable<T> GetAll();
        void Delete(int id);
    }
}
