// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;

namespace CinemaManager.Filter.Date
{
	/// <summary>
	///     Interface for the Function to Filter Dates.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IDateFilter<in T> : IFilter<T>
	{
		/// <summary>
		///     Startdatum. Von-Datum
		/// </summary>
		DateTime? StartDate { get; set; }

		/// <summary>
		///     Enddatum. Bis-Datum
		/// </summary>
		DateTime? EndDate { get; set; }
	}
}