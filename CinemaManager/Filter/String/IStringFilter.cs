// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

namespace CinemaManager.Filter.String
{
	/// <summary>
	///     Gibt die Möglichkeit nach Strings zu Filtern.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IStringFilter<in T> : IFilter<T>
	{
		/// <summary>
		///     Text, nach welchem gefiltert wird
		/// </summary>
		string Text { get; set; }
	}
}