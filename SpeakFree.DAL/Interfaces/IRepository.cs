namespace SpeakFree.DAL.Interfaces
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<T> where T: class
    {
        IEnumerable<T> GetAll();

        T Get(int id);

        IEnumerable<T> Find(Func<T, Boolean> predicacte);

        Task Create(T item);

        Task Update(T item);

        Task Delete(T item);
    }
}
