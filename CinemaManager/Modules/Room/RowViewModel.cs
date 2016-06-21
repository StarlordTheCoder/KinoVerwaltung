// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CinemaManager.Model;

namespace CinemaManager.Modules.Room
{
	/// <summary>
	///     VIewmodel of the Rows
	/// </summary>
	public class RowViewModel
	{
		/// <summary>
		///     Viewmodel of the Row
		/// </summary>
		/// <param name="rowNumber">Number of the Row</param>
		/// <param name="seats">List of Seats in this row</param>
		public RowViewModel(int rowNumber, IEnumerable<SeatModel> seats)
		{
			RowNumber = rowNumber;
			Seats = new ObservableCollection<SeatViewModel>(seats.Select(s => new SeatViewModel(s)));
		}

		public ObservableCollection<SeatViewModel> Seats { get; }
		private int RowNumber { get; }
	}
}