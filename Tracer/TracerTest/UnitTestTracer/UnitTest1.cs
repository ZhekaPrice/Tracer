using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Collections.Generic;

namespace UnitTestTracer
{
    [TestClass]
    public class UnitTest1
    {
        [TestClass]
        public class TracerTests
        {
            private Tracer.Tracer tracer;
            int waittime = 200;

            public void TestPattern()
            {
                tracer.StartTrace();
                Thread.Sleep(waittime);
                tracer.StopTrace();
            }


            [TestMethod]
            public void OneMethodTimeTest()
            {
                tracer = new Tracer.Tracer();
                TestPattern();
                long actualtime = tracer.GetTraceResult().Threads[0].Time;
                long expected = waittime;
                Assert.IsTrue(actualtime >= expected);
            }


            [TestMethod]
            public void OneInAnotherTimeTest()
            {
                tracer = new Tracer.Tracer();
                tracer.StartTrace();
                Thread.Sleep(waittime);
                TestPattern();
                tracer.StopTrace();
                long actualtime = tracer.GetTraceResult().Threads[0].Time;
                long expected = waittime * 2;
                Assert.IsTrue(actualtime >= expected);
            }


            [TestMethod]
            public void MultiThreadTimeTest()
            {
                tracer = new Tracer.Tracer();
                var threadList = new List<Thread>();
                for (int i = 0; i < 3; i++)
                {
                    Thread thread = new Thread(TestPattern);
                    threadList.Add(thread);
                    thread.Start();
                }
                foreach (Thread thread in threadList)
                {
                    thread.Join();
                }
                long actualtime = 0;
                for (int i = 0; i < 3; i++)
                {
                    actualtime += tracer.GetTraceResult().Threads[i].Time;
                }
                long expected = waittime * 3;
                Assert.IsTrue(actualtime >= expected);
            }


            [TestMethod]
            public void MultiThreadTest()
            {
                tracer = new Tracer.Tracer();
                var threadList = new List<Thread>();
                for (int i = 0; i < 4; i++)
                {
                    Thread thread = new Thread(TestPattern);
                    threadList.Add(thread);
                    thread.Start();
                }
                foreach (Thread thread in threadList)
                {
                    thread.Join();
                }
                Assert.AreEqual(tracer.GetTraceResult().Threads.Count, 4);
                for (int i = 0; i < 4; i++)
                {
                    Assert.AreEqual(tracer.GetTraceResult().Threads[i].InsideMethods.Count, 1);
                }
            }


            [TestMethod]
            public void OneInAnotherTest()
            {
                tracer = new Tracer.Tracer();
                tracer.StartTrace();
                Thread.Sleep(waittime);
                TestPattern();
                tracer.StopTrace();
                Assert.AreEqual(tracer.GetTraceResult().Threads.Count, 1);
                Assert.AreEqual(tracer.GetTraceResult().Threads[0].InsideMethods.Count, 1);
                Assert.AreEqual(tracer.GetTraceResult().Threads[0].InsideMethods[0].InsideMethods.Count, 1);
                Assert.AreEqual(tracer.GetTraceResult().Threads[0].InsideMethods[0].Name, "OneInAnotherTest");
                Assert.AreEqual(tracer.GetTraceResult().Threads[0].InsideMethods[0].ClassName, "TracerTests");
                Assert.AreEqual(tracer.GetTraceResult().Threads[0].InsideMethods[0].InsideMethods[0].Name, "TestPattern");
                Assert.AreEqual(tracer.GetTraceResult().Threads[0].InsideMethods[0].InsideMethods[0].ClassName, "TracerTests");
            }
        }
    }
}
