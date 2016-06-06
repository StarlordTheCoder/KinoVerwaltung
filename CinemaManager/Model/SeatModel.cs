// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;

namespace CinemaManager.Model
{
	/// <summary>
	///     Platz
	/// </summary>
	[Serializable]
	public class SeatModel
	{
		/// <summary>
		///     Reihe
		/// </summary>
		public int Row { get; set; }

		/// <summary>
		///     Platznummer
		/// </summary>
		public int Number { get; set; }

		/// <summary>
		///     Platzart. Beispielsweise Sofa
		/// </summary>
		public SeatType SeatType { get; set; }
	}
}