using System;
using System.Deployment.Application;
using System.Security.Policy;

namespace Grappachu.Core.Preview.Deployment
{
    internal class ApplicationDeploymentWrapper : IApplicationDeployment
    {
        public bool Update()
        {
            return ApplicationDeployment.CurrentDeployment.Update();
        }

        public ApplicationDeployUpdateInfo CheckForDetailedUpdate()
        {
            // Sometimes CheckForDetailedUpdate throws exception when running async
            // so calling first CheckForUpdate
            if (ApplicationDeployment.CurrentDeployment.CheckForUpdate())
                return new ApplicationDeployUpdateInfo(ApplicationDeployment.CurrentDeployment.CheckForDetailedUpdate());
            return null;
        }

        public ApplicationId GetDeploymentInfo()
        {
            var appSecurityInfo = new ApplicationSecurityInfo(AppDomain.CurrentDomain.ActivationContext);
            return appSecurityInfo.DeploymentId;
        }

        #region Properties

        public bool IsNetworkDeployed
        {
            get { return ApplicationDeployment.IsNetworkDeployed; }
        }

        public Uri ActivationUri
        {
            get { return ApplicationDeployment.CurrentDeployment.ActivationUri; }
        }

        public Uri UpdateLocation
        {
            get { return ApplicationDeployment.CurrentDeployment.UpdateLocation; }
        }

        public bool IsFirstRun
        {
            get { return ApplicationDeployment.CurrentDeployment.IsFirstRun; }
        }

        #endregion
    }
}