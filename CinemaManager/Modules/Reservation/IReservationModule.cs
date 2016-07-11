// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.ObjectModel;

namespace CinemaManager.Modules.Reservation
{
	/// <summary>
	///     Interface for <see cref="ReservationModule" />
	/// </summary>
	public interface IReservationModule : IModule
	{
		/// <summary>
		///     Alle gefilterten Reservationen
		/// </summary>
		ObservableCollection<ReservationViewModel> Reservations { get; }

		/// <summary>
		///     Ausgewählte Reservation
		/// </summary>
		ReservationViewModel SelectedReservation { get; set; }

		/// <summary>
		///     Fügt eine Reservation hinzu
		/// </summary>
		void AddReservation();

		/// <summary>
		///     Remove the <see cref="SelectedReservation" />
		/// </summary>
		void RemoveReservation();
	}
}