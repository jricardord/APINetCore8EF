using DataBaseFirst;
using Microsoft.EntityFrameworkCore;
using BusinessRules.Interfaces;
using BusinessRules.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IEmail, Email>();

builder.Services.AddDbContext<EscuelaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("stringConnection")));


var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<EscuelaContext>();
//    context.Database.Migrate();
//}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
