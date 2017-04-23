using System;
using System.ComponentModel;

namespace Grappachu.Core.Media
{
    /// <summary>
    ///     Rappresenta un oggetto dotato di funzionalità di riproduzione multimediale
    /// </summary>
    public interface IPlayable : INotifyPropertyChanged
    {
        /// <summary>
        ///     Ottiene lo stato del controllo
        /// </summary>
        MediaState Status { get; }

        /// <summary>
        ///     Richiamato al termine della riproduzione del contenuto
        /// </summary>
        event EventHandler MediaEnded;

        /// <summary>
        /// Richiamato al cambio di stato di un player multimediale
        /// </summary>
        event EventHandler<MediaStateEventArgs> MediaStateChanged;

        /// <summary>
        ///     Avvia la riproduzione di un contenuto
        /// </summary>
        void Play();

        /// <summary>
        ///     Arresta la riproduzione di un contenuto
        /// </summary>
        void Stop();

        /// <summary>
        ///     Sospende la riproduzione di un contenuto
        /// </summary>
        void Pause();
    }
}