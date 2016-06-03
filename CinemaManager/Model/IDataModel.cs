// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

namespace CinemaManager.Model
{
	/// <summary>
	///     Stellt Daten (<see cref="CinemasModel"/>) zusammen mit einer <see cref="Load"/> und <see cref="Save"/> Methode an.
	/// </summary>
	public interface IDataModel
	{
		/// <summary>
		/// Daten
		/// </summary>
		CinemasModel CinemasModel { get; set; }

		/// <summary>
		/// Speichert die Daten (<see cref="CinemasModel"/>) ab
		/// </summary>
		void Save();

		/// <summary>
		/// Lädt die Daten (<see cref="CinemasModel"/>)
		/// </summary>
		void Load();
	}
}