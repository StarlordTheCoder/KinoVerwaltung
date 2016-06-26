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
		private int _rowNumber;

		/// <summary>
		///     Viewmodel of the Row
		/// </summary>
		/// <param name="rowNumber">Number of the Row</param>
		/// <param name="seats">List of Seats in this row</param>
		public RowViewModel(int rowNumber, IEnumerable<SeatModel> seats)
		{
			_rowNumber = rowNumber;
			Seats = new ObservableCollection<SeatViewModel>(seats.Select(s => new SeatViewModel(s)));
		}

		/// <summary>
		///     Liste der <see cref="SeatViewModel" /> in einer Reihe
		/// </summary>
		public ObservableCollection<SeatViewModel> Seats { get; }

		/// <summary>
		///     Nummer/Position der Reihe
		/// </summary>
		public int RowNumber
		{
			get { return _rowNumber; }
			set
			{
				_rowNumber = value;
				foreach (var seat in Seats)
				{
					seat.Model.Place.Row = _rowNumber;
				}
			}
		}
	}
}