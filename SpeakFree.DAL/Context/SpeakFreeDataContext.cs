namespace SpeakFree.DAL.Context
{
    using System.Net.Mime;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using SpeakFree.DAL.Models;

    public class SpeakFreeDataContext: IdentityDbContext<User>
    {
        public DbSet<Message> Messages { get; set; }

		public SpeakFreeDataContext(DbContextOptions<SpeakFreeDataContext> options)
	        :base(options)
        {
            this.Database.EnsureCreated();
        }
    }
}
