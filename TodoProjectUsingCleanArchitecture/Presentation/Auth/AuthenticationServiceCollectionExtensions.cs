using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TodoProjectUsingCleanArchitecture.Contract;

namespace TodoProjectUsingCleanArchitecture.Presentation.Auth
{
    public static class AuthenticationServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                };
            });
            
            // Add authorization policies based on token permission claims.
            // These policies require specific JWT claims to be set to "true" for the action to be authorized.
            services.AddAuthorization(options =>
            {
                // CanCreate Policy: Authorizes requests that add/create items.
                options.AddPolicy(ApiEndpoints.Policies.CanCreate, policy => 
                    policy.RequireClaim(ApiEndpoints.Policies.CanCreate, "true"));
                
                // CanEdit Policy: Authorizes requests that edit/update items.
                options.AddPolicy(ApiEndpoints.Policies.CanEdit, policy => 
                    policy.RequireClaim(ApiEndpoints.Policies.CanEdit, "true"));
                
                // CanDelete Policy: Authorizes requests that delete items.
                options.AddPolicy(ApiEndpoints.Policies.CanDelete, policy => 
                    policy.RequireClaim(ApiEndpoints.Policies.CanDelete, "true"));
                
                // CanAssign Policy: Authorizes requests that update authorization/permissions for other users.
                options.AddPolicy(ApiEndpoints.Policies.CanAssign, policy => 
                    policy.RequireClaim(ApiEndpoints.Policies.CanAssign, "true"));
            });

            return services;
        }
    }
}
