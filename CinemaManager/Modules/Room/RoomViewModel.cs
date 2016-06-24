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
	/// <summary>
	/// ViewModel of the RoomModel
	/// </summary>
	public class RoomViewModel : NotifyPropertyChangedBase, IRoomViewModel
	{
		private RowViewModel _selectedRow;

		/// <summary>
		/// Contains methods and Datas of a room for the gui
		/// </summary>
		/// <param name="roomModel">Model of the Room</param>
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
		public RowViewModel SelectedRow
		{
			get { return _selectedRow; }
			set
			{
				if (Equals(_selectedRow, value)) return;
				_selectedRow = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// List of the Rows if the Room
		/// </summary>
		public ObservableCollection<RowViewModel> Rows { get; } = new ObservableCollection<RowViewModel>();

		/// <summary>
		/// Currently selected Seats in the GUI
		/// </summary>
		public ObservableCollection<SeatViewModel> SelectedSeats { get; }

		/// <summary>
		/// Model of the Room
		/// </summary>
		public RoomModel Model { get; set; }

		/// <summary>
		///     Add a Row into a Room
		/// </summary>
		public void AddRow()
		{
			RowViewModel row;
			if (SelectedRow != null)
			{
				row = new RowViewModel(SelectedRow.RowNumber + 1, new List<SeatModel>());
				foreach (var r in Rows.Where(r => r.RowNumber > SelectedRow.RowNumber))
				{
					r.RowNumber++;
				}
			}
			else
			{
				row = new RowViewModel(Rows.Any() ? Rows.Max(r => r.RowNumber) + 1 : 1, new List<SeatModel>());
			}
			Rows.Add(row);
			SelectedRow = row;
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