using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using Rise.Assessment.Phonebook.API.BackgroundServices;
using Rise.Assessment.Phonebook.Application.Handlers;
using Rise.Assessment.Phonebook.Infrastructure;
using System;

namespace Rise.Assessment.Phonebook.API
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
            #region RabbitMQ
            services.AddSingleton(service => new ConnectionFactory()
            {
                Uri = new Uri(Configuration.GetConnectionString("RabbitMQ")),
                DispatchConsumersAsync = true
            });
            services.AddSingleton<Services.RabbitMQClientService>();
            #endregion

            services.AddDbContext<PhonebookDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    configure => configure.MigrationsAssembly("Rise.Assessment.Phonebook.Infrastructure"));
            });

            services.AddMediatR(typeof(CreatePersonCommandHandler).Assembly);
            services.AddMediatR(typeof(CreatePersonDetailCommandHandler).Assembly);
            services.AddMediatR(typeof(DeletePersonCommandHandler).Assembly);
            services.AddMediatR(typeof(DeletePersonDetailCommandHandler).Assembly);
            services.AddMediatR(typeof(GetReportQueryHandler).Assembly);

            services.AddHostedService<ExcelReportBackgroundService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rise.Assessment.Phonebook.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rise.Assessment.Phonebook.API v1"));
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
