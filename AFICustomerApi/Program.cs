using AFICustomerApi.Model;
using AFICustomerApi.Utilities;
using AFICustomerApi.Validation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Customer;Trusted_Connection=True;"));
builder.Services.AddScoped<IDateTimeService, DateTimeService>();
builder.Services.AddScoped<IValidator<Customer>, CustomerValidator>();

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
