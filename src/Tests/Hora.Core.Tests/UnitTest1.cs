using Hora.Core.Queues;
using System.Diagnostics;
using Xunit;

namespace Hora.Core.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            MemoryQueue.Enqueue(() => WriteToDebug("hello world"));
        }

        public static int WriteToDebug(string name)
        {
            Debug.WriteLine(name);
            return 0;
        }
    }
}
