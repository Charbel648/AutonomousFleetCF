using Fleet.API.Validation;
using Fleet.Application;
using Fleet.Persistance;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFleetApplication();
builder.Services.AddFleetPersistance(builder.Configuration);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();

public partial class Program
{
}
