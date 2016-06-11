// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;

namespace CinemaManager.Filter
{
	/// <summary>
	///     Interface for every Filter
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IFilter<in T>
	{
		/// <summary>
		///     Ob der Filter aktiv ist
		/// </summary>
		bool IsEnabled { get; set; }

		/// <summary>
		///     Wird im GUI neben dem Filter angezeigt
		/// </summary>
		string Label { get; }

		/// <summary>
		///     Überprüft, ob die <paramref name="data" /> diesem Filter entsprechen.
		/// </summary>
		/// <param name="data">Daten, welche zu prüfen sind</param>
		/// <returns>True, wenn die Daten valid sind</returns>
		bool Check(T data);

		event EventHandler FilterChanged;
	}
}