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
		private PresentationViewModel _selectedPresentation;

		public PresentationModule()
		{
			AddPresentationCommand = new DelegateCommand(AddPresentation);
			RemovePresentationCommand = new DelegateCommand(RemovePresentation, () => ValueSelected);

			PresentationFilterConfigurator
				.NumberFilter("ID", p => p.FilmId)
				.DateFilter("Day", p => p.StartTime);

			PresentationFilterConfigurator.FilterChanged += (sender, e) => FilterChanged();
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
		public override bool Enabled => PresentationModels != null;

		/// <summary>
		///     Titel für das Dockingframework
		/// </summary>
		public override string Title => "Presentation";

		/// <summary>
		///     Model der ausgewählten Präsentation
		/// </summary>
		public PresentationViewModel SelectedPresentation
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

		/// <summary>
		///     Liste Aller präsenatationen
		/// </summary>
		public ObservableCollection<PresentationViewModel> Presentations { get; } = new ObservableCollection<PresentationViewModel>();

		/// <summary>
		///     Gibt zurück, ob eine Präsentation ausgewählt ist
		/// </summary>
		public bool ValueSelected => SelectedPresentation != null;

		private static IList<PresentationModel> PresentationModels => Session.Instance.SelectedCinemaModel?.Presentations;

		/// <summary>
		///     Entfernt die ausgewählte Präsentation
		/// </summary>
		public void RemovePresentation()
		{
			PresentationModels.Remove(SelectedPresentation.Model);
			Presentations.Remove(SelectedPresentation);
		}

		/// <summary>
		///     Fügt eine neue Präsentation hinzu
		/// </summary>
		public void AddPresentation()
		{
			var presentation = new PresentationModel
			{
				FilmId = PresentationModels.Any() ? PresentationModels.Max(p => p.FilmId) + 1 : 1,
				StartTime = DateTime.Now
			};

			PresentationModels.Add(presentation);
			Presentations.Add(new PresentationViewModel(presentation));
		}

		/// <summary>
		///     Aktualisiert die Daten im Modul.
		///     Beispielsweise wenn sich die Daten verändert haben.
		/// </summary>
		public override void Refresh()
		{
			FilterChanged();
		}

		private void FilterChanged()
		{
			if (PresentationModels != null)
			{
				var filteredData = PresentationFilterConfigurator.FilterData(PresentationModels);
				Presentations.Clear();

				foreach (var presentation in filteredData.Select(p => new PresentationViewModel(p)))
				{
					Presentations.Add(presentation);
				}
			}

			OnPropertyChanged(nameof(Enabled));
		}
	}
}