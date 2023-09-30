using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace UtilitiesLibrary
{
    public class BinarySerializer
    {
        public BinarySerializer()
        {
        }

        
        public static T BinaryDeSerializeFromFile<T>(string fileName)
        {
            object obj = null;
            FileStream stream = null;

            try
            {
                if (!File.Exists(fileName))
                {
                    Debug.WriteLine("Fil hittas ej");
                    throw new FileNotFoundException("File does not exist");
                }
                stream = new FileStream(fileName, FileMode.Open);
                Debug.WriteLine("Stream opened");
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                Debug.WriteLine("Binary formatter created");
                obj = binaryFormatter.Deserialize(stream);
                Debug.WriteLine("Object deserialized and opened");
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                //throw;
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

        public static bool BinarySerializeToFile(object obj, string fileName)
        {
            bool sucessfull = false;
            FileStream stream = null;

            try
            {
                stream = new FileStream(fileName, FileMode.Create);
                Debug.WriteLine("Fil skapad");
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                Debug.WriteLine("Binär formatter skapad");
                binaryFormatter.Serialize(stream, obj);
                Debug.WriteLine("Fil sparad");
                sucessfull = true;

            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            return sucessfull;
        }
    }
}
