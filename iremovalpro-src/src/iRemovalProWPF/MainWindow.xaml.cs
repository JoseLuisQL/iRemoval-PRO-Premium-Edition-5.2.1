using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace iRemovalProWPF
{
    /// <summary>
    /// MainWindow limpia reconstruida a mano. Conserva los 18 controles y los
    /// handlers del launcher original, pero sin la VM de ConfuserEx (C834A786).
    ///
    /// Handlers conocidos (recuperados de los closures del MainWindow.cs original):
    ///   Sn_MouseDown     → Library.Action(5)   (click en label Serial Number)
    ///   Imei_MouseDown   → Library.Action(6)   (click en label IMEI)
    ///   Button_Click_5   → Library.Action(9)   (click en checkrainButt — botón principal)
    ///
    /// Los demas handlers son stubs que logean a Debug (el comportamiento real
    /// estaba dentro de C834A786._3CB74B1B(args, magicNumber) y no se pudo
    /// recuperar estaticamente).
    ///
    /// Para que la app sea funcional, copiar el iremovalpro.dll ORIGINAL (33 MB)
    /// al directorio del .exe. Sin eso, las llamadas a Library.Action tiran
    /// DllNotFoundException al primer click.
    /// </summary>
    public partial class MainWindow : Window
    {
        private Library.FormCallback _callback;
        private static readonly bool _engineAvailable = CheckEngineAvailable();

        public MainWindow()
        {
            App.LogWrite("MainWindow: constructor start");
            InitializeComponent();
            App.LogWrite("MainWindow: InitializeComponent done");
            Loaded += MainWindow_Loaded;
            App.LogWrite("MainWindow: constructor end");
        }

        private static bool CheckEngineAvailable()
        {
            try
            {
                var dllPath = System.IO.Path.Combine(AppContext.BaseDirectory, "iremovalpro.dll");
                if (!System.IO.File.Exists(dllPath))
                {
                    App.LogWrite($"CheckEngineAvailable: DLL not found at {dllPath}");
                    return false;
                }
                App.LogWrite($"CheckEngineAvailable: DLL found at {dllPath} ({new System.IO.FileInfo(dllPath).Length} bytes)");
                return true;
            }
            catch (Exception ex)
            {
                App.LogWrite($"CheckEngineAvailable: exception {ex.Message}");
                return false;
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            App.LogWrite($"MainWindow_Loaded: _engineAvailable={_engineAvailable}");

            if (!_engineAvailable)
            {
                UpdateStatus("iremovalpro.dll no encontrada. Copia el DLL original (33 MB) al lado del .exe.");
                App.LogWrite("MainWindow_Loaded: engine not available, status updated, returning");
                return;
            }

            try
            {
                App.LogWrite("MainWindow_Loaded: creating FormCallback delegate");
                _callback = new Library.FormCallback(OnEngineCallback);
                App.LogWrite("MainWindow_Loaded: calling SetCallbacks");
                Library.SetCallbacks(_callback);
                App.LogWrite("MainWindow_Loaded: SetCallbacks returned OK");
                App.LogWrite("MainWindow_Loaded: calling SetWinInfo");
                Library.SetWinInfo("iRemoval PRO Premium", "5.2.1", "Blackhound 0.7.1", "@2022");
                App.LogWrite("MainWindow_Loaded: SetWinInfo returned OK");
                UpdateStatus("Listo. Conecta un iPhone y presiona checkra1n.");
                App.LogWrite("MainWindow_Loaded: complete");
            }
            catch (Exception ex)
            {
                App.LogWrite($"MainWindow_Loaded: EXCEPTION {ex.GetType().Name}: {ex.Message}");
                App.LogWrite(ex.StackTrace);
                UpdateStatus($"Error al inicializar engine: {ex.Message}");
            }
        }

        private void OnEngineCallback(int action, string key, string value)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Debug.WriteLine($"[engine] action={action} key={key ?? "<null>"} value={value ?? "<null>"}");
                UpdateStatus($"engine: action={action} {key}={value}");
            }));
        }

        // ===== Handlers conocidos (recuperados del original) =====

        private void Button_Click_5(object sender, RoutedEventArgs e) => InvokeEngineAction(9, "checkra1n");

        private void Sn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) InvokeEngineAction(5, "sn");
        }

        private void Imei_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) InvokeEngineAction(6, "imei");
        }

        // ===== Handlers stub (comportamiento real dentro de la VM de ConfuserEx) =====

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("[stub] Button_Click — original era C834A786._3CB74B1B(args, 21446)");
            UpdateStatus("Boton 'Jailbreak' presionado (handler stub).");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("[stub] Button_Click_1 — original era C834A786._3CB74B1B(args, 272778)");
            UpdateStatus("Boton 'iRa1n' presionado (handler stub).");
        }

        private void TopImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("[stub] TopImage_MouseDown — original era C834A786._3CB74B1B(args, 306378)");
        }

        private void QrImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("[stub] QrImage_MouseDown — original era C834A786._3CB74B1B(args, 281357)");
            UpdateStatus("QR click (handler stub).");
        }

        private void Activate_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("[stub] Activate_Click — original era C834A786._3CB74B1B(args, 297829)");
            UpdateStatus("Activate (handler stub).");
        }

        private void Erase_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("[stub] Erase_Click — original era C834A786._3CB74B1B(args, 279625)");
            UpdateStatus("Erase (handler stub).");
        }

        // ===== Helpers =====

        private void InvokeEngineAction(int action, string label)
        {
            if (!_engineAvailable)
            {
                UpdateStatus("iremovalpro.dll no cargada — copia el DLL original al lado del .exe.");
                return;
            }
            try
            {
                UpdateStatus($"Invocando engine action {action} ({label})...");
                progress.Value = 10;
                Library.Action(action);
                UpdateStatus($"Engine action {action} invocada.");
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error en Action({action}): {ex.Message}");
            }
        }

        private void UpdateStatus(string text)
        {
            if (scanText != null) scanText.Content = text;
            Debug.WriteLine($"[ui] {text}");
        }
    }
}
