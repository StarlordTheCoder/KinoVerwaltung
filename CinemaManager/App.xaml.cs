// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace CinemaManager
{
	/// <summary>
	///     Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		private void App_OnStartup(object sender, StartupEventArgs e)
		{
			var startupFile = e.Args.FirstOrDefault(File.Exists);

			new MainWindow(startupFile).Show();
		}

		private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			MessageBox.Show(e.Exception.StackTrace);
		}
	}
}