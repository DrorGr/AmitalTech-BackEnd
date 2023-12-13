using AmitalBE.Context;
using AmitalBE.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MainDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(options => { options.AddPolicy("AllowSpecificOrigin", builder => builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod()); });
builder.Services.AddTransient<MainLogic>();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowSpecificOrigin");
if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }

app.Run();




