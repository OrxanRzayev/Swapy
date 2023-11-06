using Swapy.Common.Attributes;
using Swapy.DAL.Interfaces;

namespace Swapy.API.Middlewares
{
    public class CheckMiddleware : IMiddleware
    {
        private readonly IUserTokenRepository _userTokenRepository;

        public CheckMiddleware(IUserTokenRepository userTokenRepository)
        {
            _userTokenRepository = userTokenRepository;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            bool shouldCheck = context.GetEndpoint()?.Metadata?.Any(em => em.GetType() == typeof(CheckAttribute)) ?? false;

            if (shouldCheck)
            {
                if (!context.Request.Headers.ContainsKey("Authorization"))
                {
                    context.Items["Check"] = null;
                    await next(context);
                    return;
                }

                string authorizationHeader = context.Request.Headers["Authorization"];

                if (string.IsNullOrEmpty(authorizationHeader))
                {
                    context.Items["Check"] = null;
                    await next(context);
                    return;
                }

                if (!authorizationHeader.StartsWith("Bearer", StringComparison.OrdinalIgnoreCase))
                {
                    context.Items["Check"] = null;
                    await next(context);
                    return;
                }

                string token = authorizationHeader.Substring("Bearer".Length).Trim();

                if (string.IsNullOrEmpty(token))
                {
                    context.Items["Check"] = null;
                    await next(context);
                    return;
                }

                context.Items["Check"] = await GetUserId(token);
            }
            await next(context);
        }

        private async Task<string> GetUserId(string accessToken)
        {
            var userToken = await _userTokenRepository.GetByAccessTokenAsync(accessToken);

            if (!userToken.User.EmailConfirmed) return null;

            return userToken.UserId;
        }
    }
}
