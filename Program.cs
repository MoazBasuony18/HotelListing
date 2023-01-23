using Serilog;
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(path: "G:\\Web Elgendy\\Api\\WebApplication1\\logs\\log-.txt",
    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
    rollingInterval: RollingInterval.Day,
    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information).CreateLogger();
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(a =>
{
    a.AddPolicy("allowAll", newBuilder =>
    newBuilder.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog();
try
{
    Log.Information("Application is starting");
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseCors("allowAll");
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception)
{
    Log.Fatal("Application is failed to start");
}
finally
{
    Log.CloseAndFlush();
}

