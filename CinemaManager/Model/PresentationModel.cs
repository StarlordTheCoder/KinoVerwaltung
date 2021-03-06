﻿// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;

namespace CinemaManager.Model
{
	/// <summary>
	///     Vorstellung
	/// </summary>
	[Serializable]
	public class PresentationModel
	{
		/// <summary>
		///     Die Zeit, wann die Vorstellung beginnt
		/// </summary>
		public DateTime StartTime { get; set; }

		/// <summary>
		///     Der Film
		/// </summary>
		public int FilmId { get; set; }

		/// <summary>
		///     Der Saal
		/// </summary>
		public int RoomNumber { get; set; }

		/// <summary>
		///     Die Reservationen für diese Vorstellung
		/// </summary>
		public List<ReservationModel> Reservations { get; set; } = new List<ReservationModel>();
	}
}