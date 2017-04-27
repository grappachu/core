using System.Security.Permissions;

namespace Grappachu.Core.Preview.Environment
{
    /// <summary>
    /// Rappresenta un gestore per un'applicazione
    /// </summary>
    public interface IApplicationHelper
    {  
        /// <summary>
        /// Chiude l'applicazione e avvia immediatamente una nuova istanza.
        /// </summary> 
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        void Restart();
    }
}