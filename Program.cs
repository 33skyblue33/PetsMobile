using Microsoft.EntityFrameworkCore;
using PetsMobile;
using PetsMobile.Data;
using PetsMobile.Repository;
using PetsMobile.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<DatabaseContext>(options=>options.UseSqlite(connectionString));
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllers();
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

