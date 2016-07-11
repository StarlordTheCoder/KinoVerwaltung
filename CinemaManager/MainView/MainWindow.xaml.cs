// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.ComponentModel;
using System.Windows;

namespace CinemaManager.MainView
{
	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		/// <summary>
		///     Constructor
		/// </summary>
		/// <param name="startupFile"></param>
		public MainWindow(string startupFile)
		{
			InitializeComponent();

			var viewModel = new MainWindowViewModel(startupFile);

			Window.CommandBindings.AddRange(viewModel.CommandBindings);

			DataContext = viewModel;
		}

		/// <summary>
		///     Verzögertes Laden aufgrund des Dockingmanagers
		/// </summary>
		/// <param name="sender">Unused</param>
		/// <param name="e">Unused</param>
		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			(DataContext as MainWindowViewModel)?.LayoutService.Initialize(DockingManager);
		}

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			(DataContext as MainWindowViewModel)?.Exit(e);
		}
	}
}