using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace InfinityEngine.Serialization
{
    /// <summary>
    ///  File location
    /// </summary>
    public enum Location
    {
        /// <summary>
        /// <a href="https://docs.unity3d.com/ScriptReference/Application-dataPath.html"> Application.dataPath</a>
        /// </summary>
        DataPath,
        /// <summary>
        /// <a href="https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html"> Application.persistentDataPath</a>
        /// </summary>
        PersistentDataPath
    }

    /// <summary>
    ///  Serialization format.
    /// </summary>
    public enum Format
    {
        /// <summary>
        /// Binary file
        /// </summary>
        BinaryFile,
        /// <summary>
        /// Xml file
        /// </summary>
        XmlFile
    }

    /// <summary>
    ///   Data serializer class.
    /// </summary>
    public static class Serializer
    {
        /// <summary>
        /// Serializes the given object to the given path.
        /// </summary>
        /// <example>
        ///     Example 1 : Save an integer to <a href="https://docs.unity3d.com/ScriptReference/Application-dataPath.html"> Application.dataPath</a> in binary format.<br /><br />
        ///     The following code is an example of use of this method.<br />
        ///     <code>
        ///     int value = 100;
        ///     Serializer.Serialize(value, "myFile.txt", Location.DataPath, Format.BinaryFile);
        ///     </code>
        ///     This code serialize the integer 'value' into the file 'myFile.txt'
        ///     and save the file at 'Application.dataPath + myFile.txt'.<br /><br />
        ///
        ///
        ///     Example 2 :  Save a serializable object 'MyClass' to  <a href="https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html"> Application.persistentDataPath</a> in xml format.<br />
        ///    <code>
        ///     var myClass = new MyClass();
        ///     Serializer.Serialize(myClass, "myFile.xml", Location.PersistentdataPath, Format.XmlFile);
        ///     </code>
        ///     This code serialize the class 'myClass' into the file 'myFile.xml'
        ///     and save the file at 'Application.persistentDataPath + myFile.txt'.   
        /// </example>
        /// <typeparam name="T">The type of the data to serialize</typeparam>
        /// <param name="data">Object to serialize</param>
        /// <param name="path">The path of the file</param>
        /// <param name="location">File location</param>
        /// <param name="format">Serialization format</param>
        public static void Serialize<T>(T data, string path, Location location, Format format)
        {
            switch (format)
            {
                case Format.BinaryFile:
                    SaveFile(data, path, location);
                    break;
                case Format.XmlFile:
                    SaveXml(data, path, location);
                    break;
            }
        }

        /// <summary>
        /// Deserializes the an object of type <c>T</c> from the given path.
        /// </summary>
        /// <example>
        ///     Example 1 : Deserialize an integer to <a href="https://docs.unity3d.com/ScriptReference/Application-persistentdataPath.html"> Application.persistentdataPath</a> in xml format.<br /><br />
        ///     The following code is an example of use of this method.<br />
        ///     <code>
        ///     int value = Serializer.Deserialize &lt;int&gt;("myFile.xml", Location.PersistentdataPath, Format.XmlFile);
        ///     Debug.Log(value);
        ///     </code><br />
        ///     This code deserialize and integer object from the file 'myFile.xml'placed 
        ///     at 'Application.persistentdataPath + myFile.xml'.<br />
        ///
        ///
        ///     Example 2 :  Deserialize an object of type 'MyClass' from  <a href="https://docs.unity3d.com/ScriptReference/Application-dataPath.html"> Application.dataPath</a> in binary format.<br />
        ///    <code>
        ///     var myClass = Serializer.Deserialize&lt;MyClass&gt;("myFile.txt", Location.dataPath, Format.BinaryFile);
        ///     </code>
        ///     <br />
        ///     This code serialize the class 'myClass' into the file 'myFile.txt' and save the file at 'Application.dataPath + myFile.txt'.   
        /// </example>
        /// <typeparam name="T">The type of the data to serialize</typeparam>
        /// <param name="path">The path of the file</param>
        /// <param name="location">File location</param>
        /// <param name="format">Serialization format</param>
        public static T Deserialize<T>(string path, Location location, Format format)
        {
            switch (format)
            {
                case Format.BinaryFile:
                    return LoadFile<T>(path, location);
                case Format.XmlFile:
                    return LoadXml<T>(path, location);
                default:
                    return default(T);
            }
        }

        private static void SaveFile<T>(T data, string path, Location location)
        {
            var path2 = Path.Combine(Application.dataPath, path);
            if (location == Location.PersistentDataPath)
            {
                path2 = Path.Combine(Application.persistentDataPath, path);
            }
            if (File.Exists(path2))
            {
                File.Delete(path2);
            }
            using (var fileStream = new FileStream(path2, FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, data);
                fileStream.Close();
            }
        }

        private static T LoadFile<T>(string path, Location location)
        {
            var path2 = Path.Combine(Application.dataPath, path);
            if (location == Location.PersistentDataPath)
            {
                path2 = Path.Combine(Application.persistentDataPath, path);
            }
            using (var fileStream = new FileStream(path2, FileMode.Open))
            {
                var binaryFormatter = new BinaryFormatter();
                var result = (T)binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
                return result;
            }
        }

        private static void SaveXml<T>(T obj, string path, Location location)
        {
            var path2 = Path.Combine(Application.dataPath, path);
            if (location == Location.PersistentDataPath)
            {
                path2 = Path.Combine(Application.persistentDataPath, path);
            }
            using (var streamWriter = new StreamWriter(new FileStream(path2, FileMode.Create), Encoding.UTF8))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(streamWriter, obj);
                streamWriter.Close();
            }
        }

        private static T LoadXml<T>(string path, Location location)
        {
            var path2 = Path.Combine(Application.dataPath, path);
            if (location == Location.PersistentDataPath)
            {
                path2 = Path.Combine(Application.persistentDataPath, path);
            }
            using (var streamReader = new StreamReader(new FileStream(path2, FileMode.Open), Encoding.UTF8))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                var result = (T)xmlSerializer.Deserialize(streamReader);
                streamReader.Close();
                return result;
            }
        }
    }
}
