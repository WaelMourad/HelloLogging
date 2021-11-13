using HelloLogging.Data;
using HelloLogging.Interfaces;
using HelloLogging.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloLogging.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext dataContext;
        private readonly ILogger<UserService> logger;

        public UserService(DataContext dataContext, ILogger<UserService> logger)
        {
            this.dataContext = dataContext;
            this.logger = logger;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await dataContext.Users
             .Include(x => x.Posts)
             .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            for (int i = 0; i < 1000000; i++)
            {
                // using $ is a bad practice, instead use standard log sructure which Enhance Logging Performance ..
                // logger.LogDebug($"i: {i}"); 
                logger.LogDebug("i: {i}", i);
            }

            var users = await dataContext.Users
              .Include(x => x.Posts)
              .ToListAsync();

            return users;
        }
    }
}
