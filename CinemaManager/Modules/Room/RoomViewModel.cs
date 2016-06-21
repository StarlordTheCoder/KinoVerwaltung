// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CinemaManager.Infrastructure;
using CinemaManager.Model;

namespace CinemaManager.Modules.Room
{
	public class RoomViewModel : NotifyPropertyChangedBase
	{
		public RoomViewModel(RoomModel roomModel)
		{
			Model = roomModel;
			var groupedSeats = roomModel.Seats.GroupBy(r => r.Place.Row);
			foreach (var seats in groupedSeats)
			{
				Rows.Add(new RowViewModel(seats.Key, seats.Select(g => g)));
			}

			foreach (var seatViewModel in Rows.SelectMany(r => r.Seats))
			{
				seatViewModel.PropertyChanged += SeatViewModelOnPropertyChanged;
			}

			SelectedSeats = new ObservableCollection<SeatViewModel>(SelectedSeatModels);
		}

		private IEnumerable<SeatViewModel> SelectedSeatModels => Rows.SelectMany(r => r.Seats).Where(s => s.IsSelected);

		private void SeatViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			if (Equals(propertyChangedEventArgs.PropertyName, nameof(SeatViewModel.IsSelected)))
			{
				SelectedSeats.Clear();

				foreach (var seat in SelectedSeatModels)
				{
					SelectedSeats.Add(seat);
				}
			}
		}

		/// <summary>
		///     The currently selected Row
		/// </summary>
		public RowViewModel SelectedRow { get; set; }

		public ObservableCollection<RowViewModel> Rows { get; } = new ObservableCollection<RowViewModel>();

		public ObservableCollection<SeatViewModel> SelectedSeats { get; }

		public RoomModel Model { get; }

		/// <summary>
		///     Add a Row into a Room
		/// </summary>
		public void AddRow()
		{
			Rows.Add(new RowViewModel(1, new List<SeatModel>()));
		}

		/// <summary>
		///     Removes the selected row
		/// </summary>
		public void RemoveRow()
		{
			var seats = Model.Seats.Where(s => s.Place.Row == SelectedRow.RowNumber).ToList();
			seats.ForEach(s => Model.Seats.Remove(s));
			Rows.Remove(SelectedRow);
		}

		/// <summary>
		///     Adds a new Seat next to the selected seat
		/// </summary>
		public void AddSeat()
		{
		}

		/// <summary>
		///     Removes the selected seat
		/// </summary>
		public void RemoveSeat()
		{
		}
	}
}