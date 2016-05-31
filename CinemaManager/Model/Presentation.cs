// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;

namespace CinemaManager.Model
{
	[Serializable]
	public class Presentation
	{
		public DateTime StartTime { get; set; }
		public FilmModel Film { get; set; }
		public RoomModel Room { get; set; }
		public bool Is3d { get; set; }
	}
}