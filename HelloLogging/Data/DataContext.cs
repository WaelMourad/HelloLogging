using HelloLogging.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HelloLogging.Data
{
    public class DataContext : DbContext
    {
        private readonly ILoggerFactory loggerFactory;

        public DataContext(DbContextOptions<DataContext> options, ILoggerFactory loggerFactory)
            : base(options)
        {
            this.loggerFactory = loggerFactory;
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (loggerFactory != null && Debugger.IsAttached)
            {
                optionsBuilder?.UseLoggerFactory(loggerFactory);
            }
        }

        public void AddTestData(DataContext dataContext)
        {
            var user1 = new User
            {
                Id = 1,
                FirstName = "Wael",
                LastName = "Mourad"
            };

            dataContext.Users.Add(user1);

            var post1 = new Post
            {
                Id = 1,
                UserId = user1.Id,
                Content = "What a piece of junk!",
                DateTime = DateTime.Now
            };

            dataContext.Posts.Add(post1);

            dataContext.SaveChanges();
        }
    }
}
