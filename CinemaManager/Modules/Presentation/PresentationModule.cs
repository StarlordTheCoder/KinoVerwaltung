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
using CinemaManager.Modules.Film;
using CinemaManager.Modules.Room;
using Microsoft.Practices.Prism.Commands;

namespace CinemaManager.Modules.Presentation
{
	/// <summary>
	///     Modul zum Verwallten der Vorstellungen
	/// </summary>
	public class PresentationModule : ModuleBase, IPresentationModule
	{
		private readonly IFilmModule _filmModule;
		private readonly IRoomModule _roomModule;
		private PresentationViewModel _selectedPresentation;

		/// <summary>
		///     Constructor
		/// </summary>
		/// <param name="filmModule">Modul für Modulübergreifende Filter</param>
		/// <param name="roomModule">Modul für Modulübergreifende Filter</param>
		public PresentationModule(IFilmModule filmModule, IRoomModule roomModule)
		{
			_filmModule = filmModule;
			_roomModule = roomModule;
			AddPresentationCommand = new DelegateCommand(AddPresentation);
			RemovePresentationCommand = new DelegateCommand(RemovePresentation, () => ValueSelected);

			PresentationFilterConfigurator
				.NumberFilter("ID", p => p.FilmId)
				.DateFilter("Day", p => p.StartTime)
				.ComplexFilter(_filmModule, f => PresentationModels.Where(p => p.FilmId == f.SelectedFilm?.FilmId));

			PresentationFilterConfigurator.FilterChanged += (sender, e) => FilterChanged();

			ApplyFilmFromFilmModuleCommand = new DelegateCommand(ApplyFilmFromFilmModule, CanApplyFilmFromFilmModule);
			filmModule.ModuleDataChanged += (sender, e) => ApplyFilmFromFilmModuleCommand.RaiseCanExecuteChanged();

			ApplyRoomFromRoomModuleCommand = new DelegateCommand(ApplyRoomFromRoomModule, CanApplyRoomFromRoomModule);
			roomModule.ModuleDataChanged += (sender, e) => ApplyRoomFromRoomModuleCommand.RaiseCanExecuteChanged();
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

		private static IList<PresentationModel> PresentationModels => Session.Instance.SelectedCinemaModel?.Presentations;

		/// <summary>
		///     Command for <see cref="ApplyFilmFromFilmModule" />
		/// </summary>
		public DelegateCommand ApplyFilmFromFilmModuleCommand { get; }

		/// <summary>
		///     Command for <see cref="ApplyRoomFromRoomModule" />
		/// </summary>
		public DelegateCommand ApplyRoomFromRoomModuleCommand { get; }

		/// <summary>
		///     True, wenn das Modul aktiv ist.
		/// </summary>
		public override bool Enabled => PresentationModels != null;

		/// <summary>
		///     Titel für das Dockingframework
		/// </summary>
		public override string Title => "Presentations";

		/// <summary>
		///     Ausgewählte Präsentation
		/// </summary>
		public PresentationViewModel SelectedPresentation
		{
			get { return _selectedPresentation; }
			set
			{
				if (Equals(_selectedPresentation, value)) return;
				_selectedPresentation = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(ValueSelected));
				OnModuleDataChanged();
				RemovePresentationCommand.RaiseCanExecuteChanged();
				ApplyFilmFromFilmModuleCommand.RaiseCanExecuteChanged();
				ApplyRoomFromRoomModuleCommand.RaiseCanExecuteChanged();
			}
		}

		/// <summary>
		///     Alle gefilterten Präsenatationen
		/// </summary>
		public ObservableCollection<PresentationViewModel> Presentations { get; } =
			new ObservableCollection<PresentationViewModel>();

		/// <summary>
		///     Gibt zurück, ob eine Präsentation ausgewählt ist
		/// </summary>
		public override bool ValueSelected => SelectedPresentation != null;

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
		public async void AddPresentation()
		{
			var presentation = new PresentationModel
			{
				StartTime = DateTime.Now
			};

			PresentationModels.Add(presentation);
			var presentationViewModel = new PresentationViewModel(presentation);
			Presentations.Add(presentationViewModel);
			SelectedPresentation = presentationViewModel;

			await ApplyFilmFromFilmModuleCommand.Execute();
			await ApplyRoomFromRoomModuleCommand.Execute();
		}

		/// <summary>
		///     Aktualisiert die Daten im Modul.
		///     Beispielsweise wenn sich die Daten verändert haben.
		/// </summary>
		public override void Refresh()
		{
			FilterChanged();
		}

		private bool CanApplyRoomFromRoomModule()
		{
			return _roomModule.ValueSelected;
		}

		private void ApplyRoomFromRoomModule()
		{
			SelectedPresentation.RoomViewModel = _roomModule.SelectedRoom;
		}

		private bool CanApplyFilmFromFilmModule()
		{
			return _filmModule.ValueSelected;
		}

		private void ApplyFilmFromFilmModule()
		{
			SelectedPresentation.Film = _filmModule.SelectedFilm;
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