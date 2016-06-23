// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;

namespace CinemaManager.Model
{
	/// <summary>
	///     Ein Kino
	/// </summary>
	[Serializable]
	public class CinemaModel
	{
		/// <summary>
		///     Name des Kinos
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///     Säle des Kinos
		/// </summary>
		public List<RoomModel> Rooms { get; set; } = new List<RoomModel>();

		/// <summary>
		///     Vorstellungen des Kinos
		/// </summary>
		public List<PresentationModel> Presentations { get; set; } = new List<PresentationModel>();

		/// <summary>
		///     Benutzer des Kinos
		/// </summary>
		public List<UserModel> Users { get; set; } = new List<UserModel>();

		/// <summary>
		///     Filme des Kinos
		/// </summary>
		public List<FilmModel> Films { get; set; } = new List<FilmModel>();

		/// <summary>
		///     Die verschiedenen Sitzarten, welche das Kino unterstützt
		/// </summary>
		public List<SeatType> SeatTypes { get; set; } = new List<SeatType>();

		/// <summary>
		///     Adresse des Kinos
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		///     Ist das aktive Kino.
		/// </summary>
		public bool IsActive { get; set; }
	}
}