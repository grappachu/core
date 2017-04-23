using System;

namespace Grappachu.Core.Media
{
    /// <summary>
    ///     Rappresenta un oggetto con funzionalità di controllo temporale
    /// </summary>
    public interface ISeekable : IPlayable
    {
        /// <summary>
        ///     Ottiene la posizione dell'iterazione corrente
        /// </summary>
        TimeSpan Position { get; }

        /// <summary>
        ///     Ottiene la durata del contenuto in riproduzione
        /// </summary>
        TimeSpan Duration { get; }

        /// <summary>
        ///     Indica se il controllo supporta il seek
        /// </summary>
        bool CanSeek();

        /// <summary>
        ///     Aggiorna la riproduzione di un contenuto all'istante specificato
        /// </summary>
        /// <param name="time"></param>
        void Seek(TimeSpan time);
    }
}