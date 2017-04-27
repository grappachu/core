using System;
using System.Diagnostics;
using System.IO;
using Grappachu.Core.Lang.Extensions;
using Grappachu.Core.Lang.Text;
using Grappachu.Core.Preview.Runtime.Serialization;
using Grappachu.Core.Security.Hashing;

namespace Grappachu.Core.Preview.IO
{
    /// <summary>
    /// Fornisce i metodi statici per la gestione di file e directory
    /// </summary>
    public class FileManager
    {
        /// <summary>
        /// Elimina un file su disco, se presente.
        /// </summary>
        public static void SafeDelete(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        /// <summary>
        /// Ottiene la dimensione in byte di un file, 0 se non esiste.
        /// </summary>
        public static long GetSize(string path)
        {
            var finfo = new FileInfo(path);
            if (finfo.Exists)
            {
                return finfo.Length;
            }
            return 0;
        }

        /// <summary>
        /// Salva un oggetto su un file
        /// </summary>
        /// <param name="objectToSave"></param>
        /// <param name="filename"></param>
        /// <param name="serializer"></param>  
        public static void SaveToFile(object objectToSave, string filename, IRuntimeSerializer serializer)
        {
            var bytes = serializer.Serialize(objectToSave);
            File.WriteAllBytes(filename, bytes);
        }


        /// <summary>
        /// Ripristina un oggetto serializzato da un file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public static T ReadFromFile<T>(string filename, IRuntimeSerializer serializer)
        {
            var bytes = File.ReadAllBytes(filename);
            var toReturn = serializer.Deserialize(bytes);
            return (T)toReturn;
        }

        /// <summary>
        /// Verifica lo stato di un file su disco.
        /// </summary>
        /// <param name="filename">Percorso del file da verificare</param>
        /// <param name="hash">Hash atteso per questo file</param>
        /// <param name="provider">Specifica l'algoritmo di hashing da utilizzare</param>
        /// <param name="usePersistentFile">Legge, crea o aggiorna un file di testo con l'hash calcolato per il file. 
        /// Può rendere piu veloci i controlli ripetuti.</param>
        /// <returns></returns> 
        public static FileStatus VerifyHash(string filename, string hash, IHashProvider provider, bool usePersistentFile = false)
        {
            try
            {
                if (!File.Exists(filename))
                {
                    return FileStatus.Missing;
                }

                string calucatedHash;

                if (usePersistentFile)
                {
                    var hashfile = GetHashFileName(filename, provider);
                    // utilizza un file temporaneo con l'hash calcolato per velocizzare i controlli successivi
                    if (File.Exists(hashfile) && File.GetLastWriteTime(hashfile) >= File.GetLastWriteTime(filename))
                    {
                        calucatedHash = File.ReadAllText(hashfile);
                    }
                    else
                    {
                        calucatedHash = provider.Hash(new FileInfo(filename)).ToString(default(BinaryRepresentation));
                        if (File.Exists(hashfile))
                        {
                            // If File is hidden File.WriteAllText throws UnauthorizedAccessException
                            File.SetAttributes(hashfile, FileAttributes.Normal);
                        }
                        File.WriteAllText(hashfile, calucatedHash);
                        File.SetAttributes(hashfile, FileAttributes.Hidden | FileAttributes.NotContentIndexed);
                    }
                }
                else
                {
                    calucatedHash = provider.Hash(new FileInfo(filename)).ToString(default(BinaryRepresentation));
                }

                return calucatedHash.Equals(hash)
                    ? FileStatus.Present
                    : FileStatus.Corrupted;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return FileStatus.Unknown;
            }

        }

        /// <summary>
        /// Rimuove un file di hash relativo ad un file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="provider"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static void CleanHash(string filename, IHashProvider provider)
        {
            var hashfile = GetHashFileName(filename, provider);
            if (File.Exists(hashfile))
            {
                File.Delete(hashfile);
            }
        }

        private static string GetHashFileName(string filename, IHashProvider provider)
        {
            var hashfile = filename + "." + provider.Algorythm;
            return hashfile;
        }
    }
}
