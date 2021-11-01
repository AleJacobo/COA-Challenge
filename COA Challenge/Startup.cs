using AutoMapper;
using COA.Core.Interfaces;
using COA.Core.Services;
using COA.Domain.Profiles;
using COA.Infrastructure.Data;
using COA.Infrastructure.Repositories;
using COA.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Text;

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

            // Add SQL Server and Contexts on build.
            services.AddEntityFrameworkSqlServer();
            services.AddDbContextPool<AppDbContext>((services, options) =>
            {
                options.UseInternalServiceProvider(services);
                options.UseSqlServer(Configuration.GetConnectionString("ApplicationConnectionString"));
            });

            // AddAutoMapper and Configurations

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Add DependenyInjections
            services.AddTransient<IUsersServices, UsersServices>();
            services.AddScoped<IUnitOfWork,UOW>();

            // Add Controllers
            services.AddControllers();

            // Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "COA Challenge",
                    Version = "v1",
                    Description = $"Challenge para COA, creado con tecnologia NET Core 5.0 \n" +
                    $"-Se siguio un patron por capas, con un patron de Generic Repository y Unit of Work \n" +
                    $"-Utilizo un modelado de base de datos utilizando EF Core y un patron CodeFirst" +
                    $"-Se agrega tambien inyeccion de dependendias para darle mas claridad de responsabilidad a cada clase \n" +
                    $"-Se utiliza la libreria de Automapper para trabajar los DTOs de manera automatizada." +
                    $"-"

                });
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

            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "text/plain";
                    await context.Response.BodyWriter.WriteAsync(Encoding.ASCII.GetBytes("No se ha podido ejecutar la operacion"));
                });
            });

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
