using System;
using System.Threading.Tasks;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Hangfire.HangfireJobs", Version = "1.0")]

namespace Hangfire.Tests.Api.HangfireJobs
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class Delayed
    {
        [IntentManaged(Mode.Merge)]
        public Delayed()
        {
        }

        [AutomaticRetry(Attempts = 10, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public async Task ExecuteAsync()
        {
            // TODO: Implement job functionality
            throw new NotImplementedException("Your implementation here...");
        }
    }
}