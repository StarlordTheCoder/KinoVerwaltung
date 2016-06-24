// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Windows.Input;

namespace CinemaManager.Modules.Room
{
	/// <summary>
	///     Interaction logic for SeatControl.xaml
	/// </summary>
	public partial class SeatControl
	{
		/// <summary>
		/// Initialisiert das Usercontrol des Sitzes
		/// </summary>
		public SeatControl()
		{
			InitializeComponent();
		}

		private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var seat = DataContext as SeatViewModel;

			if (seat != null)
			{
				seat.IsSelected = !seat.IsSelected;
			}
		}
	}
}