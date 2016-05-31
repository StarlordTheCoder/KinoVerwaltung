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
		/// <param name="layoutToLoad"></param>
		public MainWindow(string layoutToLoad)
		{
			InitializeComponent();

			var viewModel = new MainWindowViewModel(DockingManager);

			if (!string.IsNullOrEmpty(layoutToLoad))
			{
				viewModel.LoadLayout(layoutToLoad);
			}

			DataContext = viewModel;
		}
	}
}