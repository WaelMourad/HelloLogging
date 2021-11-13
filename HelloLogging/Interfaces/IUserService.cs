using HelloLogging.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloLogging.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(int id);
    }
}
