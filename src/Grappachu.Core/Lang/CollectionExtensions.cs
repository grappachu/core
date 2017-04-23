using System.Collections;
using System.Collections.Generic;

namespace Grappachu.Core.Lang
{
    /// <summary>
    /// Aggiunge dei metodi di utilità per gli elementi che implementano  ICollection  
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Cerca l'oggetto specificato e restituisce la prima occorrenza, in base zero, dell'oggetto trovato
        /// </summary> 
        /// <param name="collection"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable collection, T obj)
        {
            var e = collection.GetEnumerator();
            int i = 0;
            while (e.MoveNext())
            {
                if (ReferenceEquals(e.Current, obj))
                {
                    return i; 
                }
                i++;
            }
            return -1;
        }

        /// <summary>
        /// Consente di inserire un elemento in ICollection<typeparam name="T">T</typeparam> in corrispondenza dell'indice specificato
        /// </summary> 
        /// <param name="collection"></param>
        /// <param name="index"></param>
        /// <param name="obj"></param>
        public static void Insert<T>(this ICollection<T> collection, int index, T obj)
        {
            var reuse = new List<T>();
            var e = collection.GetEnumerator();
            for (int j = 0; j < collection.Count; j++)
            {
                e.MoveNext();
                if (j >= index)
                {
                    reuse.Add(e.Current);
                }
            }
            collection.Add(obj);
            foreach (var x in reuse)
            {
                if (collection.Remove(x))
                {
                    collection.Add(x);
                }
            }
        }

        /// <summary>
        /// Consente di spostare un elemento in ICollection<typeparam name="T">T</typeparam> di una posizione verso l'inizio dell'elenco
        /// </summary> 
        /// <param name="collection"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool MoveUp<T>(this ICollection<T> collection, T obj)
        {
            var curIdx = collection.IndexOf(obj);
            if (curIdx > 0)
            {
                var newIdx = curIdx - 1;
                if (collection.Remove(obj))
                {
                    collection.Insert(newIdx, obj);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Consente di spostare un elemento in ICollection<typeparam name="T">T</typeparam> di una posizione verso la fine dell'elenco
        /// </summary> 
        /// <param name="collection"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool MoveDown<T>(this ICollection<T> collection, T obj)
        {
            var curIdx = collection.IndexOf(obj);
            if (curIdx < collection.Count - 1)
            {
                var newIdx = curIdx + 1;
                if (collection.Remove(obj))
                {
                    collection.Insert(newIdx, obj);
                    return true;
                }
            }
            return false;
        }

    }
}