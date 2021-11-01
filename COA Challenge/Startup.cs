using AutoMapper;
using COA.Core.Interfaces;
using COA.Core.Services;
using COA.Domain.Profiles;
using COA.Infrastructure.Data;
using COA.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace COA_Challenge
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
            // Json Options

            // Add SQL Server and Contexts on build.
            services.AddEntityFrameworkSqlServer();
            services.AddDbContextPool<AppDbContext>((services, options) =>
            {
                options.UseInternalServiceProvider(services);
                options.UseSqlServer(Configuration.GetConnectionString("ApplicationConnectionString"));
            });

            // AddAutoMapper and Configurations

            var mappingConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new UserProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Add DependenyInjections
            services.AddTransient<IUsersServices, UsersServices>();
            services.AddScoped<UOW>();

            // Add Controllers
            services.AddControllers();

            // Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "COA_Challenge", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "COA_Challenge v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
