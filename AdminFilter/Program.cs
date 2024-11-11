

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
namespace AdminFilter{

    public class Program{

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        
var builder = WebApplication.CreateBuilder(args);


// Add session support
builder.Services.AddDistributedMemoryCache();  // In-memory session storage
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;  // Required for GDPR compliance
});
         
        builder.Services.AddControllers();
      
        
var app = builder.Build();


  
        app.MapControllers();
        app.UseSession();
    

app.MapGet("/", () => "Hello World!");

app.Run("http://localhost:5222");
        }
    }
}