using System.Threading.Tasks;
using Webservice.Interfaces;
using Webservice.Models;

namespace Webservice.Services
{
    public class LogService : ILogService
    {
        private readonly WebserviceContext _context;

        public LogService(WebserviceContext context)
        {
            _context = context;
        }
        public async Task<bool> Log(Log log)
        {
            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}