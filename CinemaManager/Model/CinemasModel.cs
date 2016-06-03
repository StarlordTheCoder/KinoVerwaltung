// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;

namespace CinemaManager.Model
{
	/// <summary>
	/// Überklasse aller Models. Beinhaltet eine liste aller Kinos. Der Anfangspunkt der Serialisierung.
	/// </summary>
	[Serializable]
	public class CinemasModel
	{
		public List<CinemaModel> Cinemas { get; } = new List<CinemaModel>();
	}
}