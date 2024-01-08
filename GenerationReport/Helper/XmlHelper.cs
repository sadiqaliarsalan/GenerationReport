using System.Xml.Serialization;

namespace GenerationReport.Helper
{
    public static class XmlHelper
    {
        public static T DeserializeXml<T>(string filePath)
        {
            using var reader = new StreamReader(filePath);
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(reader);
        }

        public static void SerializeXml<T>(T obj, string filePath)
        {
            var serializer = new XmlSerializer(typeof(T));
            using var writer = new StreamWriter(filePath);
            serializer.Serialize(writer, obj);
        }
    }
}
