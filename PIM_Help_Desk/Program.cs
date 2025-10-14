using Microsoft.EntityFrameworkCore;
using OpenAI;
using Pim.Helpdesk;
using Pim.Helpdesk.Infrastructure.Context;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

Bootstrapper.RegisterServices(builder.Services);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddHttpClient();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference("/scalar/v1");

}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
