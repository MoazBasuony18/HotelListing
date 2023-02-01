using Microsoft.AspNetCore.Identity;
using WebApplication1.Data;

namespace WebApplication1
{
    public static class ServiceExtension
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<ApiUser>(opt => opt.User.RequireUniqueEmail = true);
            builder=new IdentityBuilder(builder.UserType,typeof(IdentityRole),services);
            builder.AddEntityFrameworkStores<HotelDbContext>().AddDefaultTokenProviders();
        }
    }
}
