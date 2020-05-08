using Hora.Core.Enums;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Hora.Core.Queues
{
    public class MemoryQueue
    {
        private bool _queueProcessing;
        private static List<Job> _memoryJobQueue = new List<Job>();
        internal void Enqueue(Job job)
        {
            _memoryJobQueue.Add(job);
            ProcessQueue();
        }

        private void ProcessQueue()
        {
            if (_queueProcessing) return;
            _queueProcessing = true;

            for (int i = 0; i < _memoryJobQueue.Count; i++)
            {
                var job = _memoryJobQueue[i];

                try
                {
                    job.State = JobState.Running;
                    job.MethodCall();

                    //if completed successfully remove
                    job.State = JobState.Completed;
                    _memoryJobQueue.Remove(job);
                }
                catch (Exception e)
                {
                    job.FailureMessage = e.Message;
                    job.State = JobState.Failed;

                    // remove children tasks
                    _memoryJobQueue.RemoveAll(j => j.CallerName == job.MethodCall.GetMethodInfo().Name);

                    // if retries is RetryCount != retries, requeue
                    if(job.Retries < job.RetryCount)
                    {
                        job.State = JobState.Requeued;
                        job.Retries++;
                        _memoryJobQueue.Insert(0, job);
                    }
                }

                // has i reached end?
                if (i >= _memoryJobQueue.Count && _memoryJobQueue.Count > 0) i = -1;
            }
        }
    }
}
