namespace Trucks.Utilities
{
    using System.Text;
    using System.Xml.Serialization;


    public class XmlHelper
    {
        public T Deserialize<T>(string inputXml, string rootName)
        {
            //Deserialize from XML
            XmlRootAttribute xmlRoot = new XmlRootAttribute(rootName);
            //Serialize and deserialize        type data to serialize, XmlRootAttribute
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), xmlRoot);
            StringReader reader = new StringReader(inputXml);
            T supplierDtos = (T)xmlSerializer.Deserialize(reader);
            return supplierDtos;
        }

        public IEnumerable<T> DeserializeCollection<T>(string inputXml, string rootName)
        {
            XmlRootAttribute xmlRoot = new XmlRootAttribute(rootName);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T[]), xmlRoot);
            StringReader reader = new StringReader(inputXml);
            T[] deserializedDtos = (T[])xmlSerializer.Deserialize(reader);
            return deserializedDtos;
        }

        public string Serialize<T>(T obj, string rootname)
        {
            StringBuilder sb = new StringBuilder();
            XmlRootAttribute xmlRoot = new XmlRootAttribute(rootname);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), xmlRoot);
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            using StringWriter writer = new StringWriter(sb);
            xmlSerializer.Serialize(writer, obj, namespaces);
            return sb.ToString().TrimEnd();
        }

        public string Serialize<T>(T[] obj, string rootname)
        {
            StringBuilder sb = new StringBuilder();
            XmlRootAttribute xmlRootAttribute = new XmlRootAttribute(rootname);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T[]), xmlRootAttribute);
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            using StringWriter writer = new StringWriter(sb);
            xmlSerializer.Serialize(writer, obj, namespaces);
            return sb.ToString().TrimEnd();
        }
    }

}