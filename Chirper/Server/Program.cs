using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using MassiveJobs.Core.Hosting;
using Chirper.Server.Jobs;

namespace Chirper.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .InitMassiveJobs(PublishPeriodicJobs)
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void PublishPeriodicJobs()
        {
            TestPeriodicJob.CancelPeriodic("test");
        }
    }
}
