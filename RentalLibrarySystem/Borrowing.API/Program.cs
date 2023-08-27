using Borrowing.API.Data;
using Borrowing.API.Repositories;
using Borrowing.API.Mapper;
using Common;
using JwtAuthenticationManager.Extension;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Borrowing.API.Consumer;

namespace Borrowing.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<BorrowContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            builder.Services.AddScoped<IBorrowRepository,BorrowRepository>();
            builder.Services.AddAutoMapper(typeof(MapperConfig));

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

                    cfg.ReceiveEndpoint(ServiceBus.QueueNames.borrowingQueue, c =>
                    {
                        c.ConfigureConsumer<MemberCreatedConsumer>(context);
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


            app.MapControllers();

            app.Run();
        }
    }
}