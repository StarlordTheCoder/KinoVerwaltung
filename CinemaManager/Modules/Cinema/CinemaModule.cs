// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Collections.ObjectModel;
using CinemaManager.Filter;
using CinemaManager.Model;

namespace CinemaManager.Modules.Cinema
{
	public class CinemaModule : ModuleBase
	{
		public CinemaModule()
		{
			CinemaFilterConfigurator
				.StringFilter(new StringFilter<CinemaModel>(c => c.Name, "Name"))
				.StringFilter(new StringFilter<CinemaModel>(c => c.Address, "Address"));

			Session.Instance.DataModel.Load();

			Cinemas = new ObservableCollection<CinemaModel>(Session.Instance.DataModel.CinemasModel.Cinemas);
		}

		public override string Title => "Cinema Manager";

		public IFilterConfigurator<CinemaModel> CinemaFilterConfigurator { get; } = new FilterConfigurator<CinemaModel>();
		public ObservableCollection<CinemaModel> Cinemas { get; }
	}
}