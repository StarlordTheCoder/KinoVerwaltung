// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using CinemaManager.Filter;
using CinemaManager.Model;

namespace CinemaManager.Modules.Cinema
{
	public class CinemaModule : ModuleBase
	{
		public override string Title => "Cinema Manager";

		public IFilterConfigurator<CinemaModel> CinemaFilterConfigurator { get; } = new FilterConfigurator<CinemaModel>();

		public CinemaModule()
		{
			CinemaFilterConfigurator
				.StringFilter(new StringFilter<CinemaModel>(c => c.Name, "Name"))
				.StringFilter(new StringFilter<CinemaModel>(c => c.Address, "Address"));
		}
	}
}