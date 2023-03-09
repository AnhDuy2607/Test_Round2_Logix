using BE.Authentication.Register;
using BE.Core.Register;
using BE.Infrastructure.DataContext.Register;
using BE.Repository.Register;
using BE.UnitOfWork.Register;
using BE.Utilities;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.RegisterRepository();
builder.Services.RegisterUnitOfWork();
builder.Services.RegisterCoreServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

string connectionString = builder.Configuration.GetConnectionString("BE.MovieDb");
builder.Services.RegisterMovieDataContext(connectionString);
builder.Services.RegisterAuthentication();
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true).AllowCredentials());
app.MapControllers();

app.Run();
