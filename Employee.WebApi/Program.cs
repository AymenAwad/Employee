using Abp.Domain.Repositories;
using Application;
using Microsoft.Extensions.Configuration;
using infrastructure;
using Infrastructure.Extension;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.FeatureManagement;
using Persistence.Contexts;
using Shared.Middleware;
using Application.Interfaces;
using Application.Interface.Services;
using System.Reflection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.ConfigureServicesForIdentity();
builder.Services.AddSingleton<RequestResponseLoggingMiddleware>();

builder.Services.ConfigureCors();

builder.Services.AddController();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{

    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
          , b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
});

//builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddIdentityService(builder.Configuration);
//builder.Services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });
builder.Services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMailSetting(builder.Configuration);
builder.Services.AddScopedServices();
builder.Services.AddTransientServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddSwaggerOpenAPI();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddVersion();
builder.Services.AddFeatureManagement();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestApiJWT v1"));
}

app.UseCors(options =>
     options.AllowAnyOrigin()
     .AllowAnyHeader()
     .AllowAnyMethod()
     );

app.ConfigureCustomExceptionMiddleware();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
//app.ConfigureSwagger();
//app.ConfigureSwagger();

app.UseHttpsRedirection();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.MapControllers();

app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.Run();
