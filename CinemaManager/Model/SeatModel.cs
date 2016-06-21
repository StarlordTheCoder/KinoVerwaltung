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
		///     Die Reihe und Sitznummer
		/// </summary>
		public SeatIdentifier Place { get; set; }

		/// <summary>
		///     Platzart. Beispielsweise Sofa
		/// </summary>
		public int SeatTypeId { get; set; }
	}
}