using Microsoft.EntityFrameworkCore;
using VKTestTask.Domain;
using VKTestTask.Extensions;
using VKTestTask.Middlewares;

namespace VKTestTask;

public class Program
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseNpgsql(connectionString);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            //.UseLoggerFactory(new NullLoggerFactory()) for logging disable
        });

        services.AddControllers();
        services.AddSwaggerGen();
        services.AddEndpointsApiExplorer();

        services.AddDomain();
        services.AddHelpers();
    }

    public static void ConfigureApplication(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        
        app.UseMiddleware<ErrorsHandlerMiddleware>();
        app.UseMiddleware<BasicAuthMiddleware>();


        app.MapControllers();

        using var scope = app.Services.CreateScope();
        scope.ServiceProvider.GetService<ApplicationContext>();
    }

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();

        ConfigureApplication(app);

        app.Run();
    }
}