using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private TraceResult traceResult;

        public  Tracer()
        {
            traceResult = new TraceResult();
        }


        public void StartTrace()
        {
            MethodBase baseMethod = new StackTrace().GetFrame(1).GetMethod();
            var method = new MethodInfo
            {
                ClassName = baseMethod.ReflectedType.Name,
                Name = baseMethod.Name
            };
            traceResult.GetThread(Thread.CurrentThread.ManagedThreadId).StartMethodTracing(method);

        }

        public void StopTrace()
        {
            traceResult.GetThread(Thread.CurrentThread.ManagedThreadId).StopMethodTracing();
        }

        public TraceResult GetTraceResult()
        {
            return traceResult;
        }
    }
}
