﻿// CinemaManager created by Seraphin, Pascal & Alain as a school project
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
		public List<RoomModel> Rooms { get; set; }

		/// <summary>
		///     Vorstellungen des Kinos
		/// </summary>
		public List<PresentationModel> Presentations { get; set; }

		/// <summary>
		///     Benutzer des Kinos
		/// </summary>
		public List<UserModel> Users { get; set; }

		/// <summary>
		///     Adresse des Kinos
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		///     Filme des Kinos
		/// </summary>
		public List<FilmModel> Films { get; set; }

		/// <summary>
		///     Ist das aktive Kino.
		/// </summary>
		public bool IsActive { get; set; }
	}
}