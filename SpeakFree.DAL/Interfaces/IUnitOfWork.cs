namespace SpeakFree.DAL.Interfaces
{
    using System;

    using SpeakFree.DAL.Models;

    public interface IUnitOfWork: IDisposable
    {
        IRepository<User> Users { get; set; }
    }
}
