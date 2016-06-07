// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

namespace CinemaManager.Filter
{
	/// <summary>
	/// Gibt die Möglichkeit nach Strings zu Filtern.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IStringFilter<in T> : IFilter<T>
	{
		string Text { get; set; }
	}
}