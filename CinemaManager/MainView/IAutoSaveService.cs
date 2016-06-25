// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

namespace CinemaManager.MainView
{
	/// <summary>
	///     Interface für einen Services, welcher das automatische Speichern erlaubt
	/// </summary>
	public interface IAutoSaveService
	{
		/// <summary>
		///     Disables the AutoSave
		/// </summary>
		void DisableAutoSave();

		/// <summary>
		///     Enables the AutoSave
		/// </summary>
		void EnableAutoSave();
	}
}