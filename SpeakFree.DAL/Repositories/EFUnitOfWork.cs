using System;
using System.Collections.Generic;
using System.Text;

namespace SpeakFree.DAL.Repositories
{
    using SpeakFree.DAL.Context;
    using SpeakFree.DAL.Interfaces;
    using SpeakFree.DAL.Models;

    public class EFUnitOfWork : IUnitOfWork
    {
        private SpeakFreeDataContext context;
        public IRepository<User> Users { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IRepository<Message> Messages { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        public EFUnitOfWork(string connectionString)
        {
            this.context = new SpeakFreeDataContext();
            //this.context = new SpeakFreeDataContext(connectionString);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
