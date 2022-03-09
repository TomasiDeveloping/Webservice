using Webservice.Models;

namespace Webservice.Interfaces;

public interface ILogService
{
    Task<bool> LogAsync(Log log);
}