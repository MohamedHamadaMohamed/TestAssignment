
using TestAssignment.Repository.IRepository;
using TestAssignment.Repository;
using TestAssignment.Services;

namespace TestAssignment
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


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            builder.Services.AddHostedService<TemporalBlockService>();
            builder.Services.AddHttpClient<IPGeolocationService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy =>
                    {
                        policy.AllowAnyOrigin()   
                              .AllowAnyMethod()   
                              .AllowAnyHeader();  
                    });
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

             app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthorization();



            app.MapControllers();

            app.Run();
        }
    }
}
