using System.Windows;

namespace iRemovalProWPF
{
    /// <summary>
    /// App entry point. Reconstruido limpio desde App.cs decompilado.
    /// El original llamaba a _31335E26.E794E8A4(app) que era el setup obfuscado
    /// (registraba SetCallbacks en el engine). Esta version llama a SetCallbacks
    /// directamente desde MainWindow.OnLoaded, que es mas simple y compila limpio.
    /// </summary>
    public partial class App : Application
    {
    }
}
