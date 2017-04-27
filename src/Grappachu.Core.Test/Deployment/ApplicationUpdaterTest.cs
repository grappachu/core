//using System;
//using System.Deployment.Application;
//using System.IO;
//using Grappachu.Core.Deployment;
//using Grappachu.Core.Environment;
//using Grappachu.Core.UI;
//using NUnit.Framework;
//using SharpTestsEx;

//namespace Grappachu.Core.Test.Deployment
//{
//    [TestFixture]
//    internal class ApplicationUpdaterTest
//    {
//        #region Init

//        [SetUp]
//        public void SetUp()
//        {
//            _dialogHolder = new Mock<IDialogHolder>();
//            _deploymentWrapper = new Mock<IApplicationDeployment>();
//            _applicationHelper = new Mock<IApplicationHelper>();

//            _sut = new ApplicationUpdater(_deploymentWrapper, _applicationHelper, _dialogHolder);
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            if (Directory.Exists(_tempDir))
//            {
//                Directory.Delete(_tempDir, true);
//            }
//        }

//        private ApplicationUpdater _sut;

//        private Mock<IDialogHolder> _dialogHolder;
//        private IApplicationDeployment _deploymentWrapper;
//        private IApplicationHelper _applicationHelper;
//        private string _tempDir;

//        #endregion

//        #region Tests

//        [Test]
//        [TestCase(false)]
//        [TestCase(true)]
//        public void Update_Should_CheckRemoteVersion_When_NetworkDeployed(bool userInteraction)
//        {
//            _sut.RequireUserInteraction = userInteraction;

//            _deploymentWrapper.Expect(x => x.IsNetworkDeployed).Return(true);

//            _sut.Update();

//            _deploymentWrapper.AssertWasCalled(x => x.CheckForDetailedUpdate());
//        }

//        [Test]
//        [TestCase(false)]
//        [TestCase(true)]
//        public void Update_Should_DoNothing_When_NotNetworkDeployed(bool userInteraction)
//        {
//            _sut.RequireUserInteraction = userInteraction;

//            _deploymentWrapper.Expect(x => x.IsNetworkDeployed).Return(false);

//            _sut.Update();

//            _deploymentWrapper.AssertWasNotCalled(x => x.Update());
//        }

//        [Test]
//        [TestCase(false)]
//        [TestCase(true)]
//        public void Update_Should_DoNothing_When_RunningLatestVersion(bool userInteraction)
//        {
//            _sut.RequireUserInteraction = userInteraction;

//            var updateCheckInfo = new ApplicationDeployUpdateInfo
//            {
//                UpdateAvailable = false,
//                AvailableVersion = new Version(1, 0, 2),
//                IsUpdateRequired = false,
//                MinimumRequiredVersion = new Version(1, 0, 2),
//                UpdateSizeBytes = 100000
//            };
//            _deploymentWrapper.Expect(x => x.IsNetworkDeployed).Return(true);
//            _deploymentWrapper.Expect(x => x.CheckForDetailedUpdate()).Return(updateCheckInfo);

//            _sut.Update();

//            _deploymentWrapper.AssertWasNotCalled(x => x.Update());
//        }

//        [Test]
//        [TestCase(false)]
//        [TestCase(true)]
//        public void Update_Should_FireAdEvent_After_Update(bool userInteraction)
//        {
//            _sut.RequireUserInteraction = userInteraction;

//            bool eventFired = false;

//            var updateCheckInfo = new ApplicationDeployUpdateInfo
//            {
//                UpdateAvailable = true,
//                AvailableVersion = new Version(1, 0, 3),
//                IsUpdateRequired = false,
//                MinimumRequiredVersion = new Version(1, 0, 2),
//                UpdateSizeBytes = 100000
//            };
//            _deploymentWrapper.Expect(x => x.IsNetworkDeployed).Return(true);
//            _deploymentWrapper.Expect(x => x.CheckForDetailedUpdate()).Return(updateCheckInfo);
//            // User Agreed
//            _dialogHolder.Stub(d => d.Question(Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<bool>.Is.Anything))
//                .Return(true);

