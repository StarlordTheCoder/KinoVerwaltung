// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using CinemaManager.Filter;
using CinemaManager.Model;

namespace CinemaManager.Modules.Cinema
{
	public class CinemaModule : ModuleBase
	{
		public CinemaModule()
		{
			CinemaFilterConfigurator
				.StringFilter(new StringFilter<CinemaModel>("Name", c => c.Name, c => c.Address));
		}

		public override string Title => "Cinema Manager";
		public override void Refresh()
		{
			//TODO
		}

		public IFilterConfigurator<CinemaModel> CinemaFilterConfigurator { get; } = new FilterConfigurator<CinemaModel>();
	}
}