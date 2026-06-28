using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;

namespace iRemovalProWPF;

public class App : Application
{
	private bool _contentLoaded;

	[GeneratedCode("PresentationBuildTasks", "8.0.10.0")]
	[DebuggerNonUserCode]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			C8BFB49E._81BBAF2C[31](this, C8BFB49E._81BBAF2C[62]("MainWindow.xaml", UriKind.Relative));
			Uri a4A366BC = C8BFB49E._81BBAF2C[62]("/iRemovalProWPF;component/app.xaml", UriKind.Relative);
			C8BFB49E._81BBAF2C[1](this, a4A366BC);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "8.0.10.0")]
	[STAThread]
	public static void Main()
	{
		App app = new App();
		app.InitializeComponent();
		_31335E26.E794E8A4(app);
	}

	public App()
	{
		C8BFB49E._81BBAF2C[34](this);
	}
}
