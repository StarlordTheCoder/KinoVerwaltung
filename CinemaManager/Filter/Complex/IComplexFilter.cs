// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using CinemaManager.Modules;

namespace CinemaManager.Filter.Complex
{
	/// <summary>
	///     Erlaubt Modulübergreifende Filter
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TM">Modul</typeparam>
	public interface IComplexFilter<in T, out TM> : IFilter<T> where TM : IModule
	{
		/// <summary>
		///     Referenz zum Modul, welches gefilter wird
		/// </summary>
		TM Module { get; }
	}
}