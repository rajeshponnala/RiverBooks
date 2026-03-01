using System.Reflection;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using RiverBooks.Books;
using RiverBooks.Users;
using Serilog;

var logger = Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

logger.Information("Starting web host");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Map Module Services

builder.Services.AddFastEndpoints()
  .AddAuthenticationJwtBearer(s => { 
      s.SigningKey = builder.Configuration["Auth:JwtSecret"]!;
  })
  .AddAuthorization()
  .SwaggerDocument();

List<Assembly> mediatRAssemblies = [typeof(Program).Assembly];
builder.Services.AddBookServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddUserModuleServices(builder.Configuration, logger, mediatRAssemblies);

// MediatR
builder.Services.AddMediatR(cfg =>
  cfg.RegisterServicesFromAssemblies([.. mediatRAssemblies])
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication().UseAuthorization(); 


app.UseFastEndpoints().UseSwaggerGen();

app.Run();

public partial class Program { }
