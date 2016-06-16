// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
		public RoomModel Model { get; }
	}

	public class RowViewModel
	{
		public RowViewModel(int rowNumber, IEnumerable<SeatModel> seats)
		{
			RowNumber = rowNumber;
			Seats = new ObservableCollection<SeatModel>(seats);
		}

		public ObservableCollection<SeatModel> Seats { get; }
		private int RowNumber { get; }
	}
}