using Unab.Practice.Employees.Persistence;
using Unab.Practice.Employees.UseCases;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
.AddJsonOptions(opts =>
{
    var enumConverter = new JsonStringEnumConverter();
    opts.JsonSerializerOptions.Converters.Add(enumConverter);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//servicios de capas
builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices();

const string myPolicy = "policyEmployee";

builder.Services.AddCors(options =>
  options.AddPolicy(myPolicy, builder =>
  {
      builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
  })
 );

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseCors("policyEmployee");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();