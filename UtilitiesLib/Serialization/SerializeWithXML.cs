using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace UtilitiesLibrary
{
    public class SerializeWithXML
    {
        public SerializeWithXML()
        {
        }

        public static bool XMLSerialize<T>(T obj, string fileName)
        {
            bool successful = false;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            TextWriter stream = new StreamWriter(fileName);

            try
            {
                xmlSerializer.Serialize(stream, obj);
                successful = true;
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Serialization failed, reason: " + e.Message);
            }

            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            return successful;
        }

        public static T XMLDeSerializeFile<T>(string fileName)
        {
            object obj = null;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            TextReader stream = new StreamReader(fileName);

            try
            {
                obj = (T)xmlSerializer.Deserialize(stream);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Serialization failed, reason: " + e.Message);
            }

            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            return (T)obj;
        }
    }
}
