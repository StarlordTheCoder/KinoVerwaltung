// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Linq;
using System.Windows.Media;
using CinemaManager.Infrastructure;
using CinemaManager.Model;

namespace CinemaManager.Modules.Room
{
	/// <summary>
	///     Contains Datas and methods of the seats for the GUI
	/// </summary>
	public class SeatViewModel : NotifyPropertyChangedBase
	{
		private static readonly SolidColorBrush NormalBrush = new SolidColorBrush(Colors.LightGray);
		private static readonly SolidColorBrush SelectedBrush = new SolidColorBrush(Colors.Aqua);
		private static readonly SolidColorBrush ReserverBrush = new SolidColorBrush(Colors.DarkRed);
		private static readonly SolidColorBrush SelectedAndReservedBrush = new SolidColorBrush(Colors.DarkOrange);
		private bool _isReserved;
		private bool _isSelected;

		/// <summary>
		///     Datas of the seat
		/// </summary>
		/// <param name="model">The Model of this Particular seat</param>
		public SeatViewModel(SeatModel model)
		{
			Model = model;
		}

		/// <summary>
		///     The Model of the Seat
		/// </summary>
		public SeatModel Model { get; }

		/// <summary>
		///     Returns wheter this seat is currently selected
		/// </summary>
		public bool IsSelected
		{
			get { return _isSelected; }
			set
			{
				if (_isSelected == value) return;
				_isSelected = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(BackgroundColor));
			}
		}

		/// <summary>
		///     Returns wheter this seat has a reservation or not
		/// </summary>
		public bool IsReserved
		{
			get { return _isReserved; }
			set
			{
				if (value == _isReserved) return;
				_isReserved = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(BackgroundColor));
			}
		}

		/// <summary>
		///     The Type of the selected Seat
		/// </summary>
		public SeatType SelectedSeatType
		{
			get { return Session.Instance.SelectedCinemaModel?.SeatTypes.FirstOrDefault(s => s.Id == Model.SeatTypeId); }
			set
			{
				if (Model.SeatTypeId == value.Id) return;
				Model.SeatTypeId = value.Id;
				OnPropertyChanged();
				OnPropertyChanged(nameof(Width));
			}
		}

		/// <summary>
		///     Grösse des Sitzes für Anzeig im GUI
		/// </summary>
		public double Width => (SelectedSeatType?.Capacity ?? 1)*30;

		/// <summary>
		///     Die Hintergrundfarbe. Abhängig ob reserviert oder selektiert
		/// </summary>
		public SolidColorBrush BackgroundColor
			=> IsSelected ? (IsReserved ? SelectedAndReservedBrush : SelectedBrush) : (IsReserved ? ReserverBrush : NormalBrush);
	}
}