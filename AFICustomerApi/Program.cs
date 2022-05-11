using AFICustomerApi.Model;
using AFICustomerApi.Utilities;
using AFICustomerApi.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.CommandLine;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var jsonConfigurationSource = new JsonConfigurationSource
{
    Path = "appsettings.json",
    ReloadOnChange = true,
    Optional = false
};

var commandArgs = new CommandLineConfigurationSource
{
    Args = args
};

var configuration = new ConfigurationBuilder().Add(jsonConfigurationSource).Add(commandArgs).Build();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CustomerContext>(
    options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IDateTimeService, DateTimeService>();
builder.Services.AddScoped<IValidator<Customer>, CustomerValidator>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Customer API",
        Version = "v1",
        Description = "An API to perform Customer operations",
        Contact = new OpenApiContact
        {
            Name = "Hugh Ashby-Hayter",
            Email = "h.ashbyhayter@gmail.com",
        },
    });
    var xmlFile = "AFICustomerApi.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

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
