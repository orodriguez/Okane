using Okane.Core;
using Okane.Storage.EntityFramework;
using Okane.Storage.InMemory;
using Okane.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// App dependencies
builder.Services.AddOkaneCore(builder.Configuration);
builder.Services.AddOkaneWebApi(builder.Configuration);

if (builder.Configuration["Storage"] == "EF")
    builder.Services.AddOkaneEntityFrameworkStorage();
else
    builder.Services.AddOkaneInMemoryStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();