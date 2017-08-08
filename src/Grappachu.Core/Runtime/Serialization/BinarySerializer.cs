using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Grappachu.Core.Runtime.Serialization
{
    /// <summary>
    /// Defines a component for data serialization using binary format
    /// </summary>
    public class BinarySerializer : IRuntimeSerializer
    {
        /// <summary>
        /// Serializes an object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public byte[] Serialize(object obj)
        {
            byte[] retval = null;
            if (obj != null)
            {
                using (var buffer = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(buffer, obj);
                    buffer.Flush();
                    retval = buffer.ToArray();
                } 
            }
            return retval;
        }

        /// <summary>
        /// Deserializes an object
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public object Deserialize(byte[] data)
        {
            object retval = null;
            if (data != null)
            {
                using (var buffer = new MemoryStream(data))
                {
                    var formatter = new BinaryFormatter();
                    retval = formatter.Deserialize(buffer);
                }
            }
            return retval;
        }
    }
}