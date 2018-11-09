using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;


namespace Tracer
{
    public class JSONSerialize : ISerialize
    {
        public void Serialize(Stream stream, TraceResult traceResult)
        {
            var jsonFormat = new DataContractJsonSerializer(typeof(TraceResult));

            using (var jsonWriter = JsonReaderWriterFactory.CreateJsonWriter(stream, Encoding.UTF8, true, true))
            {
                jsonFormat.WriteObject(jsonWriter, traceResult);
            }
        }
    }

    public class XmlSerialize : ISerialize
    {
        public void Serialize(Stream stream, TraceResult traceResult)
        {
            var xmlFormat = new DataContractSerializer(typeof(TraceResult));
            var setting = new XmlWriterSettings();
            setting.Indent = true;
            using (var xmlWriter = XmlWriter.Create(stream, setting))
            {
                xmlFormat.WriteObject(xmlWriter, traceResult);
            }

        }
    }
}
