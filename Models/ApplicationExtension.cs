using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.MSSqlServer;

namespace Dot_Net_Core_Tutorial.Models
{
    public static class ApplicationExtension
    {
        public static void ConfigureSeriLog(this IHostBuilder host)
        {
            host.UseSerilog((ctx,lc) =>
            {
                lc.WriteTo.Console();
                lc.WriteTo.File(new JsonFormatter(), "log.txt");
                lc.WriteTo.MSSqlServer("Server=DESKTOP-5TS63KH;Database=LoggingDb;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;",
                    new MSSqlServerSinkOptions
                    {
                        TableName = "Logs",
                        SchemaName = "dbo",
                        AutoCreateSqlDatabase = true,
                        AutoCreateSqlTable = true,
                    });
            });
        }
    }
}
