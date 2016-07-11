// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;

namespace CinemaManager.Model
{
	/// <summary>
	///     Enthält die Sitztypen und deren Daten Serilaisierbar.
	/// </summary>
	[Serializable]
	public class SeatType
	{
		/// <summary>
		///     Die ID der Sitzart
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     Die Anzahl Personen, welche auf diesem Sitz platz haben
		/// </summary>
		public int Capacity { get; set; }

		/// <summary>
		///     Der Name, welcher im GUI angezeigt wird
		/// </summary>
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public string DisplayName { get; set; }

		/// <summary>
		///     Der Preis-Multiplikator. Beispielsweise 2 für einen Doppelsitz
		/// </summary>
		public double PriceMultiplicator { get; set; }
	}
}