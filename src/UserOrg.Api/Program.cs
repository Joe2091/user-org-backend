using Microsoft.EntityFrameworkCore;
using MediatR;
using UserOrg.Infrastructure.Persistence;
using UserOrg.Infrastructure.Repositories;
using UserOrg.BusinessLogic.Interfaces;
using UserOrg.Api.Middleware;

using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(IUserRepository).Assembly);

// DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
    );
});


var firebaseCredentialsPath = builder.Configuration["Firebase:CredentialsPath"];

if (string.IsNullOrWhiteSpace(firebaseCredentialsPath))
{
    throw new Exception("Firebase credentials path not configured. Check appsettings.Development.json");
}

if (FirebaseApp.DefaultInstance == null)
{
    FirebaseApp.Create(new AppOptions
    {
        Credential = GoogleCredential.FromFile(firebaseCredentialsPath)
    });
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors("Frontend");
app.UseMiddleware<FirebaseAuthMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
