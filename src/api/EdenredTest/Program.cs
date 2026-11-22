using Autofac;
using Autofac.Extensions.DependencyInjection;
using EdenredTest.Application.Shared.AutofacModules;
using EdenredTest.Application.Shared.Filters;
using EdenredTest.Extensions;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwagger();
builder.Services.AddCustomConfiguration(builder.Configuration);
builder.Services.AddAutofac();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), Assembly.Load(new AssemblyName("EdenredTest.Application")));

var allowedOrigins =
    builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEnd", policy =>
    {
        policy
            .WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ModuleApplication()));
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ModuleHandler()));
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ModuleMediator()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("FrontEnd");

app.UseAuthorization();

app.MapControllers();

app.Run();
