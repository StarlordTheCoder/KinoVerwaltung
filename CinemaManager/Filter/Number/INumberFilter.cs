// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

namespace CinemaManager.Filter.Number
{
	/// <summary>
	///     Gibt die Möglichkeit nach Strings zu Filtern.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface INumberFilter<in T> : IFilter<T>
	{
		/// <summary>
		///     Zahl, nach welcher gefiltert wird
		/// </summary>
		int? Number { get; set; }
	}
}