using Hora.Core.Enums;
using System.Diagnostics;
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
            return Task.CompletedTask;

        }

        private Task SubJobs(string text)
        {
            Debug.WriteLine(text);
            return Task.CompletedTask;
        }
    }
}
