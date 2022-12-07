using DAL_Lascaux_Demo.Data;
using Lascaux_Demo_Evan;
using Microsoft.EntityFrameworkCore;
using BAL_Lascaux_Demo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CinemaDbContext>(options => options.UseInMemoryDatabase("mydb"));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<TimeSlotService, TimeSlotService>();
builder.Services.AddScoped<MovieService, MovieService>();
builder.Services.AddScoped<CinemaRoomService, CinemaRoomService>();

var app = builder.Build();

//One time filling of the context
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<CinemaDbContext>();

await context.fillDb();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .SetIsOriginAllowed(origin => true) // allow any origin
                /*.AllowCredentials()*/); // allow credentials

app.UseAuthorization();

app.MapControllers();

app.Run();
