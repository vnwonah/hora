using Hora.Core.Enums;
using System;

namespace Hora.Core
{
    internal class Job
    {
        public Guid Id { get; }
        public Action MethodCall { get;  }
        public JobState State { get; set; }
        public Job(Action methodCall)
        {
            Id = Guid.NewGuid();
            MethodCall = methodCall;
            State = JobState.Queued;
        }
    }
}
