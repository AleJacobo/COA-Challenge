using AutoMapper;
using COA.Core.Interfaces;
using COA.Core.Services;
using COA.Domain.Common;
using COA.Domain.Exceptions;
using COA.Domain.Profiles;
using COA.Infrastructure.Data;
using COA.Infrastructure.Repositories;
using COA.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
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
            services.AddScoped<IUnitOfWork, UOW>();

            // Add Controllers
            services.AddControllers();

            // Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "COA Challenge",
                    Version = "v1",
                    Description = $"Challenge para COA, creado con tecnologia NET Core 5.0 \n " +
                    $"\n -Se siguio un patron por capas, con un patron de Generic Repository y Unit of Work \n" +
                    $"\n -Utilizo un modelado de base de datos utilizando EF Core y un patron CodeFirst \n " +
                    $"\n -Se agrega tambien inyeccion de dependendias para darle mas claridad de responsabilidad a cada clase \n" +
                    $"\n -Se utiliza la libreria de Automapper para trabajar los DTOs de manera automatizada \n" +
                    $"\n -Se crea e implementa un middleware de Exception Handler, para poder liberar performance en las llamadas \n" +
                    $"\n -Se anexa tambien un UnitTest, con tecnologia xUnit para verificar endpoints",
                    Contact = new OpenApiContact()
                    {
                        Name = "Alejandro Jacobo Guerrero",
                        Url = new Uri("https://github.com/AleJacobo"),
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.IncludeXmlComments(xmlPath);
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
                    var response = new Result();
                    var ex = context.Features.Get<IExceptionHandlerPathFeature>().Error;
                    if (ex.GetType() == typeof(COAException))
                    {
                        await response.Fail(ex.Message);
                    }
                    else
                    {
                        await response.Fail("No se ha podido ejecutar la operacion");
                    }
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    await context.Response.BodyWriter.WriteAsync(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(response)));
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
