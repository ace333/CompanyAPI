using CompanyAPI.Application.Core;
using CompanyAPI.Application.Validation;
using CompanyAPI.Infrastructure.Context;
using CompanyAPI.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationLayerBase).GetTypeInfo().Assembly));
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IIsinValidator, IsinValidator>();

builder.Services.AddValidatorsFromAssemblyContaining<CompanyCommandBaseValidator>();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

if(builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
}

// sql database
var connectionString = builder.Configuration.GetConnectionString("Sql");
builder.Services.AddDbContext<CompanyDbContext>(options => options.UseSqlServer(connectionString, 
    sqlServerOptions =>
    {
        sqlServerOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(CompanyDbContext))?.FullName);
        sqlServerOptions.EnableRetryOnFailure(2);
    }));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Version = "v1",
        Title = "CompanyAPI"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// run migrations if condition met
if (app.Configuration.GetValue<bool>("RunEFMigrations"))
{
    var dbContext = app.Services.GetRequiredService<CompanyDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CompanyAPI V1");
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();

app.MapControllers();

app.Run();