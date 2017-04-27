using System;

namespace Grappachu.Core.Preview.Deployment
{
    /// <summary>
    ///     Rappresenta un componente per il controllo del deploy di una applicatione
    /// </summary>
    public interface IApplicationDeployment
    {
        /// <summary>
        ///     Ottiene un valore che indica che l'applicazione è deployata con il sistema di deploy gestito da questa interfaccia.
        /// </summary>
        bool IsNetworkDeployed { get; }

        /// <summary>
        ///     Ottiene l'Url utilizzato per avviare il manifesto di distribuzione dell'applicazione
        /// </summary>
        Uri ActivationUri { get; }

        /// <summary>
        ///     Ottiene la condivisione file o il sito web utilizzato per l'aggiornamento automatica da perte dell'applicazione
        /// </summary>
        Uri UpdateLocation { get; }

        /// <summary>
        ///     Ottiene un valore che indica se si tratta della prima volta che l'applicazione è stata eseguita sul computer client
        /// </summary>
        bool IsFirstRun { get; }

        /// <summary>
        ///     Avvia in modo sincrono il download e l'installazione dell'ultima versione dell'applicazione corrente.
        /// </summary>
        bool Update();

        /// <summary>
        ///     Ottiene informazioni dettagliate sull'aggiornamento
        /// </summary>
        /// <returns></returns>
        ApplicationDeployUpdateInfo CheckForDetailedUpdate();

        /// <summary>
        ///     Ottiene informazioni sull'applicazione nel dominio corrente
        /// </summary>
        ApplicationId GetDeploymentInfo();
    }
}