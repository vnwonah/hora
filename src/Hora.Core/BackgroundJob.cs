using Hora.Core.Enums;
using Hora.Core.Queues;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Hora.Core
{
    public class BackgroundJob
    {
        private static MemoryQueue _memoryQueue;
        public static void Enqueue(Action p, JobType jobType = JobType.Memory, 
            int retryCount = 0, [CallerMemberName] string callerName = null)
        {
            try
            {
                var job = new Job(p, callerName, retryCount);

                switch (jobType)
                {
                    case JobType.Memory:
                        EnqueueMemoryJob(job);
                        break;
                }
            }
            catch (ArgumentNullException e)
            {
                throw;
            }
           
        }

        private static void EnqueueMemoryJob(Job job)
        {
            if (_memoryQueue is null)
                _memoryQueue = new MemoryQueue();
            Task.Run(() => _memoryQueue.Enqueue(job));
        }
    }
}
