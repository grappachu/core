using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Grappachu.Core.Preview.Runtime.Serialization
{
    /// <summary>
    /// Rappresenta un componente per la serializzazione dei dati in formato binario
    /// </summary>
    public class BinarySerializer : IRuntimeSerializer
    {
        /// <summary>
        /// Serializza un oggetto
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
        /// Deserializza un oggetto
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