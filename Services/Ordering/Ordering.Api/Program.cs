using Ordering.Api;
using Ordering.Application;
using Ordering.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.MigrateDatabase<OrderDBContext>((context, services) =>
{
    var logger = services.GetService<ILogger<OrderDBContextSeed>>();
    OrderDBContextSeed.SeedAsync(context, logger).Wait();
});

app.Run();
