// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;

namespace CinemaManager.Model
{
	[Serializable]
	public class SeatIdentifier
	{
		public int Number { get; set; }
		public int Row { get; set; }
	}
}