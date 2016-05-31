// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.IO;

namespace CinemaManager
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

			DataContext = viewModel;
		}
	}
}