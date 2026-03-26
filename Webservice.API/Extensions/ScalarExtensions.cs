using Scalar.AspNetCore;

namespace Webservice.API.Extensions;

public static class ScalarExtensions
{
    extension(WebApplication app)
    {
        public void MapScalar()
        {
            app.MapScalarApiReference(options =>
            {
                options.WithTitle("Webservice API");
            });
        }
    }
}