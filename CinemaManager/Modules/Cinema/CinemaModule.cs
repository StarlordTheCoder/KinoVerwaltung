// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using CinemaManager.Filter;

namespace CinemaManager.Modules.Cinema
{
	public class CinemaModule : ModuleBase
	{
		public override string Title => "Cinema Manager";

		public CinemaModule()
		{
			FilterConfigurator
				.StringFilter(new StringFilterAcceptor(f => FilterConfigurator.StringAcceptors.Remove(f)));
		}
	}
}