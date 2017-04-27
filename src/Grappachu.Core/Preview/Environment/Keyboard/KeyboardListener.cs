using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace Grappachu.Core.Preview.Environment.Keyboard
{
    /// <summary>
    /// Listens keyboard globally. 
    /// <remarks>Uses WH_KEYBOARD_LL.</remarks>
    /// </summary>
    /// <example>
    /// public partial class App : Application
    /// {
    ///    KeyboardListener KListener = new KeyboardListener();
    ///
    ///    private void Application_Startup(object sender, StartupEventArgs e)
    ///    {
    ///        KListener.KeyDown += new RawKeyEventHandler(KListener_KeyDown);
    ///    }
    ///
    ///    void KListener_KeyDown(object sender, RawKeyEventArgs args)
    ///    {
    ///        Console.WriteLine(args.Key.ToString());
    ///    }
    ///
    ///    private void Application_Exit(object sender, ExitEventArgs e)
    ///    {
    ///        KListener.Dispose();
    ///    }
    /// }
    /// </example>
    public class KeyboardListener : IDisposable
    {
        /// <summary>
        /// Creates global keyboard listener.
        /// </summary>
        public KeyboardListener()
        {
            // We have to store the HookCallback, so that it is not garbage collected runtime

            // Set the hook
            _hookId = InterceptKeys.SetHook(LowLevelKeyboardProc);

            // Assign the asynchronous callback event
            _hookedKeyboardCallbackAsync = KeyboardListener_KeyboardCallbackAsync;
        }

        /// <summary>
        /// Destroys global keyboard listener.
        /// </summary>
        ~KeyboardListener()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        /// <summary>
        /// Fired when any of the keys is pressed down.
        /// </summary>
        public event RawKeyEventHandler KeyDown;

        /// <summary>
        /// Fired when any of the keys is released.
        /// </summary>
        public event RawKeyEventHandler KeyUp;


        

        #region Inner workings
        /// <summary>
        /// Hook ID
        /// </summary>
        private readonly IntPtr _hookId = IntPtr.Zero;

        /// <summary>
        /// Asynchronous callback hook.
        /// </summary>
        private delegate void KeyboardCallbackAsync(InterceptKeys.KeyEvent keyEvent, int vkCode);

        /// <summary>
        /// Actual callback hook.
        /// 
        /// <remarks>Calls asynchronously the asyncCallback.</remarks>
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        private IntPtr LowLevelKeyboardProc(int nCode, UIntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
                if (wParam.ToUInt32() == (int)InterceptKeys.KeyEvent.WM_KEYDOWN ||
                    wParam.ToUInt32() == (int)InterceptKeys.KeyEvent.WM_KEYUP ||
                    wParam.ToUInt32() == (int)InterceptKeys.KeyEvent.WM_SYSKEYDOWN ||
                    wParam.ToUInt32() == (int)InterceptKeys.KeyEvent.WM_SYSKEYUP)
                    _hookedKeyboardCallbackAsync.BeginInvoke((InterceptKeys.KeyEvent)wParam.ToUInt32(), Marshal.ReadInt32(lParam), null, null);

            return NativeMethods.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        /// <summary>
        /// Event to be invoked asynchronously (BeginInvoke) each time key is pressed.
        /// </summary>
        private readonly KeyboardCallbackAsync _hookedKeyboardCallbackAsync;

        /// <summary>
        /// HookCallbackAsync procedure that calls accordingly the KeyDown or KeyUp events.
        /// </summary>
        /// <param name="keyEvent">Keyboard event</param>
        /// <param name="vkCode">VKCode</param>
        void KeyboardListener_KeyboardCallbackAsync(InterceptKeys.KeyEvent keyEvent, int vkCode)
        {
            switch (keyEvent)
            {
                // KeyDown events
                case InterceptKeys.KeyEvent.WM_KEYDOWN:
                    if (KeyDown != null)
                        KeyDown(this, new RawKeyEventArgs(vkCode, false));
                    break;
                case InterceptKeys.KeyEvent.WM_SYSKEYDOWN:
                    if (KeyDown != null)
                        KeyDown(this, new RawKeyEventArgs(vkCode, true));
                    break;

                // KeyUp events
                case InterceptKeys.KeyEvent.WM_KEYUP:
                    if (KeyUp != null)
                        KeyUp(this, new RawKeyEventArgs(vkCode, false));
                    break;
                case InterceptKeys.KeyEvent.WM_SYSKEYUP:
                    if (KeyUp != null)
                        KeyUp(this, new RawKeyEventArgs(vkCode, true));
                    break;
            }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Disposes the hook.
        /// <remarks>This call is required as it calls the UnhookWindowsHookEx.</remarks>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///  The bulk of the clean-up code is implemented in Dispose(bool)
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                NativeMethods.UnhookWindowsHookEx(_hookId);
            }
        }

        #endregion
    }

    /// <summary>
    /// Raw KeyEvent arguments.
    /// </summary>
    public class RawKeyEventArgs : EventArgs
    {

       
        /// <summary>
        /// VKCode of the key.
        /// </summary>
        public int VkCode;

        /// <summary>
        /// WPF Key of the key.
        /// </summary>
        public Key Key;

        /// <summary>
        /// Is the hitted key system key.
        /// </summary>
        public bool IsSysKey;

        /// <summary>
        /// Create raw keyevent arguments.
        /// </summary>
        /// <param name="vkCode"></param>
        /// <param name="isSysKey"></param>
        public RawKeyEventArgs(int vkCode, bool isSysKey)
        {
            VkCode = vkCode;
            IsSysKey = isSysKey;
            Key = KeyInterop.KeyFromVirtualKey(vkCode);
        }
    }

    /// <summary>
    /// Raw keyevent handler.
    /// </summary>
    /// <param name="sender">sender</param>
    /// <param name="e">raw keyevent arguments</param>
    public delegate void RawKeyEventHandler(object sender, RawKeyEventArgs e);

   
    #region WINAPI Helper class

    /// <summary>
    /// Winapi Key interception helper class.
    /// </summary>
    internal static class InterceptKeys
    {
        public delegate IntPtr LowLevelKeyboardProc(int nCode, UIntPtr wParam, IntPtr lParam);

        // ReSharper disable InconsistentNaming       

        public static int WH_KEYBOARD_LL = 13;

        public enum KeyEvent
        {
            WM_KEYDOWN = 256,
            WM_KEYUP = 257,
            WM_SYSKEYUP = 261,
            WM_SYSKEYDOWN = 260
        }

        // ReSharper restore InconsistentNaming

        public static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return NativeMethods.SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    NativeMethods.GetModuleHandle(curModule.ModuleName), 0);
            }
        }

      
    
    }

    #endregion

}