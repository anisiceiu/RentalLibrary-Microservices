using Common;
using MassTransit;
using Member.API.Data;
using Member.API.Mapper;
using Member.API.Repositories;
using Member.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Member.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<MemberContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IMemberRepository, MemberRepository>();

            builder.Services.AddAutoMapper(typeof(MapperConfig));

            builder.Services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    var uri = new Uri(ServiceBus.Url);
                    cfg.Host(uri, host =>
                    {
                        host.Username(ServiceBus.UserName);
                        host.Password(ServiceBus.Password);
                    });
                });
            });


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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}