using Catalog.API.Consumer;
using Catalog.API.Data;
using Catalog.API.Mapper;
using Catalog.API.Repositories;
using Catalog.API.Repositories.Interfaces;
using Common;
using JwtAuthenticationManager.Extension;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<CatalogContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IBindingRepository, BindingRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            builder.Services.AddAutoMapper(typeof(MapperConfig));
            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<RequestBookConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    var uri = new Uri(ServiceBus.Url);
                    cfg.Host(uri, host =>
                    {
                        host.Username(ServiceBus.UserName);
                        host.Password(ServiceBus.Password);
                    });

                    cfg.ReceiveEndpoint(ServiceBus.QueueNames.catalogQueue, c =>
                    {
                        c.ConfigureConsumer<RequestBookConsumer>(context);
                    });

                });
            });
            builder.Services.AddCustomJwtAuthentication();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}