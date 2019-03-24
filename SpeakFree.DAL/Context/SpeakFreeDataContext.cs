namespace SpeakFree.DAL.Context
{
    using System.Net.Mime;

    using Microsoft.EntityFrameworkCore;

    public class SpeakFreeDataContext: DbContext
    {
        public SpeakFreeDataContext()
        {
            this.Database.EnsureCreated();
        }
    }
}
