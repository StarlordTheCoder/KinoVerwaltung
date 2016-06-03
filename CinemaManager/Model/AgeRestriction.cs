// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;

namespace CinemaManager.Model
{
	/// <summary>
	///     Enum für die verfügbaren Alterseinschränkungen
	/// </summary>
	[Serializable]
	public enum AgeRestriction
	{
		/// <summary>
		///    Ohne Alterseinschränkungen
		/// </summary>
		None = 0,

		/// <summary>
		///     Freigegeben ab 6 Jahren
		/// </summary>
		Six = 6,

		/// <summary>
		///     Freigegeben ab 12 Jahren
		/// </summary>
		Twelve = 12,


		/// <summary>
		///     Freigegeben ab 16 Jahren
		/// </summary>
		Sixteen = 16,


		/// <summary>
		///     Freigegeben ab 18 Jahren
		/// </summary>
		Eighteen = 18
	}
}