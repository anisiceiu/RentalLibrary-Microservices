using Common;
using Identity.Consumer;
using Identity.Data;
using Identity.Repositories;
using Identity.Repositories.Interfaces;
using JwtAuthenticationManager;
using JwtAuthenticationManager.Extension;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddSingleton<JwtTokenHandler>();

            builder.Services.AddCustomJwtAuthentication();
            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<MemberCreatedConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    var uri = new Uri(ServiceBus.Url);
                    cfg.Host(uri, host =>
                    {
                        host.Username(ServiceBus.UserName);
                        host.Password(ServiceBus.Password);
                    });

                    cfg.ReceiveEndpoint(ServiceBus.QueueNames.identityQueue, c =>
                    {
                        c.ConfigureConsumer<MemberCreatedConsumer>(context);
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

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}