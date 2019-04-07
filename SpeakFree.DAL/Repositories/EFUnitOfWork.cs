using System;
using System.Collections.Generic;
using System.Text;

namespace SpeakFree.DAL.Repositories
{
	using Microsoft.EntityFrameworkCore;

	using SpeakFree.DAL.Context;
    using SpeakFree.DAL.Interfaces;
    using SpeakFree.DAL.Models;

    public class EFUnitOfWork : IUnitOfWork
    {
        private SpeakFreeDataContext context;

		public IRepository<Message> Messages { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public IRepository<User> Users { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public EFUnitOfWork(string connectionString)
        {
			DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
			builder.UseSqlServer(connectionString);
            //this.context = new SpeakFreeDataContext(builder.);
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
