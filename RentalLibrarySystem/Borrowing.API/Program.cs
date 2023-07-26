using Borrowing.API.Data;
using Borrowing.API.Repositories;
using Catalog.API.Mapper;
using JwtAuthenticationManager.Extension;
using Microsoft.EntityFrameworkCore;

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