using Order.API.Validation;
using Order.Application;
using Order.Persistance;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOrderApplication();
builder.Services.AddOrderPersistance(builder.Configuration);

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
