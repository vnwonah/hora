using Hora.Core.Enums;
using System;

namespace Hora.Core
{
    internal class Job
    {
        public string Id { get; }
        public Action MethodCall { get;  }
        public JobState State { get; set; }
        public string FailureMessage { get; set; }
        public string CallerName { get; }
        public int RetryCount { get;  }
        public int Retries { get; set; }
        public Job(Action methodCall, string callerName, int retryCount = 0)
        {
            Id = Guid.NewGuid().ToString();
            MethodCall = methodCall;
            State = JobState.Queued;
            CallerName = callerName;
            RetryCount = retryCount;
        }
    }
}
