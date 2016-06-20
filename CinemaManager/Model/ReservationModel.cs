// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;

namespace CinemaManager.Model
{
	/// <summary>
	///     Enthält die Daten der Reservation für Serialisierung
	/// </summary>
	[Serializable]
	public class ReservationModel
	{
		/// <summary>
		///     Usermodel des Reservierers für Serialisierung
		/// </summary>
		public int ReservatorId { get; set; }

		/// <summary>
		///     Die Reihennummer, welche reserviert wurde
		/// </summary>
		public int RowNumber { get; set; }

		/// <summary>
		///     Die Sitznummer, welche reserviert wurde
		/// </summary>
		public int SeatNumber { get; set; }
	}
}