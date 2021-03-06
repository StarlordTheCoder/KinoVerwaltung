﻿// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;
using CinemaManager.MainView;
#if DEBUG
using CinemaManager.Metrics;

#endif

namespace CinemaManager
{
	/// <summary>
	///     Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		private void App_OnStartup(object sender, StartupEventArgs e)
		{
			FrameworkElement.LanguageProperty.OverrideMetadata(
				typeof(FrameworkElement),
				new FrameworkPropertyMetadata(
					XmlLanguage.GetLanguage(
						CultureInfo.CurrentCulture.IetfLanguageTag)));

			var startupFile =
				AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData?.FirstOrDefault(File.Exists) ??
				e.Args.FirstOrDefault(File.Exists);

			var view = new MainWindow(startupFile);

			view.Show();

#if DEBUG
			new MetricsManager().StartMetrics(view.DataContext as MainWindowViewModel);
#endif
		}

		private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			MessageBox.Show(e.Exception.StackTrace);
		}
	}
}