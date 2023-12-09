using Microsoft.EntityFrameworkCore;
using MyUser.Models;

namespace MyUser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<UserContext>(options =>
       options.UseSqlServer(UserContext.ConnectionString));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            var context = new UserContext();
            context.Database.Migrate();

            if (!context.Users.Any())
            {
                var users = new List<User>()
                {
                    new User(){ FirstName = "Jahonger", LastName = "Ahmedov" },
                    new User(){ FirstName = "Jake", LastName = "Esh" },
                    new User(){ FirstName = "Rasul", LastName = "Azimov" },
                };
                context.Users.AddRange(users);

                context.SaveChanges();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}