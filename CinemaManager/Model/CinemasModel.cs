// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;

namespace CinemaManager.Model
{
	[Serializable]
	public class CinemasModel
	{
		public List<CinemaModel> Cinemas { get; } = new List<CinemaModel>();
	}
}