using System.Text.Json.Serialization;

using SkamBook.API.Configurations;
using SkamBook.API.Filters;
using SkamBook.Application;
using SkamBook.Application.Chat;
using SkamBook.Infrastructure;
using SkamBook.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfigurationsApi();


builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilters)))
    //.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterUserCommandValidator>())
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.AllowTrailingCommas = true;
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.Configure<SendGridSettings>(builder.Configuration.GetSection(nameof(SendGridSettings)));
builder.Services.Configure<GoogleApiSettings>(builder.Configuration.GetSection(nameof(GoogleApiSettings)));
builder.Services.AddInfrastructure(builder);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});
    



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseRouting();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chatHub");
    endpoints.MapControllers();
});

app.Run();

