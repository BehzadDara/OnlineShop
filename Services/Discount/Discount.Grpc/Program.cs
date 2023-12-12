using Discount.Grpc.Extensions;
using Discount.Grpc.Repositories;
using Discount.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ICouponRepository, CouponRepository>();

var app = builder.Build();

app.MapGrpcService<DiscountService>();

app.MigrateDatabase<Program>();

app.Run();
