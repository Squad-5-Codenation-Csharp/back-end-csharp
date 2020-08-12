using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using CentralDeErros.Data.Repository;
using CentralDeErros.Repositories;
using CentralDeErros.Infrastructure;
using CentralDeErros.Services;
using CentralDeErros.Api.Data;
using Microsoft.EntityFrameworkCore;
using CentralDeErros.Data.Interfaces;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.InteropServices;

namespace CentralDeErros
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<CentralDeErrosApiContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("CentralDeErrosApiContext")));
            services.AddSwaggerGen();

            var key = Encoding.ASCII.GetBytes(Variables.JwtKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseDeveloperExceptionPage();


            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./v1/swagger.json", "Central de Erros API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
