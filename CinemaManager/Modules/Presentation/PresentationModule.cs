// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using CinemaManager.Filter;
using CinemaManager.Filter.Complex;
using CinemaManager.Filter.Date;
using CinemaManager.Filter.Number;
using CinemaManager.Filter.String;
using CinemaManager.Model;
using CinemaManager.Modules.Cinema;

namespace CinemaManager.Modules.Presentation
{
	public class PresentationModule : ModuleBase
	{
		public PresentationModule()
		{
			PresentationFilterConfigurator
				.NumberFilter(new NumberFilter<PresentationModel>("ID", p => p.FilmId))
				.DateFilter(new DateFilter<PresentationModel>("Day", p => p.StartTime));
		}

		/// <summary>
		///     Filter-Konfigurator für die Kinos
		/// </summary>
		public IFilterConfigurator<PresentationModel> PresentationFilterConfigurator { get; } =
			new FilterConfigurator<PresentationModel>();

		/// <summary>
		///     Titel für das Dockingframework
		/// </summary>
		public override string Title => "Presentation";

		/// <summary>
		///     Aktualisiert die Daten im Modul.
		///     Beispielsweise wenn sich die Daten verändert haben.
		/// </summary>
		public override void Refresh()
		{
			// TODO
		}
	}
}