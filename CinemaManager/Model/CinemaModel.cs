﻿// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;

namespace CinemaManager.Model
{
	[Serializable]
	public class CinemaModel
	{
		public string Name { get; set; }
		public List<RoomModel> Rooms { get; set; }
		public List<Presentation> Presentations { get; set; }
		public string Address { get; set; }
	}
}