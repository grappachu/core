using System;
using System.Runtime.InteropServices;
using Grappachu.Core.Environment.Keyboard;

namespace Grappachu.Core.Environment
{
    /// <summary>
    ///     Native Methods Set
    /// </summary>
    internal class NativeMethods
    {
        /// <summary>
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("user32.dll", BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint msg, UIntPtr wParam, IntPtr lParam);

        /// <summary>
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);

        /// <summary>
        /// </summary>
        /// <param name="hhk"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnhookWindowsHookEx(IntPtr hhk);

        /// <summary>
        /// </summary>
        /// <param name="hhk"></param>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, UIntPtr wParam, IntPtr lParam);

        /// <summary>
        /// </summary>
        /// <param name="lpModuleName"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr GetModuleHandle(string lpModuleName);


        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //internal static extern IntPtr SetWindowsHookEx(int idHook, InterceptKeys.LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);


    }
}