using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Tracer;

namespace TracerMethod
{
    internal class UnitTestMethod
    {
        private ITracer Tracer;

        internal UnitTestMethod(ITracer tracer)
        {
            Tracer = tracer;
        }

        internal void EasyMethod()
        {
            Tracer.StartTrace();
            Thread.Sleep(new Random().Next(10, 500));
            Tracer.StopTrace();
        }

        internal void NotEasyMethod()
        {
            Tracer.StartTrace();
            Thread.Sleep(new Random().Next(10, 500));
            EasyMethod();
            Tracer.StopTrace();
        }

        internal void HardMethod()
        {
            Tracer.StartTrace();
            Thread.Sleep(new Random().Next(10, 500));
            NotEasyMethod();
            var threadList = new List<Thread>();
            threadList.Add(new Thread(EasyMethod));
            threadList.Add(new Thread(NotEasyMethod));
            foreach (Thread thread in threadList)
            {
                thread.Start();
            }
            foreach (Thread thread in threadList)
            {
                thread.Join();
            }

            Tracer.StopTrace();
        }
    }

    class Program
    {
        private static Tracer.Tracer tracer;

        static void Main(string[] args)
        {
            tracer = new Tracer.Tracer();
            var test = new UnitTestMethod(tracer);
            test.HardMethod();
            new ConsoleWrite().Write(new XmlSerialize(), tracer.GetTraceResult());
            new ConsoleWrite().Write(new JSONSerialize(), tracer.GetTraceResult());
            new FileWrite("D:\\Учеба\\лабы\\5 сем\\СПП\\1 лаба\\Result1.txt").Write(new XmlSerialize(), tracer.GetTraceResult());
            new FileWrite("D:\\Учеба\\лабы\\5 сем\\СПП\\1 лаба\\Result.txt").Write(new JSONSerialize(), tracer.GetTraceResult());
            Console.ReadKey();
        }
    }
}
