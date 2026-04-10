using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PeliculasAPI;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<AplicationDbContext>(opciones =>
   opciones.UseSqlServer("name=DefaultConnection"));


builder.Services.AddOutputCache(opciones => {
    opciones.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(60);
});

var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos")!.Split(",");
builder.Services.AddCors(opciones => {

    opciones.AddDefaultPolicy(opcionesCORS =>
    opcionesCORS.WithOrigins(origenesPermitidos).AllowAnyMethod().AllowAnyHeader()
    .WithExposedHeaders("cantidad-total-registros"));
}
    );

var app = builder.Build();

// Configure the HTTP request pipeline.



app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors();


app.UseOutputCache();

app.UseAuthorization();


app.MapControllers();


app.Run();
