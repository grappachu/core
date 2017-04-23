using System;
using System.Diagnostics;
using System.IO;

namespace Grappachu.Core.Environment.Keyboard
{
    /// <summary>
    ///     Gestore per la tastiera virtuale
    /// </summary>
    public static class OnScreenKeyboard
    {
        private const string WIN8_PROCESS_PATH_X64 = @"Microsoft Shared\ink\TabTip.exe";
        private const string WIN8_PROCESS_PATH_X86 = @"Microsoft Shared\ink\TabTip32.exe";
        private const string WIN8_HANDLE_NAME = "IPTIP_Main_Window";

        private const uint WM_SYSCOMMAND = 0x0112;
        private const uint SC_CLOSE = 0xF060;

        /// <summary>
        ///     Ottiene un valore che indica se la tastiera su schermo è al momento visualizzata
        /// </summary>
        public static bool IsVisible
        {
            get
            {
                IntPtr iHandle = NativeMethods.FindWindow(WIN8_HANDLE_NAME, "");
                return (iHandle != IntPtr.Zero);
            }
        }


        private static string GetProcessPath()
        {
            string root = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonProgramFiles);
            if (IntPtr.Size >= 8)
            {
                // 64-bit
                string vkX64 = Path.Combine(root, WIN8_PROCESS_PATH_X64);
                return vkX64;
            }
            // 32-bit  
            string vkX86 = Path.Combine(root, WIN8_PROCESS_PATH_X86);
            return vkX86;
        }

        /// <summary>
        ///     Visualizza la tastiera su schermo se non visibile
        /// </summary>
        public static bool Show()
        {
            // TODO: Il comando nativo di Show non sembra funzionare su tutti i dispositivi. 
            // Inoltre rilanciare il processo non sembra dare effetti collaterali (es. duplicazione dei processi)

            //var iHandle = NativeMethods.FindWindow(WIN8_HANDLE_NAME, "");
            //   if (iHandle == IntPtr.Zero)
            //{
            string vkPath = GetProcessPath();
            Process p = Process.Start(vkPath);
            return p != null;
            //}
            //NativeMethods.ShowWindow(iHandle, ShowWindowCommands.Show);
            //return true;
        }


        /// <summary>
        ///     Nasconde la tastiera su schermo se non visibile
        /// </summary>
        public static bool Hide()
        {
            IntPtr iHandle = NativeMethods.FindWindow(WIN8_HANDLE_NAME, "");
            if (iHandle != IntPtr.Zero)
            {
                // close the window using API        
                NativeMethods.SendMessage(iHandle, WM_SYSCOMMAND, new UIntPtr(SC_CLOSE), IntPtr.Zero);
                return true;
            }
            return false;
        }
    }
}