using System;
using System.Globalization;
using System.Threading;

namespace Grappachu.Core.Preview.Runtime.Threading
{
    /// <summary>
    ///     Fornisce metodi per l'esecuzione di task
    /// </summary>
    public static class RuntimeHelper
    {
        /// <summary>
        ///     Runs a Action under a specified culture
        /// </summary>
        /// <param name="culture">Culture to run</param>
        /// <param name="task">task to perform under specified culture</param>
        public static void RunWithCulture(CultureInfo culture, Action task)
        {
            if (culture == null)
                throw new ArgumentNullException(nameof(culture));

            var originalCulture = new
            {
                Culture = Thread.CurrentThread.CurrentCulture,
                UICulture = Thread.CurrentThread.CurrentUICulture
            };

            try
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
                task();
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = originalCulture.Culture;
                Thread.CurrentThread.CurrentUICulture = originalCulture.UICulture;
            }
        }

        /// <summary>
        ///     Start an Action within an STA Thread
        /// </summary>
        /// <param name="goForIt"></param>
        public static void RunAsStaThread(Action goForIt)
        {
            var @event = new AutoResetEvent(false);
            var thread = new Thread(
                () =>
                {
                    goForIt();
                    @event.Set();
                });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            @event.WaitOne();
        }
    }
}