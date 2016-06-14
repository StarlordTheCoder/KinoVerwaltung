// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;
using CinemaManager.Modules;

namespace CinemaManagerTest.Filter
{
	public interface IDummyModule : IModule
	{
		IEnumerable<IDummyModel> ExampleList { get; set; }
	}

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