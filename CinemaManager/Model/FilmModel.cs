// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;

namespace CinemaManager.Model
{
	[Serializable]
	public class FilmModel
	{
		public string FilmName { get; set; }
		public TimeSpan Length { get; set; }
		public string Publisher { get; set; }
		public string Regisseur { get; set; }
		public string[] MainActors { get; set; }
		public AgeRestrictors AgeRestriction { get; set; }
	}
}