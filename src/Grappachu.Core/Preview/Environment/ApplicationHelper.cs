using System;
using System.Deployment.Application;
using System.Reflection;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Grappachu.Core.Preview.Environment
{
    /// <summary>
    ///     Rappresenta un gestore per l'applicazione corrente
    /// </summary>
    public class ApplicationHelper : IApplicationHelper
    {
        /// <summary>
        ///     Riavvia l'applicazione corrente
        /// </summary>
        /// <remarks>
        ///     Il riavvio dell'applicazione potrebbe non funzionare nei seguenti casi:
        ///     L'evento Closing potrebbe impedire lo shutdown dell'applicazione.
        ///     Se l'applicazione è single Instance
        /// </remarks>
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public void Restart()
        {
            Application.Restart();
            System.Environment.Exit(0);
        }

        /// <summary>
        ///     Ottiene il nome del prodotto corrente
        /// </summary>
        /// <returns></returns>
        public static string GetProductName()
        {
            return (Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()).GetName().Name;
        }

        /// <summary>
        ///     Ottiene la versione dell'applicazione corrente.
        /// </summary>
        /// <returns></returns>
        public static Version GetProductVersion()
        {
            return ApplicationDeployment.IsNetworkDeployed
                ? ApplicationDeployment.CurrentDeployment.CurrentVersion
                : (Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()).GetName().Version;
        }

        /// <summary>
        ///     Ottiene il nome del società dell'applicazione corrente
        /// </summary>
        /// <returns></returns>
        public static string GetCompanyName()
        {
            string company = ((AssemblyCompanyAttribute) Attribute
                .GetCustomAttribute((Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()),
                    typeof (AssemblyCompanyAttribute), false)).Company;
            return company;
        }
    }
}