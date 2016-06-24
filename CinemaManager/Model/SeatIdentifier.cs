// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;

namespace CinemaManager.Model
{
	/// <summary>
	/// Identifizierer des Sitzes, mit Hilfe von Reihe und Nummer
	/// </summary>
	[Serializable]
	public class SeatIdentifier
	{
		/// <summary>
		/// Nummer des Sitzes
		/// </summary>
		public int Number { get; set; }
		/// <summary>
		/// Reihe, in welcher sich der Sitz befindet
		/// </summary>
		public int Row { get; set; }
	}
}