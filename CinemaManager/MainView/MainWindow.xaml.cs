// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

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

			var viewModel = new MainWindowViewModel(DockingManager);

			viewModel.LoadFile(startupFile);

			Window.CommandBindings.AddRange(viewModel.CommandBindings);

			DataContext = viewModel;
		}
	}
}