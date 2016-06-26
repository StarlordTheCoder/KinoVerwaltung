// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CinemaManager.Infrastructure;
using CinemaManager.Model;

namespace CinemaManager.Modules.Room
{
	/// <summary>
	///     ViewModel of the RoomModel
	/// </summary>
	public class RoomViewModel : NotifyPropertyChangedBase
	{
		private RowViewModel _selectedRow;
		private uint _maximumSelected;

		/// <summary>
		///     Contains methods and Datas of a room for the gui
		/// </summary>
		/// <param name="roomModel">Model of the Room</param>
		/// <param name="maximumSelected"><see cref="MaximumSelected"/></param>
		public RoomViewModel(RoomModel roomModel, uint maximumSelected = 0)
		{
			_maximumSelected = maximumSelected;
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
		}

		private IEnumerable<SeatViewModel> AllSelectedSeatModels => Rows.SelectMany(r => r.Seats).Where(s => s.IsSelected);

		/// <summary>
		///     Die maximale Anzahl der Sitze, welche gleichzeitig selektiert sein darf
		/// </summary>
		public uint MaximumSelected
		{
			get { return _maximumSelected; }
			set
			{
				if (_maximumSelected == value) return;
				_maximumSelected = value;
				OnPropertyChanged();
				RecalculateSelection();
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
		///     List of the Rows if the Room
		/// </summary>
		public ObservableCollection<RowViewModel> Rows { get; } = new ObservableCollection<RowViewModel>();

		/// <summary>
		///     Currently selected Seats in the GUI
		/// </summary>
		public ObservableCollection<SeatViewModel> SelectedSeats { get; } = new ObservableCollection<SeatViewModel>();

		/// <summary>
		///     Model of the Room
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
			var selectedSeat = SelectedRow.Seats.FirstOrDefault(s => s.IsSelected);

			int index;

			if (selectedSeat != null)
			{
				index = selectedSeat.Model.Place.Number + 1;
				foreach (var seat in SelectedRow.Seats.Where(s => s.Model.Place.Number >= index))
				{
					seat.Model.Place.Number++;
				}
			}
			else
			{
				index = SelectedRow.Seats.Any() ? SelectedRow.Seats.Max(s => s.Model.Place.Number) + 1 : 0;
			}

			var newModel = new SeatModel
			{
				Place = new SeatIdentifier
				{
					Row = SelectedRow.RowNumber,
					Number = index
				}
			};

			var newSeat = new SeatViewModel(newModel);

			newSeat.PropertyChanged += SeatViewModelOnPropertyChanged;

			SelectedRow.Seats.Insert(index, newSeat);
			Model.Seats.Add(newSeat.Model);
			SelectedSeats.Clear();
			newSeat.IsSelected = true;
		}

		/// <summary>
		///     Removes the selected seat
		/// </summary>
		public void RemoveSeat()
		{
			var seatToRemove = SelectedSeats.First();

			Model.Seats.Remove(seatToRemove.Model);
			seatToRemove.PropertyChanged -= SeatViewModelOnPropertyChanged;
			SelectedSeats.Remove(seatToRemove);
			foreach (var row in Rows)
			{
				row.Seats.Remove(seatToRemove);
			}
		}

		private void SeatViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			if (!_isUpdatingSelection && Equals(propertyChangedEventArgs.PropertyName, nameof(SeatViewModel.IsSelected)))
			{
				RecalculateSelection();
			}
		}

		private bool _isUpdatingSelection;

		private void RecalculateSelection()
		{
			_isUpdatingSelection = true;

			foreach (var seat in SelectedSeats.Where(s => !s.IsSelected).ToList())
			{
				SelectedSeats.Remove(seat);
			}

			foreach (var seat in AllSelectedSeatModels.Except(SelectedSeats).ToList())
			{
				SelectedSeats.Add(seat);
			}

			while (SelectedSeats.Count > 0 && SelectedSeats.Count > MaximumSelected)
			{
				var seatToRemove = SelectedSeats.First();
				seatToRemove.IsSelected = false;
				SelectedSeats.Remove(seatToRemove);
			}

			_isUpdatingSelection = false;
		}
	}
}