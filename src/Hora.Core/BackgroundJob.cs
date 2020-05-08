using Hora.Core.Enums;
using Hora.Core.Queues;
using System;

namespace Hora.Core
{
    public class BackgroundJob
    {
        public static void Enqueue(Action p, JobType jobType = JobType.Memory)
        {
            try
            {
                var job = new Job(p);
                switch (jobType)
                {
                    case JobType.Memory:
                        MemoryQueue.Enqueue(job);
                        break;
                    case JobType.Persistent:
                        break;
                }
            }
            catch (ArgumentNullException e) when (e.ParamName == "job")
            {

                throw;
            }
           
        }
    }
}
