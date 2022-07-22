using Serilog;

namespace Assist.July._2022.BE2.Application.Services
{
    public class StaticLogger
    {
        public static void EnsureInitialized()
        {
            if (Log.Logger is not Serilog.Core.Logger)
            {
                Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .CreateLogger();
            }
        }
    }
}
