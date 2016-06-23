// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;

namespace CinemaManager.Model
{
	/// <summary>
	///     Ein Film
	/// </summary>
	[Serializable]
	public class FilmModel
	{
		/// <summary>
		///     Filmname
		/// </summary>
		public string FilmName { get; set; }

		/// <summary>
		///     Film ID
		/// </summary>
		public int FilmId { get; set; }

		/// <summary>
		///     Filmlänge
		/// </summary>
		public TimeSpan Length { get; set; }

		/// <summary>
		///     Verlag
		/// </summary>
		public string Publisher { get; set; }

		/// <summary>
		///     Regisseur
		/// </summary>
		public string Director { get; set; }

		/// <summary>
		///     Hauptdarsteller
		/// </summary>
		public List<string> MainActors { get; set; } = new List<string>();

		/// <summary>
		///     Altersfreigabe
		/// </summary>
		public AgeRestriction AgeRestriction { get; set; }
	}
}