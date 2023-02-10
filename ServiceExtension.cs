using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1
{
    public static class ServiceExtension
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<ApiUser>(opt => {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 0;

                }) ;
            builder=new IdentityBuilder(builder.UserType,typeof(IdentityRole),services);
            builder.AddEntityFrameworkStores<HotelDbContext>().AddDefaultTokenProviders();
        }
        public static void ConfigureJWT(this IServiceCollection services,IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            var key = Environment.GetEnvironmentVariable("KEY");
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });
        }
        public static void ConfigureExceptionHandler(this IApplicationBuilder app) {
            app.UseExceptionHandler(error => {
            error.Run(async context =>{
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType="application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if(contextFeature != null)
                {
                    Log.Error($"Something went wrong in {contextFeature.Error}");
                    await context.Response.WriteAsync(new Error
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Internal Server Error Please try again later."
                    }.ToString());
                }
            });
            });
        
        }
    
    }
}