//            _sut.Updated += (sender, args) =>
//            {
//                _deploymentWrapper.AssertWasCalled(x => x.Update());
//                eventFired = true;
//            };

//            _sut.Update();

//            eventFired.Should().Be.True();
//        }

//        [Test]
//        [TestCase(false)]
//        [TestCase(true)]
//        public void Update_Should_FireAdEvent_Before_Update(bool userInteraction)
//        {
//            _sut.RequireUserInteraction = userInteraction;

//            bool eventFired = false;
//            var updateCheckInfo = new ApplicationDeployUpdateInfo
//            {
//                UpdateAvailable = true,
//                AvailableVersion = new Version(1, 0, 3),
//                IsUpdateRequired = false,
//                MinimumRequiredVersion = new Version(1, 0, 2),
//                UpdateSizeBytes = 100000
//            };
//            _deploymentWrapper.Expect(x => x.IsNetworkDeployed).Return(true);
//            _deploymentWrapper.Expect(x => x.CheckForDetailedUpdate()).Return(updateCheckInfo);
//            // User Agreed
//            _dialogHolder.Stub(d => d.Question(Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<bool>.Is.Anything))
//                .Return(true);


//            _sut.Updating += (sender, args) =>
//            {
//                eventFired = true;
//                _deploymentWrapper.AssertWasNotCalled(x => x.Update());
//            };

//            _sut.Update();

//            eventFired.Should().Be.True();
//            _deploymentWrapper.AssertWasCalled(x => x.Update());
//        }

//        [Test]
//        [TestCase(false)]
//        [TestCase(true)]
//        public void Update_Should_RunUpdate_When_UpdateAvailable(bool userInteraction)
//        {
//            _sut.RequireUserInteraction = userInteraction;

//            var updateCheckInfo = new ApplicationDeployUpdateInfo
//            {
//                UpdateAvailable = true,
//                AvailableVersion = new Version(1, 0, 3),
//                IsUpdateRequired = false,
//                MinimumRequiredVersion = new Version(1, 0, 2),
//                UpdateSizeBytes = 100000
//            };
//            _deploymentWrapper.Expect(x => x.IsNetworkDeployed).Return(true);
//            _deploymentWrapper.Expect(x => x.CheckForDetailedUpdate()).Return(updateCheckInfo);

//            // User agreed
//            _dialogHolder.Stub(d => d.Question(Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<bool>.Is.Anything))
//                .Return(true);

//            _sut.Update();

//            _deploymentWrapper.AssertWasCalled(x => x.Update());
//        }


//        [Test]
//        public void Update_Should_AllowUserToRefuse_When_UserInteractive_Is_Set()
//        {
//            _sut.RequireUserInteraction = true;
//            var updateCheckInfo = new ApplicationDeployUpdateInfo
//            {
//                UpdateAvailable = true,
//                AvailableVersion = new Version(1, 0, 3),
//                IsUpdateRequired = false,
//                MinimumRequiredVersion = new Version(1, 0, 2),
//                UpdateSizeBytes = 100000
//            };
//            _deploymentWrapper.Expect(x => x.IsNetworkDeployed).Return(true);
//            _deploymentWrapper.Expect(x => x.CheckForDetailedUpdate()).Return(updateCheckInfo);

//            // User Refused
//            _dialogHolder.Stub(d => d.Question(Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<bool>.Is.Anything))
//                .Return(false);

//            _sut.Update();

//            _deploymentWrapper.AssertWasNotCalled(x => x.Update());
//        }

//        [Test]
//        [TestCase(false, false)]
//        [TestCase(false, true)]
//        [TestCase(true, false)]
//        [TestCase(true, true)]
//        public void Update_Should_RestartApplication_OnlyWhen_Restart_Is_Set(bool userInteraction, bool restartAfterUpdate)
//        {
//            _sut.RequireUserInteraction = userInteraction;
//            _sut.RestartAfterUpdate = restartAfterUpdate;

