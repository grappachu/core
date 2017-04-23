using System;
using System.Deployment.Application;
using System.IO;
using System.Text;
using Grappachu.Core.Domain;
using Grappachu.Core.Environment;
using Grappachu.Core.UI;

namespace Grappachu.core.Deployment
{
    /// <summary>
    ///     Rappresenta un componente per la verifica e l'aggiornamento di un'applicazione pubblicata con ClickOnce
    /// </summary>
    public sealed class ApplicationUpdater
    {
        private readonly IApplicationHelper _applicationHelper;
        private readonly IApplicationDeployment _deploymentWrapper;
        private readonly IDialogHolder _dialog;
        //  private readonly ILog _log = LogManager.GetLogger(typeof (ApplicationUpdater));

        #region Events

        /// <summary>
        ///     Richiamato prima dell'inizio dell'aggiornamento
        /// </summary>
        public event EventHandler Updating;

        /// <summary>
        ///     Richiamato prima dell'inizio dell'aggiornamento
        /// </summary>
        private void OnUpdating()
        {
            EventHandler handler = Updating;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Richiamato al termine dell'aggiornamento
        /// </summary>
        public event EventHandler Updated;

        /// <summary>
        ///     Richiamato al termine dell'aggiornamento
        /// </summary>
        private void OnUpdated()
        {
            EventHandler handler = Updated;
            if (handler != null) handler(this, EventArgs.Empty);
        }


        /// <summary>
        ///     Richiamato sul verificarsi di un errore quando <see cref="RequireUserInteraction" /> è
        ///     <value>false</value>
        /// </summary>
        public event EventHandler<ErrorArgs> Error;

        /// <summary>
        ///     Richiamato sul verificarsi di un errore
        /// </summary>
        /// <param name="ex"></param>
        private void OnError(Exception ex)
        {
            if (RequireUserInteraction)
            {
                _dialog.Error(string.Format(@"Cannot install the latest version of the application. 

Please check your network connection, or try again later. Error: {0}", ex),
                    "Update Error");
            }
            else
            {
                EventHandler<ErrorArgs> handler = Error;
                if (handler != null)
                {
                    handler(this, new ErrorArgs(ex));
                }
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Ottiene o imposta il nome del prodotto per cui vengono ricercati gli aggiornamenti
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        ///     Ottiene o imposta un valore che indica se aggiornare l'applicazione con l'intervento di un utente oppure in
        ///     modalità automatica.
        /// </summary>
        public bool RequireUserInteraction { get; set; }

        /// <summary>
        ///     Ottiene o imposta un valore che indica se l'applicazione può essere riavviata al termine di una operazione di
        ///     aggiornamento
        /// </summary>
        public bool RestartAfterUpdate { get; set; }

        /// <summary>
        ///     Ottiene un valore che indica la data dell'ultima verifica degli aggiornamenti andata a buon fine
        /// </summary>
        public DateTime LastChecked { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Attiva una nuova istanza di ApplicationUpdater
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public ApplicationUpdater()
            : this(new Dialogs())
        {
        }

        /// <summary>
        ///     Attiva una nuova istanza di ApplicationUpdater
        /// </summary>
        public ApplicationUpdater(IDialogHolder dialogHolder)
            : this(new ApplicationDeploymentWrapper(), new ApplicationHelper(), dialogHolder)
        {
            ProductName = @"Application";
            RequireUserInteraction = false;
            RestartAfterUpdate = true;
            LastChecked = DateTime.MinValue;
        }

        /// <summary>
        ///     Attiva una nuova istanza di ApplicationUpdater
        /// </summary>
        public ApplicationUpdater(IApplicationDeployment deploymentWrapper, IApplicationHelper applicationHelper,
            IDialogHolder dialogHolder)
        {
            _deploymentWrapper = deploymentWrapper;
            _applicationHelper = applicationHelper;
            _dialog = dialogHolder;
        }

        #endregion

        /// <summary>
        ///     Verifica ed aggiorna l'applicazione corrente
        /// </summary>
        public void Update()
        {
            try
            {
                if (_deploymentWrapper.IsNetworkDeployed)
                {
                    ApplicationDeployUpdateInfo info = OnCheckUpdates(_deploymentWrapper);
                    if (info == null)
                    {
                        //_log.Warn("Checking for application updates failed: Connection failed");
                        //_log.WarnFormat(" - ActivationURI: {0}", _deploymentWrapper.ActivationUri);
                        //_log.WarnFormat(" - UpdateLocation: {0}", _deploymentWrapper.UpdateLocation);
                        return;
                    }

                    if (info.UpdateAvailable)
                    {
                        //_log.WarnFormat("A new version of {0} is available", ProductName);
                        //_log.InfoFormat(" - Version: {0} )", info.AvailableVersion);
                        //_log.InfoFormat(" - Size:    {0} bytes)", info.UpdateSizeBytes);

                        if (RequireUserInteraction)
                        {
                            //  _log.Info("Performing update");
                            OnPerformUserInteractiveUpdate(info);
                        }
                        else
                        {
                            //   _log.Info("Performing update in silent mode");
                            OnPerformSilentUpdate();
                        }
                    }
                    else
                    {
                        //  _log.InfoFormat("{0} is up to date.", ProductName);
                    }
                }
                else
                {
                    //  _log.Info("This is a debug version and cannot be updated automatically.");
                }
            }
            catch (Exception ex)
            {
                // _log.Error("Update Failed.", ex);
                OnError(ex);
            }
        }

        /// <summary>
        ///     Verifica la disponibilità di nuovi aggiornamenti per l'applicazione corrente
        /// </summary>
        private ApplicationDeployUpdateInfo OnCheckUpdates(IApplicationDeployment applicationDeployment)
        {
            ApplicationDeployUpdateInfo info;
            try
            {
                //  _log.Info("Checking for updates");
                info = applicationDeployment.CheckForDetailedUpdate();
                LastChecked = DateTime.Now;
            }
            catch (DeploymentDownloadException dde)
            {
                //   _log.Error("The new version of the application cannot be downloaded at this time.", dde);
                OnError(dde);
                return null;
            }
            catch (InvalidDeploymentException ide)
            {
                //   _log.Error("Cannot check for a new version of the application. The ClickOnce deployment is corrupt. ", ide);
                OnError(ide);
                return null;
            }
            catch (InvalidOperationException ioe)
            {
                //_log.Error(
                //    "The current application is either not configured to support updates, or there is another update check operation already in progress.",
                //    ioe);
                OnError(ioe);
                return null;
            }
            return info;
        }


        private void OnPerformSilentUpdate()
        {
            bool res = DoUpdate(_deploymentWrapper);
            if (res)
            {
                //  _log.Info("Application updated succesfully");
                if (RestartAfterUpdate)
                {
                    AppRestart();
                }
            }
        }


        private bool DoUpdate(IApplicationDeployment ad)
        {
            try
            {
                //  _log.Debug("Running preliminary operations");
                OnUpdating();

                //   _log.Debug("Running update");
                bool ret = ad.Update();

                //   _log.Debug("Running finalizing operations");
                OnUpdated();

                return ret;
            }
            catch (DeploymentDownloadException dde)
            {
                //  _log.Error("The new deployment could not be downloaded from its location on the network.", dde);
                OnError(dde);
                return false;
            }
            catch (InvalidDeploymentException idex)
            {
                //  _log.Error("Your ClickOnce deployment is corrupted.", idex);
                OnError(idex);
                return false;
            }
            catch (InvalidOperationException ioex)
            {
                //  _log.Error("The application is currently being updated.", ioex);
                OnError(ioex);
                return false;
            }
            catch (TrustNotGrantedException tngex)
            {
                //_log.Error(
                //    "The local computer did not grant the application the permission level it requested to execute.",
                //    tngex);
                OnError(tngex);
                return false;
            }
        }


        // ---------------------------------------------


        private void OnPerformUserInteractiveUpdate(ApplicationDeployUpdateInfo info)
        {
            // Require User to confirm update
            // ---------------------
            bool updateCancelled = false;
            if (info.IsUpdateRequired)
            {
                // Display a message that the app MUST reboot. Display the minimum required version.
                _dialog.Info(
                    string.Format("{0} has detected a mandatory update from your current version to version {1}. {0} will now install the update and restart.",
                   ProductName, info.MinimumRequiredVersion),
                    "Update Available");
            }
            else
            {
                if (_dialog.Question(string.Format("An update is available. Would you like to update {0} now?", ProductName),
                    "Update Available") != true)
                {
                    // _log.Warn("User has refused to update.");
                    updateCancelled = true;
                }
                else
                {
                    //  _log.Info("User agreed to update");
                }
            }


            // Require User to confirm update
            // ---------------------
            if (!updateCancelled)
            {
                // _log.Info("Performing update");
                bool res = DoUpdate(_deploymentWrapper);

                if (res)
                {
                    //_log.Info("Application updated succesfully");
                    if (RestartAfterUpdate)
                    {
                        _dialog.Info("The application has been upgraded and will now restart.");
                        AppRestart();
                    }
                    else
                    {
                        _dialog.Info("The application has been upgraded and the new version will be available after restart.");
                    }
                }
            }
        }


        private void AppRestart()
        {
            try
            {
                //_log.Warn("Restarting application");
                _applicationHelper.Restart();
            }
            catch (Exception ex)
            {
                //_log.Error("An error has occurred while restarting application", ex);
                OnError(ex);
            }
        }


        /// <summary>
        ///     Crea un collegamento con il riferimento al deployment dell'applicazione (appref-ms).
        /// </summary>
        /// <remarks>
        ///     Il collegamento creato con questo metodo riferisce al prodotto installato
        ///     pertanto non viene alterato a seguito di un aggiornamento da parte di ClickOnce.
        /// </remarks>
        /// <param name="path"></param>
        /// <param name="caption"></param>
        public string CreateDeploymentShortcut(DirectoryInfo path, string caption)
        {
            if (_deploymentWrapper.IsNetworkDeployed)
            {
                string linkPath = Path.Combine(path.ToString(), caption + ".appref-ms");
                Uri updateLocation = _deploymentWrapper.UpdateLocation;

                ApplicationId deploymentInfo = _deploymentWrapper.GetDeploymentInfo();
                using (var shortcutFile = new StreamWriter(linkPath, false, Encoding.Unicode))
                {
                    shortcutFile.Write(@"{0}#{1}, Culture=neutral, PublicKeyToken=",
                        updateLocation.ToString().Replace(" ", "%20"), deploymentInfo.Name.Replace(" ", "%20"));
                    foreach (byte b in deploymentInfo.PublicKeyToken)
                        shortcutFile.Write("{0:x2}", b);
                    shortcutFile.Write(", processorArchitecture={0}", deploymentInfo.ProcessorArchitecture);
                }
                return linkPath;
            }
            throw new InvalidDeploymentException("This is not a ClickOnce application");
        }
    }
}