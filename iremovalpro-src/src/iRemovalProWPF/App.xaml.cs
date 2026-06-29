using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace iRemovalProWPF
{
    /// <summary>
    /// App entry point con manejo global de excepciones y logging a archivo.
    ///
    /// Si la app crashea silenciosamente (PowerShell vuelve al prompt sin mostrar
    /// ventana), revisar crash.log al lado del .exe para ver el stack trace.
    ///
    /// El original decompilado llamaba a _31335E26.E794E8A4(app) que era el setup
    /// obfuscado (registraba SetCallbacks en el engine). Esta version llama a
    /// SetCallbacks directamente desde MainWindow.OnLoaded.
    /// </summary>
    public partial class App : Application
    {
        private static readonly string LogPath =
            Path.Combine(AppContext.BaseDirectory, "crash.log");

        public App()
        {
            // Capturar excepciones no manejadas en el UI thread
            DispatcherUnhandledException += App_DispatcherUnhandledException;

            // Capturar excepciones no manejadas en cualquier thread
            AppDomain.CurrentDomain.UnhandledException += App_DomainUnhandledException;

            // Capturar tareas que fallaron y nadie observo
            TaskScheduler.UnobservedTaskException += App_UnobservedTaskException;

            LogWrite("=== iRemovalProWPF startup ===");
            LogWrite($"Runtime: {Environment.Version}");
            LogWrite($"OS: {Environment.OSVersion}");
            LogWrite($"64-bit: {Environment.Is64BitProcess}");
            LogWrite($"BaseDirectory: {AppContext.BaseDirectory}");
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            LogWrite($"[DispatcherUnhandledException] {e.Exception.GetType().Name}: {e.Exception.Message}");
            LogWrite(e.Exception.StackTrace);
            MessageBox.Show(
                $"Error en UI thread:\n\n{e.Exception.Message}\n\nVer crash.log para detalle.",
                "iRemovalProWPF — Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            e.Handled = true; // no matar la app
        }

        private void App_DomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            LogWrite($"[AppDomain.UnhandledException] IsTerminating={e.IsTerminating}");
            if (ex != null)
            {
                LogWrite($"  {ex.GetType().Name}: {ex.Message}");
                LogWrite(ex.StackTrace);
            }
            else
            {
                LogWrite($"  ExceptionObject: {e.ExceptionObject}");
            }
        }

        private void App_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            LogWrite($"[UnobservedTaskException] {e.Exception.GetType().Name}: {e.Exception.Message}");
            LogWrite(e.Exception.StackTrace);
            e.SetObserved();
        }

        public static void LogWrite(string line)
        {
            try
            {
                var stamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                File.AppendAllText(LogPath, $"[{stamp}] {line}{Environment.NewLine}");
                Debug.WriteLine($"[log] {line}");
            }
            catch { /* no romper si no se puede loguear */ }
        }
    }
}
