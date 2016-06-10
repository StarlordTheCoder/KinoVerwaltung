// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;

namespace CinemaManager.Filter
{
	/// <summary>
	///     Interface for the Function to Filter Dates.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IDateFilter<in T> : IFilter<T>
	{
		DateTime? StartDate { get; set; }
		DateTime? EndDate { get; set; }
	}
}