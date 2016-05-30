// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

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
		public MainWindow()
		{
			InitializeComponent();

			DataContext = new MainWindowViewModel(DockingManager);
		}
	}
}