using Microsoft.Extensions.Logging;

using MassiveJobs.Core;

namespace Chirper.Server.Jobs
{
    public class TestPeriodicJob: Job<TestPeriodicJob>
    {
        private readonly ILogger<TestPeriodicJob> _logger;

        public TestPeriodicJob(ILogger<TestPeriodicJob> logger)
        {
            _logger = logger;
        }

        public override void Perform()
        {
            _logger.LogWarning("Test periodic job performed.");
        }
    }
}
