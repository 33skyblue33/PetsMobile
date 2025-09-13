using Microsoft.EntityFrameworkCore;
using PetsMobile;
using PetsMobile.Data;
using PetsMobile.Repository;
using PetsMobile.Repository.Interface;
using PetsMobile.Services;
using PetsMobile.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<DatabaseContext>(options=>options.UseInMemoryDatabase(connectionString ?? ""));

builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IBreedRepository, BreedRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IBreedService, BreedService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

