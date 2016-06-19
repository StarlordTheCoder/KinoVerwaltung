// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CinemaManager.Infrastructure;
using CinemaManager.Model;

namespace CinemaManager.Modules.Room
{
	public class RoomViewModel
	{
		public RoomViewModel(RoomModel roomModel)
		{
			Model = roomModel;
			var groupedSeats = roomModel.Seats.GroupBy(r => r.Row);
			foreach (var seats in groupedSeats)
			{
				Rows.Add(new RowViewModel(seats.Key, seats.Select(g => g)));
			}
		}

		public ObservableCollection<RowViewModel> Rows { get; } = new ObservableCollection<RowViewModel>();

		public IList<SeatViewModel> SelectedSeats => Rows.SelectMany(r => r.Seats).Where(s => s.IsSelected).ToList();

		public RoomModel Model { get; }
	}

	public class RowViewModel
	{
		public RowViewModel(int rowNumber, IEnumerable<SeatModel> seats)
		{
			RowNumber = rowNumber;
			Seats = new ObservableCollection<SeatViewModel>(seats.Select(s => new SeatViewModel(s)));
		}

		public ObservableCollection<SeatViewModel> Seats { get; }
		private int RowNumber { get; }
	}

	public class SeatViewModel : NotifyPropertyChangedBase
	{
		private bool _isSelected;
		public SeatModel Model { get; }

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

		public SeatType SelectedSeatType
			=> Session.Instance.SelectedCinemaModel?.SeatTypes.First(s => s.Id == Model.SeatTypeId);

		public SeatViewModel(SeatModel model)
		{
			Model = model;
		}
	}
}