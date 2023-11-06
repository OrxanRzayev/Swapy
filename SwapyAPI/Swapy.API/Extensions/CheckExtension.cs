using Swapy.API.Middlewares;

namespace Swapy.API.Extensions
{
    public static class CheckExtension
    {
        public static IApplicationBuilder UseCheck(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CheckMiddleware>();
        }
    }
}
