// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;

namespace CinemaManager.Model
{
	/// <summary>
	/// Enthält die Sitztypen und deren Daten Serilaisierbar.
	/// </summary>
	[Serializable]
	public class SeatType
	{
		public static SeatType Basic;
		public static SeatType Double;
		public static SeatType Lounge;

		public int Capacity { get; set; }
		public double PriceAddition { get; set; }
	}
}