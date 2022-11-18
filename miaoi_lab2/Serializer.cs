using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

using System.Threading.Tasks;

namespace miaoi_lab2
{
    class Serializer
    {
        public static string path = "..\\papkaSave";
        public static void doSerializing(Class examples)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            
                using (FileStream fs = File.Create(path))
                    formatter.Serialize(fs,examples);
                Console.WriteLine(path);  
        }

        public static void doSerializingAdd1(Example example)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Class examples = new Class();
            if (File.Exists(path)) {
                using (FileStream fs = File.Open(path, FileMode.Open))
                    examples = (Class)formatter.Deserialize(fs);
                examples.addExample(example);
                using (FileStream fs = File.Create(path))
                    formatter.Serialize(fs, examples);
                }
            
        }

        public static Class doDeserializing()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Class examples = new Class();
            if (File.Exists(path)){
                using (FileStream fs = File.Open(path, FileMode.Open))
                    examples = (Class)formatter.Deserialize(fs);
                
            }
            return examples;
        }
    }
}
