using Webservice.Models;

namespace Webservice.Interfaces;

public interface IUserService
{
    Task<User> ValidateCredentials(string username, string password);
}