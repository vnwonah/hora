using System;
using System.Collections.Generic;

namespace Hora.Core.Queues
{
    public static class MemoryQueue
    {
        private static List<Job> _memoryJobQueue = new List<Job>();
        internal static void Enqueue(Job job)
        {
            job.MethodCall();
            _memoryJobQueue.Add(job);
        }
    }
}
