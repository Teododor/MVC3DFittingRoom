using Proiect.Common.DTOs;

namespace ProiectAcademie.Code.ExtensionMethods
{
    public static class ServiceCollectionExtensionMethods
    {
        public static IServiceCollection AddCurrentUser(this IServiceCollection services)
        {
            services.AddScoped(s =>
            {
                var accessor = s.GetService<IHttpContextAccessor>();
                var httpContext = accessor.HttpContext;
                var claims = httpContext.User.Claims;

                var userIdClaim = claims?.FirstOrDefault(c => c.Type == "Id")?.Value;
                var isParsingSuccessful = Int32.TryParse(userIdClaim, out int id);

                return new CurrentUserDto
                {
                    Id = id,
                    IsAuthenticated = httpContext.User.Identity.IsAuthenticated,
                    Email = httpContext.User.Identity.Name,
/*                    FirstName = httpContext.User.Identity.Name,
                    LastName = httpContext.User.Identity.*/
                };
            });

            return services;
        }
    }
}
