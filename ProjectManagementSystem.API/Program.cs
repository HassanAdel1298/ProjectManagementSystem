
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Application;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Application.Profiles;
using ProjectManagementSystem.Entity.Data;
using System.Diagnostics;


namespace ProjectManagementSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<Context>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("Default")).
                LogTo(log => Debug.WriteLine(log), LogLevel.Information).
                EnableServiceProviderCaching();

            });

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
                builder.RegisterModule(new AutoFacModule()));

            builder.Services.AddAutoMapper(typeof(ProjectProfile));



            var app = builder.Build();

            MapperHelper.Mapper = app.Services.GetService<IMapper>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
