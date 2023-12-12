using Npgsql;

namespace Discount.Grpc.Extensions;

public static class HostExtensions
{
    public static IHost MigrateDatabase<TContext>(this IHost host, int retry = 0)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var configuration = services.GetRequiredService<IConfiguration>();
        var logger = services.GetRequiredService<ILogger<TContext>>();

        try
        {
            logger.LogInformation("migrating postgresql database");

            using var connection = new NpgsqlConnection
                (configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            connection.Open();

            using var command = new NpgsqlCommand
            {
                Connection = connection
            };

            command.CommandText = @"create table if not exists Coupon 
                (Id serial primary key,
                ProductName varchar(200) not null,
                Description text,
                Amount int)";
            command.ExecuteNonQuery();

            /*command.CommandText = @"insert into Coupon(ProductName, Description, Amount) values 
                ('IPhone X', 'iphone discount', 150),
                ('Samsung 10', 'samsung discount', 150)";
            command.ExecuteNonQuery();*/

            logger.LogInformation("migration has been completed!");
        }
        catch (Exception ex)
        {
            logger.LogError($"an error has been occured: {ex.Message}");

            if (retry < 50)
            {
                Thread.Sleep(2000);
                host.MigrateDatabase<TContext>(retry + 1);
            }
        }

        return host;
    }
}
