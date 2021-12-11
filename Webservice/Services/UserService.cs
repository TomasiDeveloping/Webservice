using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Webservice.Interfaces;
using Webservice.Models;

namespace Webservice.Services
{
    public class UserService : IUserService
    {
        private readonly WebserviceContext _context;

        public UserService(WebserviceContext context)
        {
            _context = context;
        }
        public async Task<User> ValidateCredentials(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u =>
                u.UserName.Equals(username) && u.Password.Equals(password));
        }
    }
}