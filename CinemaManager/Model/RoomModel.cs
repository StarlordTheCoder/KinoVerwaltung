// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;

namespace CinemaManager.Model
{
	/// <summary>
	///     Ein Saal
	/// </summary>
	[Serializable]
	public class RoomModel
	{
		/// <summary>
		///     Die Saalnummer
		/// </summary>
		public int RoomNumber { get; set; }

		/// <summary>
		///     Die Plätze im Saal
		/// </summary>
		public List<SeatModel> Seats { get; set; }

		/// <summary>
		/// Looks if Room is Selectet in List
		/// </summary>
		public bool IsActive { get; set; }
	}
}