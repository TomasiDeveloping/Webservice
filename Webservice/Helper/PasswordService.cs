using System.Security.Cryptography;
using System.Text;

namespace Webservice.Helper;

public static class PasswordService
{
    public static string CreateHash(string password)
    {
        using var sha256Hash = SHA256.Create();
        var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

        var builder = new StringBuilder();
        foreach (var t in bytes) builder.Append(t.ToString("x2"));
        return builder.ToString();
    }
}