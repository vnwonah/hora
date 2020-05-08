using Hora.Core.Enums;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Hora.Core.Tests
{
    public class BackgroundJobShould
    {
        [Fact]
        public void EnqueueMemoryJob()
        {
            BackgroundJob.Enqueue(() => WriteToDebug("hello world"), JobType.Memory);
        }

        [Fact]
        public void EnqueueJobFromRunningJob()
        {
            BackgroundJob.Enqueue(() => FirstJob());
            Debug.WriteLine("returned");
        }

        [Fact]
        public void EnqueueNewMemoryJobsOnSameThread()
        {
            BackgroundJob.Enqueue(() => ShowThreadInfo("Task 1"));
            BackgroundJob.Enqueue(() => ShowThreadInfo("Task 2"));
        }

        private Task ShowThreadInfo(string s)
        {
            
            Debug.WriteLine("{0} Thread ID: {1}",
                              s, Thread.CurrentThread.ManagedThreadId);
            return Task.CompletedTask;
        }

        private Task WriteToDebug(string name)
        {
            Debug.WriteLine(name);
            return Task.CompletedTask;
        }

        private Task FirstJob()
        {
            Debug.WriteLine("Executed First Job");
            BackgroundJob.Enqueue(() => SubJobs("Subjob 1"));
            BackgroundJob.Enqueue(() => SubJobs("SubJob 2"));
            throw new Exception();

        }

        private Task SubJobs(string text)
        {
            Debug.WriteLine(text);
            return Task.CompletedTask;
        }
    }
}
