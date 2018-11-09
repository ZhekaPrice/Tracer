using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tracer
{
    public class FileWrite : IWrite
    {
        private string filename;
        public  FileWrite(string Filename)
        {
            filename = Filename;
        }
        public void Write(ISerialize serialize, TraceResult traceResult)
        {
            using (var fileStream = new FileStream(filename, FileMode.Create))
            {
                serialize.Serialize(fileStream, traceResult);
            }
        }
    }

    public class ConsoleWrite : IWrite
    {
        public void Write(ISerialize serialize, TraceResult traceResult)
        {
            using (Stream console = Console.OpenStandardOutput())
            {
                serialize.Serialize(console,traceResult);
            }
        }
    }
}
