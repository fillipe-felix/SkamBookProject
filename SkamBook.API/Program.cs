using System.Text.Json.Serialization;

using FluentValidation.AspNetCore;

using SkamBook.API.Configurations;
using SkamBook.API.Filters;
using SkamBook.Application;
using SkamBook.Application.Validators;
using SkamBook.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfigurationsApi();


builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilters)))
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterUserCommandValidator>())
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddInfrastructure(builder);
builder.Services.AddApplication();
    



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

