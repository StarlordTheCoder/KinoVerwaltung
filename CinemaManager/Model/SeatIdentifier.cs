// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;

namespace CinemaManager.Model
{
	/// <summary>
	///     Identifizierer des Sitzes, mit Hilfe von Reihe und Nummer
	/// </summary>
	[Serializable]
	public class SeatIdentifier
	{
		/// <summary>
		///     Nummer des Sitzes
		/// </summary>
		public int Number { get; set; }

		/// <summary>
		///     Reihe, in welcher sich der Sitz befindet
		/// </summary>
		public int Row { get; set; }

		private bool Equals(SeatIdentifier other)
		{
			return Number == other.Number && Row == other.Row;
		}

		/// <summary>Bestimmt, ob das angegebene Objekt mit dem aktuellen Objekt identisch ist.</summary>
		/// <returns>true, wenn das angegebene Objekt und das aktuelle Objekt gleich sind, andernfalls false.</returns>
		/// <param name="obj">Das Objekt, das mit dem aktuellen Objekt verglichen werden soll. </param>
		/// <filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((SeatIdentifier) obj);
		}

		/// <summary>Fungiert als die Standardhashfunktion. </summary>
		/// <returns>Ein Hashcode für das aktuelle Objekt.</returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			unchecked
			{
				return (Number*397) ^ Row;
			}
		}
	}
}