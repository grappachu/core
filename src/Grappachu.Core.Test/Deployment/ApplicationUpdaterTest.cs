using System;
using System.Deployment.Application;
using System.IO;
using Grappachu.core.Deployment;
using Grappachu.Core.Environment;
using Grappachu.Core.UI;
using Moq;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Deployment
{
    [TestFixture]
    internal class ApplicationUpdaterTest
    {
        #region Init

        [SetUp]
        public void SetUp()
        {
            _dialogHolderMock = new Mock<IDialogHolder>();
            _deploymentWrapperMock = new Mock<IApplicationDeployment>();
            _applicationHelperMock = new Mock<IApplicationHelper>();

            _sut = new ApplicationUpdater(_deploymentWrapperMock.Object,
                _applicationHelperMock.Object, _dialogHolderMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(_tempDir))
            {
                Directory.Delete(_tempDir, true);
            }
        }

        private ApplicationUpdater _sut;

        private Mock<IDialogHolder> _dialogHolderMock;
        private Mock<IApplicationDeployment> _deploymentWrapperMock;
        private Mock<IApplicationHelper> _applicationHelperMock;
        private string _tempDir;

        #endregion

        #region Tests

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Update_Should_CheckRemoteVersion_When_NetworkDeployed(bool userInteraction)
        {
            _sut.RequireUserInteraction = userInteraction;

            _deploymentWrapperMock.Setup(x => x.IsNetworkDeployed).Returns(true);

            _sut.Update();

            _deploymentWrapperMock.Verify(x => x.CheckForDetailedUpdate());
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Update_Should_DoNothing_When_NotNetworkDeployed(bool userInteraction)
        {
            _sut.RequireUserInteraction = userInteraction;

            _deploymentWrapperMock.Setup(x => x.IsNetworkDeployed).Returns(false);

            _sut.Update();

            _deploymentWrapperMock.Verify(x => x.Update(), Times.Never);
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Update_Should_DoNothing_When_RunningLatestVersion(bool userInteraction)
        {
            _sut.RequireUserInteraction = userInteraction;

            var updateCheckInfo = new ApplicationDeployUpdateInfo
            {
                UpdateAvailable = false,
                AvailableVersion = new Version(1, 0, 2),
                IsUpdateRequired = false,
                MinimumRequiredVersion = new Version(1, 0, 2),
                UpdateSizeBytes = 100000
            };
            _deploymentWrapperMock.Setup(x => x.IsNetworkDeployed).Returns(true);
            _deploymentWrapperMock.Setup(x => x.CheckForDetailedUpdate()).Returns(updateCheckInfo);

            _sut.Update();

            _deploymentWrapperMock.Verify(x => x.Update(), Times.Never);
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Update_Should_FireAdEvent_After_Update(bool userInteraction)
        {
            _sut.RequireUserInteraction = userInteraction;

            bool eventFired = false;

            var updateCheckInfo = new ApplicationDeployUpdateInfo
            {
                UpdateAvailable = true,
                AvailableVersion = new Version(1, 0, 3),
                IsUpdateRequired = false,
                MinimumRequiredVersion = new Version(1, 0, 2),
                UpdateSizeBytes = 100000
            };
            _deploymentWrapperMock.Setup(x => x.IsNetworkDeployed).Returns(true);
            _deploymentWrapperMock.Setup(x => x.CheckForDetailedUpdate()).Returns(updateCheckInfo);
            // User Agreed
            _dialogHolderMock.Setup(d => d.Question(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
               .Returns(true);


            _sut.Updated += (sender, args) =>
            {
                _deploymentWrapperMock.Verify(x => x.Update());
                eventFired = true;
            };

            _sut.Update();

            eventFired.Should().Be.True();
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Update_Should_FireAdEvent_Before_Update(bool userInteraction)
        {
            _sut.RequireUserInteraction = userInteraction;

            bool eventFired = false;
            var updateCheckInfo = new ApplicationDeployUpdateInfo
            {
                UpdateAvailable = true,
                AvailableVersion = new Version(1, 0, 3),
                IsUpdateRequired = false,
                MinimumRequiredVersion = new Version(1, 0, 2),
                UpdateSizeBytes = 100000
            };
            _deploymentWrapperMock.Setup(x => x.IsNetworkDeployed).Returns(true);
            _deploymentWrapperMock.Setup(x => x.CheckForDetailedUpdate()).Returns(updateCheckInfo);
            // User Agreed
            _dialogHolderMock.Setup(d => d.Question(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
               .Returns(true);
              
            _sut.Updating += (sender, args) =>
            {
                eventFired = true;
                _deploymentWrapperMock.Verify(x => x.Update());
            };

            _sut.Update();

            eventFired.Should().Be.True();
            _deploymentWrapperMock.Verify(x => x.Update(), Times.Never);
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Update_Should_RunUpdate_When_UpdateAvailable(bool userInteraction)
        {
            _sut.RequireUserInteraction = userInteraction;

            var updateCheckInfo = new ApplicationDeployUpdateInfo
            {
                UpdateAvailable = true,
                AvailableVersion = new Version(1, 0, 3),
                IsUpdateRequired = false,
                MinimumRequiredVersion = new Version(1, 0, 2),
                UpdateSizeBytes = 100000
            };
            _deploymentWrapperMock.Setup(x => x.IsNetworkDeployed).Returns(true);
            _deploymentWrapperMock.Setup(x => x.CheckForDetailedUpdate()).Returns(updateCheckInfo);

            // User agreed
            _dialogHolderMock.Setup(d => d.Question(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
               .Returns(true);


            _sut.Update();

            _deploymentWrapperMock.Verify(x => x.Update(), Times.Once);
        }


        [Test]
        public void Update_Should_AllowUserToRefuse_When_UserInteractive_Is_Set()
        {
            _sut.RequireUserInteraction = true;
            var updateCheckInfo = new ApplicationDeployUpdateInfo
            {
                UpdateAvailable = true,
                AvailableVersion = new Version(1, 0, 3),
                IsUpdateRequired = false,
                MinimumRequiredVersion = new Version(1, 0, 2),
                UpdateSizeBytes = 100000
            };
            _deploymentWrapperMock.Setup(x => x.IsNetworkDeployed).Returns(true);
            _deploymentWrapperMock.Setup(x => x.CheckForDetailedUpdate()).Returns(updateCheckInfo);

            // User Refused
            _dialogHolderMock.Setup(d => d.Question(It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(false);

            _sut.Update();

            _deploymentWrapperMock.Verify(x => x.Update(), Times.Never);
        }

        [Test]
        [TestCase(false, false)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(true, true)]
        public void Update_Should_RestartApplication_OnlyWhen_Restart_Is_Set(bool userInteraction, bool restartAfterUpdate)
        {
            _sut.RequireUserInteraction = userInteraction;
            _sut.RestartAfterUpdate = restartAfterUpdate;

            var updateCheckInfo = new ApplicationDeployUpdateInfo
            {
                UpdateAvailable = true,
                AvailableVersion = new Version(1, 0, 3),
                IsUpdateRequired = false,
                MinimumRequiredVersion = new Version(1, 0, 2),
                UpdateSizeBytes = 100000
            };
            _deploymentWrapperMock.Setup(x => x.IsNetworkDeployed).Returns(true);
            _deploymentWrapperMock.Setup(x => x.CheckForDetailedUpdate()).Returns(updateCheckInfo);
            // User agreed
            _dialogHolderMock.Setup(d => d.Question(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                    .Returns(true);

            _deploymentWrapperMock.Setup(x => x.Update()).Returns(true);


            _sut.Update();

            if (restartAfterUpdate)
            {
                _applicationHelperMock.Verify(x => x.Restart(), Times.Once);
            }
            else
            {
                _applicationHelperMock.Verify(x => x.Restart(), Times.Never);
            }
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Update_Should_FireEvent_OnlyWhen_UserInteractive_Is_Not_Set(bool userInteraction)
        {
            _sut.RequireUserInteraction = userInteraction;

            bool eventFired = false;

            var updateCheckInfo = new ApplicationDeployUpdateInfo
            {
                UpdateAvailable = true,
                AvailableVersion = new Version(1, 0, 3),
                IsUpdateRequired = false,
                MinimumRequiredVersion = new Version(1, 0, 2),
                UpdateSizeBytes = 100000
            };
            _deploymentWrapperMock.Setup(x => x.IsNetworkDeployed).Returns(true);
            _deploymentWrapperMock.Setup(x => x.CheckForDetailedUpdate()).Returns(updateCheckInfo);
            // User Agreed
            _dialogHolderMock.Setup(d => d.Question(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                 .Returns(true);


            _deploymentWrapperMock.Setup(x => x.Update()).Throws(new InvalidDeploymentException());

            _sut.Error += (sender, args) =>
            {
                eventFired = true;
            };

            _sut.Update();


            eventFired.Should().Be.EqualTo(!userInteraction);
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Update_Should_Set_LastCheck_Date(bool userInteraction)
        {
            _sut.RequireUserInteraction = userInteraction;
            _sut.LastChecked.Should().Be.LessThan(DateTime.Now);

            var updateCheckInfo = new ApplicationDeployUpdateInfo
            {
                UpdateAvailable = true,
                AvailableVersion = new Version(1, 0, 3),
                IsUpdateRequired = false,
                MinimumRequiredVersion = new Version(1, 0, 2),
                UpdateSizeBytes = 100000
            };
            _deploymentWrapperMock.Setup(x => x.IsNetworkDeployed).Returns(true);
            _deploymentWrapperMock.Setup(x => x.CheckForDetailedUpdate()).Returns(updateCheckInfo);

            // User agreed
            _dialogHolderMock.Setup(d => d.Question(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(true);

            _sut.Update();

            _sut.LastChecked.Should().Be.IncludedIn(DateTime.Now.AddSeconds(-1), DateTime.Now);
        }

        [Test]
        public void CreateDeploymentShortcut_Should_Write_A_Shortcut()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_tempDir);

            _deploymentWrapperMock.Setup(x => x.IsNetworkDeployed).Returns(true);
            _deploymentWrapperMock.Setup(x => x.GetDeploymentInfo())
                .Returns(new ApplicationId(new byte[0], "TestApp", new Version(1, 0), "x86", "en-US"));
            _deploymentWrapperMock.Setup(x => x.UpdateLocation)
                .Returns(new Uri("http://www.mytestdeploy.net"));

            var target = _sut.CreateDeploymentShortcut(new DirectoryInfo(_tempDir), "My Test-Link");

            File.Exists(target).Should().Be.True();
        }

        #endregion


    }
}