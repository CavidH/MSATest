using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;
using Serilog.Events;


#region Logger


Log.Logger = new LoggerConfiguration()
    //.MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.File(
        Path.Combine("Logs", "Log.txt"),
        rollingInterval: RollingInterval.Day,
        fileSizeLimitBytes: 10 * 1024 * 1024,
        retainedFileCountLimit: 30,
        rollOnFileSizeLimit: true,
        shared: true,
        flushToDiskInterval: TimeSpan.FromSeconds(2))
    .WriteTo.Console()
    //.WriteTo.Seq("http://localhost:5341")
    .CreateLogger();

#endregion


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("Ocelot.json");



var authenticationProviderKey = "TestKey";
// Action<JwtBearerOptions> options = o =>
// {
//     o.Authority = "https://whereyouridentityserverlives.com";
//     // etc
// };

builder.Services.AddAuthentication()
    .AddJwtBearer("HalkBank", options =>
    {
        //Token'ı yayınlayan Auth Server adresi bildiriliyor. Yani yetkiyi dağıtan mekanizmanın adresi bildirilerek ilgili API ile ilişkilendiriliyor.
        options.Authority = "https://localhost:1000";
        //Auth Server uygulamasındaki 'Garanti' isimli resource ile bu API ilişkilendiriliyor.
        options.Audience = "HalkBank";
     
    });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.WebHost.UseSerilog();
builder.Services.AddOcelot();
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseOcelot().Wait();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
