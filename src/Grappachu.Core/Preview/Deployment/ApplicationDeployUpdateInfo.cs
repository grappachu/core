using System;
using System.Deployment.Application;

namespace Grappachu.Core.Preview.Deployment
{
    /// <summary>
    ///     Rappresenta informazioni dettagliate sull'aggiornamento
    /// </summary>
    public class ApplicationDeployUpdateInfo
    {
        /// <summary>
        ///     Attiva una nuova istanza di <see cref="ApplicationDeployUpdateInfo" />
        /// </summary>
        public ApplicationDeployUpdateInfo()
        {
        }

        /// <summary>
        ///     Attiva una nuova istanza di <see cref="ApplicationDeployUpdateInfo" />
        /// </summary>
        public ApplicationDeployUpdateInfo(UpdateCheckInfo info)
        {
            AvailableVersion = info.AvailableVersion;
            UpdateSizeBytes = info.UpdateSizeBytes;
            MinimumRequiredVersion = info.MinimumRequiredVersion;
            UpdateAvailable = info.UpdateAvailable;
            IsUpdateRequired = info.IsUpdateRequired;
        }

        /// <summary>
        ///     Ottiene il numero di versione dell'ultima versione non disinstallata
        /// </summary>
        public Version AvailableVersion { get; set; }

        /// <summary>
        ///     Ottiene la versione minima che deve essere installata sul computer dell'utente.
        /// </summary>
        public Version MinimumRequiredVersion { get; set; }

        /// <summary>
        ///     Ottiene le dimensioni dell'aggiornamento disponibile
        /// </summary>
        public long UpdateSizeBytes { get; set; }

        /// <summary>
        ///     Ottiene informazioni sulla disponibilità di un aggiornamento non installato.
        /// </summary>
        /// <returns>
        ///     true se è disponibile una nuova versione dell'applicazione; in caso contrario, false.
        /// </returns>
        public bool UpdateAvailable { get; set; }

        /// <summary>
        ///     Ottiene un valore che indica se è necessario installare l'aggiornamento.
        /// </summary>
        public bool IsUpdateRequired { get; set; }
    }
}