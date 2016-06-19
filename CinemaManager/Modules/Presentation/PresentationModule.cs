// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using CinemaManager.Filter;
using CinemaManager.Model;

namespace CinemaManager.Modules.Presentation
{
	public class PresentationModule : ModuleBase
	{
		public PresentationModule()
		{
			PresentationFilterConfigurator
				.NumberFilter("ID", p => p.FilmId)
				.DateFilter("Day", p => p.StartTime);
		}

		/// <summary>
		///     Filter-Konfigurator für die Kinos
		/// </summary>
		public IFilterConfigurator<PresentationModel> PresentationFilterConfigurator { get; } =
			new FilterConfigurator<PresentationModel>();

		/// <summary>
		///     True, wenn das Modul aktiv ist.
		/// </summary>
		public override bool Enabled => true;

		/// <summary>
		///     Titel für das Dockingframework
		/// </summary>
		public override string Title => "Presentation";

		//TODO
		public PresentationModel SelectedPresentation { get; set; }
		public bool ValueSelected => SelectedPresentation != null;

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