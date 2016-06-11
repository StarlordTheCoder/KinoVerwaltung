// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;

namespace CinemaManagerTest.Filter
{
	public interface IDummyModel
	{
		string StringProperty { get; set; }
		DateTime? DateTimeProperty { get; set; }
	}

	public class DummyModel : IDummyModel
	{
		public string StringProperty { get; set; }
		public DateTime? DateTimeProperty { get; set; }
	}
}