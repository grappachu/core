using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Grappachu.Core.Preview.Environment.Keyboard;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Environment
{
    [TestFixture]
    class OnScreenKeyboardTest
    {

        [DllImport("user32.dll")]
        static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        static extern int SendMessage(int hWnd, uint msg, int wParam, int lParam);

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
            HideKeyboard();
        }

        [Test]
        public void Show_Should_Display_A_Keyboard()
        {
            var res = OnScreenKeyboard.Show();
            Thread.Sleep(1000); // Need some timeout to be displayed

            res.Should().Be.True();
            OnScreenKeyboard.IsVisible.Should().Be.True();
        }


        [Test]
        public void Show_Should_Do_Nothing_When_Keybord_Is_Already_Displayed()
        {
            ShowKeyboard();

            var res = OnScreenKeyboard.Show();
            Thread.Sleep(1000); // Need some timeout to be displayed

           // res.Should().Be.False();
            OnScreenKeyboard.IsVisible.Should().Be.True();
        }


        [Test]
        public void Should_Hide_A_Keyboard()
        {
            ShowKeyboard();

            var res = OnScreenKeyboard.Hide();
            Thread.Sleep(1000); // Need some timeout to be destroyed

            res.Should().Be.True();
            OnScreenKeyboard.IsVisible.Should().Be.False();
        }


        [Test]
        public void Hide_Should_Do_Nothing_When_Keybord_Is_Already_Hidden()
        {
            HideKeyboard();

            var res = OnScreenKeyboard.Hide();
            Thread.Sleep(1000); // Need some timeout to be destroyed

            res.Should().Be.False();
            OnScreenKeyboard.IsVisible.Should().Be.False();
        }


        private void ShowKeyboard()
        {
            var root = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonProgramFiles);
            var vkPath = Path.Combine(root, @"Microsoft Shared\ink\TabTip.exe");
            System.Diagnostics.Process.Start(vkPath);
            Thread.Sleep(2000); // Need some timeout to be displayed 
        }

        private void HideKeyboard()
        {
            // retrieve the handler of the window  
            int iHandle = FindWindow("IPTIP_Main_Window", "");
            if (iHandle > 0)
            {
                // close the window using API        
                SendMessage(iHandle, 0x0112, 0xF060, 0);
            }
            Thread.Sleep(1000); // Need some timeout to be destroyed
        }
    }


}
