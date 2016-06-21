// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Linq;
using CinemaManager.Infrastructure;
using CinemaManager.Model;

namespace CinemaManager.Modules.Room
{
	/// <summary>
	///     Contains Datas and methods of the seats for the GUI
	/// </summary>
	public class SeatViewModel : NotifyPropertyChangedBase
	{
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
		///     Returns if this seat is currently selected
		/// </summary>
		public bool IsSelected
		{
			get { return _isSelected; }
			set
			{
				if (_isSelected == value) return;
				_isSelected = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		///     The Type of the selected Seat
		/// </summary>
		public SeatType SelectedSeatType
			=> Session.Instance.SelectedCinemaModel?.SeatTypes.First(s => s.Id == Model.SeatTypeId);
	}
}