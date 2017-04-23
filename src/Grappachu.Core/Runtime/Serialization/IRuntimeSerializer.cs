namespace Grappachu.Core.Runtime.Serialization
{
    /// <summary>
    /// Definisce una interfaccia per la serializzazione dei dati
    /// </summary>
    public interface IRuntimeSerializer
    {
        /// <summary>
        /// Serializza un oggetto
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        byte[] Serialize(object data);

        /// <summary>
        /// Deserializza un oggetto
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        object Deserialize(byte[] obj);

    }
}