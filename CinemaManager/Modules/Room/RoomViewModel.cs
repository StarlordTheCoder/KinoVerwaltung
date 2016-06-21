﻿// CinemaManager created by Seraphin, Pascal & Alain as a school project
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
			var groupedSeats = roomModel.Seats.GroupBy(r => r.Place.Row);
			foreach (var seats in groupedSeats)
			{
				Rows.Add(new RowViewModel(seats.Key, seats.Select(g => g)));
			}
		}

		/// <summary>
		///     The currently selected Row
		/// </summary>
		public RowViewModel SelectedRow { get; set; }


		public ObservableCollection<RowViewModel> Rows { get; } = new ObservableCollection<RowViewModel>();

		public IList<SeatViewModel> SelectedSeats => Rows.SelectMany(r => r.Seats).Where(s => s.IsSelected).ToList();

		public RoomModel Model { get; }

		public bool ValueSelected => true;

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