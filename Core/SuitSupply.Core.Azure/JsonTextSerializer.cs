﻿using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;

namespace SuitSupply.Core.Azure
{
    public class JsonTextSerializer
    {
        private readonly JsonSerializer _serializer;

        public JsonTextSerializer()
            : this(JsonSerializer.Create(new JsonSerializerSettings
            {
                // Allows deserializing to the actual runtime type
                TypeNameHandling = TypeNameHandling.All,
                // In a version resilient way
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
            }))
        {
        }

        public JsonTextSerializer(JsonSerializer serializer)
        {
            _serializer = serializer;
        }

        public void Serialize(TextWriter writer, object graph)
        {
            var jsonWriter = new JsonTextWriter(writer);
            jsonWriter.Formatting = Formatting.Indented;

            _serializer.Serialize(jsonWriter, graph);

            // We don't close the stream as it's owned by the message.
            writer.Flush();
        }

        public object Deserialize(TextReader reader)
        {
            var jsonReader = new JsonTextReader(reader);

            try
            {
                return _serializer.Deserialize(jsonReader);
            }
            catch (JsonSerializationException e)
            {
                // Wrap in a standard .NET exception.
                throw new SerializationException(e.Message, e);
            }
        }
    }
}