using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Grappachu.Core.Security;

namespace Grappachu.Core.IO
{
    /// <summary>
    ///     Rappresenta un componente per l'indicizzazione dei file basato su una chiave
    /// </summary>
    public interface IFileIndex
    {
        /// <summary>
        ///     Ottiene il numero di elementi presenti nell'indice
        /// </summary>
        int ItemsCount { get; }

        /// <summary>
        ///     Ottiene tutti i file disponibili che rispondono alla chiava specificata
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string[] GetFiles(string key);

    }

    /// <summary>
    ///     Rappresenta un componente per l'indicizzazione dei file basato su una chiave
    /// </summary>
    public class FileIndex : BackgroundWorker, IFileIndex
    {
        private readonly IFileEnumerator _enumerator;
        private readonly IKeyGenerator<string, string> _keyGenerator;
        private readonly Dictionary<string, FileKeyWrapper> _values = new Dictionary<string, FileKeyWrapper>();

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="FileIndex"/>
        /// </summary>
        /// <param name="keyGenerator">Generatore di chiave per i file</param>
        /// <param name="enumerator">Enumeratore per i file da indicizzare</param>
        public FileIndex(IKeyGenerator<string, string> keyGenerator, IFileEnumerator enumerator)
        {
            _enumerator = enumerator;
            _keyGenerator = keyGenerator;
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;
        }

        /// <summary>
        ///     Ottiene il numero di elementi presenti nell'indice
        /// </summary>
        public int ItemsCount
        {
            get { return _values.Count; }
        }

        /// <summary>
        /// Genera l'evento <see cref="E:System.ComponentModel.BackgroundWorker.DoWork"/>. 
        /// </summary>
        /// <param name="e">Oggetto <see cref="T:System.EventArgs"/> contenente i dati dell'evento.</param>
        protected override void OnDoWork(DoWorkEventArgs e)
        {
            OnUpdate(e);
        }

        private void OnUpdate(DoWorkEventArgs e)
        {
            //  Clean:
            foreach (string file in _values.Keys)
            {
                if (!File.Exists(file))
                {
                    _values.Remove(file);
                }
            }

            // Indexing
            int i = 0;
            FileInfo[] files = _enumerator.ToArray();
            foreach (FileInfo file in _enumerator)
            {
                if (CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                i++;

                try
                {
                    if (file.Exists)
                    {
                        var keyWrapper = new FileKeyWrapper(file.FullName, _keyGenerator);
                        if (_values.ContainsKey(file.FullName))
                        {
                            FileKeyWrapper cached = _values[file.FullName];
                            if (cached.Size != file.Length || cached.LastWriteUtc != file.LastWriteTimeUtc)
                            {
                                _values[file.FullName] = keyWrapper;
                                ReportProgress((i * 100) / files.Length, keyWrapper.Hash);
                            }
                        }
                        else
                        {
                            _values.Add(file.FullName, keyWrapper);
                            ReportProgress((i * 100) / files.Length, keyWrapper.Hash);
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    if (_values.ContainsKey(file.FullName))
                    {
                        _values.Remove(file.FullName);
                    }
                }
            }
        }

        private static bool IsValid(FileKeyWrapper key, string path)
        {
            if (File.Exists(path))
            {
                if (key.LastWriteUtc == File.GetLastWriteTimeUtc(path))
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        ///     Ottiene tutti i file disponibili che rispondono alla chiava specificata
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string[] GetFiles(string key)
        {
            return _values.Where(x => x.Value.Hash == key && IsValid(x.Value, x.Key))
                .Select(z => z.Key).ToArray();
        }

        //public void SaveData(string file,  IRuntimeSerializer serializer)
        //{
        //    // Serialize data to File
        //}

        //public void LoadData(string file, IRuntimeSerializer serializer)
        //{
        //    // Deserialize data
        //}


        [Serializable]
        private sealed class FileKeyWrapper
        {
            private readonly string _file;
            private readonly IKeyGenerator<string, string> _keyGenerator;
            private readonly DateTime _lastWriteUtc;
            private readonly long _size;

            private string _hash;

            internal FileKeyWrapper(string file, IKeyGenerator<string, string> keyGenerator)
            {
                _file = file;
                _keyGenerator = keyGenerator;
                _lastWriteUtc = File.GetLastWriteTimeUtc(_file);
                _size = FileManager.GetSize(_file);
            }

            internal long Size
            {
                get { return _size; }
            }

            internal DateTime LastWriteUtc
            {
                get { return _lastWriteUtc; }
            }

            internal string Hash
            {
                get
                {
                    if (_hash != null && !HasChanged(_file))
                    {
                        return _hash;
                    }
                    _hash = _keyGenerator.GenerateKey(_file);
                    return _hash;
                }
            }

            private bool HasChanged(string file)
            {
                return File.GetLastWriteTimeUtc(file) != _lastWriteUtc;
            }

            private bool Equals(FileKeyWrapper other)
            {
                return Equals(Size, other.Size) && string.Equals(Hash, other.Hash);
            }

            /// <summary>
            /// Funge da funzione hash per un determinato tipo. 
            /// </summary>
            /// <returns>
            /// Codice hash per la classe <see cref="T:System.Object"/> corrente.
            /// </returns>
            /// <filterpriority>2</filterpriority>
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = Size.GetHashCode();
                    return hashCode;
                }
            }

            /// <summary>
            /// Consente di determinare se l'oggetto <see cref="T:System.Object"/> specificato è uguale all'oggetto <see cref="T:System.Object"/> corrente.
            /// </summary>
            /// <returns>
            /// true se l'oggetto <see cref="T:System.Object"/> specificato è uguale all'oggetto <see cref="T:System.Object"/> corrente; in caso contrario, false.
            /// </returns>
            /// <param name="obj">Oggetto <see cref="T:System.Object"/> da confrontare con l'oggetto <see cref="T:System.Object"/> corrente. 
            ///                 </param><exception cref="T:System.NullReferenceException">Il parametro <paramref name="obj"/> è null.
            ///                 </exception><filterpriority>2</filterpriority>
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals((FileKeyWrapper)obj);
            }
        }
    }
}