// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CinemaManager.Filter;
using CinemaManager.Infrastructure;
using CinemaManager.Model;
using Microsoft.Practices.Prism.Commands;

namespace CinemaManager.Modules.Presentation
{
	public class PresentationModule : ModuleBase
	{
		private PresentationModel _selectedPresentation;

		public PresentationModule()
		{
			AddPresentationCommand = new DelegateCommand(AddPresentation);
			RemovePresentationCommand = new DelegateCommand(RemovePresentation, () => ValueSelected);

			PresentationFilterConfigurator
				.NumberFilter("ID", p => p.FilmId)
				.DateFilter("Day", p => p.StartTime);

			PresentationFilterConfigurator.FilterChanged += (sender, e) => FilterChanged();
		}

		public void RemovePresentation()
		{
			PresentationModels.Remove(SelectedPresentation);
			Presentations.Remove(SelectedPresentation);
		}

		public void AddPresentation()
		{
			var presentation = new PresentationModel
			{
				FilmId = PresentationModels.Any() ? PresentationModels.Max(p => p.FilmId) + 1 : 1,
				Reservations = new List<ReservationModel>(),
				StartTime = DateTime.Now
			};

			PresentationModels.Add(presentation);
			Presentations.Add(presentation);
		}

		/// <summary>
		///     Filter-Konfigurator für die Kinos
		/// </summary>
		public IFilterConfigurator<PresentationModel> PresentationFilterConfigurator { get; set; } =
			new FilterConfigurator<PresentationModel>();

		/// <summary>
		///     Command for <see cref="AddPresentation" />
		/// </summary>
		public ICommand AddPresentationCommand { get; }

		/// <summary>
		///     Command for <see cref="RemovePresentation" />
		/// </summary>
		public DelegateCommand RemovePresentationCommand { get; }

		/// <summary>
		///     True, wenn das Modul aktiv ist.
		/// </summary>
		public override bool Enabled => true;

		/// <summary>
		///     Titel für das Dockingframework
		/// </summary>
		public override string Title => "Presentation";

		public PresentationModel SelectedPresentation
		{
			get { return _selectedPresentation; }
			set
			{
				if (Equals(_selectedPresentation, value)) return;
				_selectedPresentation = value;
				OnPropertyChanged();
				OnModuleDataChanged();
				RemovePresentationCommand.RaiseCanExecuteChanged();
			}
		}

		public ObservableCollection<PresentationModel> Presentations { get; } = new ObservableCollection<PresentationModel>();

		public bool ValueSelected => SelectedPresentation != null;

		/// <summary>
		///     Aktualisiert die Daten im Modul.
		///     Beispielsweise wenn sich die Daten verändert haben.
		/// </summary>
		public override void Refresh()
		{
			FilterChanged();
		}

		private static IList<PresentationModel> PresentationModels => Session.Instance.SelectedCinemaModel?.Presentations;

		private void FilterChanged()
		{
			if (PresentationModels != null)
			{
				var filteredData = PresentationFilterConfigurator.FilterData(PresentationModels);
				Presentations.Clear();

				foreach (var presentation in filteredData)
				{
					Presentations.Add(presentation);
				}
			}

			OnPropertyChanged(nameof(Enabled));
		}
	}
}