//            var updateCheckInfo = new ApplicationDeployUpdateInfo
//            {
//                UpdateAvailable = true,
//                AvailableVersion = new Version(1, 0, 3),
//                IsUpdateRequired = false,
//                MinimumRequiredVersion = new Version(1, 0, 2),
//                UpdateSizeBytes = 100000
//            };
//            _deploymentWrapper.Expect(x => x.IsNetworkDeployed).Return(true);
//            _deploymentWrapper.Expect(x => x.CheckForDetailedUpdate()).Return(updateCheckInfo);
//            // User agreed
//            _dialogHolder.Stub(d => d.Question(Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<bool>.Is.Anything))
//                .Return(true);
//            _deploymentWrapper.Expect(x => x.Update()).Return(true);


//            _sut.Update();

//            if (restartAfterUpdate)
//            {
//                _applicationHelper.AssertWasCalled(x => x.Restart());
//            }
//            else
//            {
//                _applicationHelper.AssertWasNotCalled(x => x.Restart());
//            }
//        }

//        [Test]
//        [TestCase(false)]
//        [TestCase(true)]
//        public void Update_Should_FireEvent_OnlyWhen_UserInteractive_Is_Not_Set(bool userInteraction)
//        {
//            _sut.RequireUserInteraction = userInteraction;

//            bool eventFired = false;

//            var updateCheckInfo = new ApplicationDeployUpdateInfo
//            {
//                UpdateAvailable = true,
//                AvailableVersion = new Version(1, 0, 3),
//                IsUpdateRequired = false,
//                MinimumRequiredVersion = new Version(1, 0, 2),
//                UpdateSizeBytes = 100000
//            };
//            _deploymentWrapper.Expect(x => x.IsNetworkDeployed).Return(true);
//            _deploymentWrapper.Expect(x => x.CheckForDetailedUpdate()).Return(updateCheckInfo);
//            // User Agreed
//            _dialogHolder.Stub(d => d.Question(Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<bool>.Is.Anything))
//                .Return(true);

//            _deploymentWrapper.Stub(x => x.Update()).Throw(new InvalidDeploymentException());

//            _sut.Error += (sender, args) =>
//            {
//                eventFired = true;
//            };

//            _sut.Update();


//            eventFired.Should().Be.EqualTo(!userInteraction);
//        }

//        [Test]
//        [TestCase(false)]
//        [TestCase(true)]
//        public void Update_Should_Set_LastCheck_Date(bool userInteraction)
//        {
//            _sut.RequireUserInteraction = userInteraction;
//            _sut.LastChecked.Should().Be.LessThan(DateTime.Now);

//            var updateCheckInfo = new ApplicationDeployUpdateInfo
//            {
//                UpdateAvailable = true,
//                AvailableVersion = new Version(1, 0, 3),
//                IsUpdateRequired = false,
//                MinimumRequiredVersion = new Version(1, 0, 2),
//                UpdateSizeBytes = 100000
//            };
//            _deploymentWrapper.Expect(x => x.IsNetworkDeployed).Return(true);
//            _deploymentWrapper.Expect(x => x.CheckForDetailedUpdate()).Return(updateCheckInfo);

//            // User agreed
//            _dialogHolder.Stub(d => d.Question(Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<bool>.Is.Anything))
//                .Return(true);

//            _sut.Update();

//            _sut.LastChecked.Should().Be.IncludedIn(DateTime.Now.AddSeconds(-1), DateTime.Now);
//        }

//        [Test]
//        public void CreateDeploymentShortcut_Should_Write_A_Shortcut()
//        {
//            _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
//            Directory.CreateDirectory(_tempDir);

//            _deploymentWrapper.Expect(x => x.IsNetworkDeployed).Return(true);
//            _deploymentWrapper.Expect(x => x.GetDeploymentInfo())
//                .Return(new ApplicationId(new byte[0], "TestApp", new Version(1, 0), "x86", "en-US"));
//            _deploymentWrapper.Expect(x => x.UpdateLocation).Return(new Uri("http://www.mytestdeploy.net"));

//            var target = _sut.CreateDeploymentShortcut(new DirectoryInfo(_tempDir), "My Test-Link");

//            File.Exists(target).Should().Be.True();
//        }

//        #endregion


//    }
//}