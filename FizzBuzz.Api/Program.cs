using FizzBuzz.Services;
using FizzBuzz.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<FizzBuzzConverterService>();
builder.Services.AddScoped<InputValidator>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
