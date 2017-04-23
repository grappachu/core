using System.Collections.Generic;

namespace Grappachu.Core.Util.Duplicates
{
    /// <summary>
    /// Rappresenta un gruppo di oggetti duplicati
    /// </summary>
    /// <typeparam name="TK"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class DuplicateGroup<TK, T>
    {
        /// <summary>
        /// Ottiene o imposta la chiave comune a tutti gli oggetti del gruppo
        /// </summary>
        public TK Key { get; set; }

        /// <summary>
        /// Ottiene l'elenco degli oggetti duplicati
        /// </summary>
        public ICollection<T> Items { get; private set; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="DuplicateGroup{TK,T}"/>
        /// </summary>
        public DuplicateGroup()
        {
            Items = new List<T>();
        }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="DuplicateGroup{TK,T}"/>
        /// </summary>
        /// <param name="key">Chiave del gruppo</param>
        public DuplicateGroup(TK key)
            : this()
        {
            Key = key;
        }
    }

    
}
