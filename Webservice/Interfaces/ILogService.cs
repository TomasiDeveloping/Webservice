using System.Threading.Tasks;
using Webservice.Models;

namespace Webservice.Interfaces
{
    public interface ILogService
    {
        Task<bool> Log(Log log);
    }
}