using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace SaveScripts
{
    public static class XmlSaves
    {
        public static void Save(SaveData data, string path)
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var writer = new StreamWriter(path);
            serializer.Serialize(writer.BaseStream, data);
            writer.Close();
        }

        public static SaveData Load(string path)
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            using var stream = new FileStream(path, FileMode.Open);
            return serializer.Deserialize(stream) as SaveData;
        }
    }
}