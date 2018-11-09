using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public interface IWrite
    {
        void Write(ISerialize serialize, TraceResult result);
    }
}
