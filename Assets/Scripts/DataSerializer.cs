using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
namespace EggNamespace.Serialization
{
    public static class DataSerializer
    {
        private static string folderPath = string.Join("//", Application.persistentDataPath,
    "EggData");


        public static void SerializeData<T>(T data, string fileName) where T : class
        {
            string fullPath = GetFullPath(fileName);
            if (!File.Exists(fullPath))
                Directory.CreateDirectory(folderPath);
            var serializer = new XmlSerializer(typeof(T));
            var stream = new FileStream(fullPath, FileMode.Create);
            serializer.Serialize(stream, data);
            stream.Close();
        }
        public static T DeserializeData<T>(string fileName) where T : class
        {
            string fullPath = GetFullPath(fileName);
            if (!File.Exists(fullPath))
                return null;
            var serializer = new XmlSerializer(typeof(T));
            var stream = new FileStream(fullPath, FileMode.Open);
            var data = serializer.Deserialize(stream) as T;
            stream.Close();
            return data;
        }
        private static string GetFullPath(string fileName)
        {
            return string.Join("//", folderPath, fileName + ".dat");
        }
    }
}