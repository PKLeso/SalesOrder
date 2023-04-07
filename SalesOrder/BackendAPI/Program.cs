using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using SalesOrder.Data;
using SalesOrder.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add Authentication
builder.Services.AddAuth(builder);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//SignalR
//builder.Services.AddSignalR(options =>
//{
//    options.EnableDetailedErrors = true;
//});
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddSignalR(); //.AddAzureSignalR();

builder.Services.AddDbContext<SalesOrderDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString"));
});

// Enable CORS
builder.Services.AddCorsPolicy();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("�nableCorsForAngularApp");

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
