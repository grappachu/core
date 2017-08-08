namespace Grappachu.Core.Runtime.Serialization
{
    /// <summary>
    ///  Defines a component for data serialization
    /// </summary>
    public interface IRuntimeSerializer
    {
        /// <summary>
        /// Serializes an object
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        byte[] Serialize(object data);

        /// <summary>
        /// Deserializes an object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        object Deserialize(byte[] obj);

    }
}