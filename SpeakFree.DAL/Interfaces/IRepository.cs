namespace SpeakFree.DAL.Interfaces
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IRepository<T> where T: class
    {
        IEnumerable<T> GetAll();

        T Get(int id);

        IEnumerable<T> Find(Func<T, Boolean> predicacte);

        void Create(T item);

        void Update(T item);

        void Delete(T item);
    }
}
