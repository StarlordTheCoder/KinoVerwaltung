// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CinemaManager.Filter;
using CinemaManager.Infrastructure;
using CinemaManager.Model;
using CinemaManager.Modules.Presentation;
using CinemaManager.Modules.User;
using Microsoft.Practices.Prism.Commands;

namespace CinemaManager.Modules.Reservation
{
	/// <summary>
	///     Modul zum Verwalten der Reser
	/// </summary>
	public class ReservationModule : ModuleBase, IReservationModule
	{
		private readonly IPresentationModule _presentationModule;
		private readonly IUserModule _userModule;
		private ReservationViewModel _selectedReservation;

		/// <summary>
		///     Constructor
		/// </summary>
		/// <param name="presentationModule">Modul für Modulübergreifende Filter</param>
		/// <param name="userModule">Modul für Modulübergreifende Filter</param>
		public ReservationModule(IPresentationModule presentationModule, IUserModule userModule)
		{
			_presentationModule = presentationModule;
			_userModule = userModule;

			AddReservationCommand = new DelegateCommand(AddReservation, () => presentationModule.ValueSelected);
			RemoveReservationCommand = new DelegateCommand(RemoveReservation, () => ValueSelected);

			_presentationModule.ModuleDataChanged += (sender, e) => AddReservationCommand.RaiseCanExecuteChanged();

			ReservationFilterConfigurator
				.NumberFilter("ID", c => c.ReservatorId)
				.ComplexFilter(presentationModule, p => p.SelectedPresentation?.Model.Reservations)
				.ComplexFilter(userModule, u => GetReservations(u.SelectedUser));

			ReservationFilterConfigurator.FilterChanged += (sender, e) => FilterChanged();
		}

		/// <summary>
		///     Filter-Configurator für die Reservationen
		/// </summary>
		public IFilterConfigurator<ReservationModel> ReservationFilterConfigurator { get; set; } =
			new FilterConfigurator<ReservationModel>();

		private static IEnumerable<ReservationModel> ReservationModels
			=> Session.Instance.SelectedCinemaModel?.Presentations.SelectMany(p => p.Reservations);

		/// <summary>
		///     Command for <see cref="AddReservation" />
		/// </summary>
		public DelegateCommand AddReservationCommand { get; }

		/// <summary>
		///     Command for <see cref="RemoveReservation" />
		/// </summary>
		public DelegateCommand RemoveReservationCommand { get; }

		/// <summary>
		///     True, wenn das Modul aktiv ist.
		/// </summary>
		public override bool Enabled => ReservationModels != null;

		/// <summary>
		///     Shows if there is a selected Reservation
		/// </summary>
		public override bool ValueSelected => SelectedReservation != null;

		/// <summary>
		///     Titel für das Dockingframework
		/// </summary>
		public override string Title => "Reservations";

		/// <summary>
		///     Alle gefilterten Reservationen
		/// </summary>
		public ObservableCollection<ReservationViewModel> Reservations { get; } =
			new ObservableCollection<ReservationViewModel>();

		/// <summary>
		///     Ausgewählte Reservation
		/// </summary>
		public ReservationViewModel SelectedReservation
		{
			get { return _selectedReservation; }
			set
			{
				if (Equals(value, _selectedReservation)) return;
				_selectedReservation = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(ValueSelected));
				RemoveReservationCommand.RaiseCanExecuteChanged();
			}
		}

		/// <summary>
		///     Remove the <see cref="SelectedReservation" />
		/// </summary>
		public void RemoveReservation()
		{
			SelectedReservation.Presentation.Model.Reservations.Remove(SelectedReservation.Model);

			Reservations.Remove(SelectedReservation);

			SelectedReservation = null;
		}

		/// <summary>
		///     Fügt eine Reservation hinzu
		/// </summary>
		public async void AddReservation()
		{
			var model = new ReservationModel();

			_presentationModule.SelectedPresentation.Model.Reservations.Add(model);

			var reservation = new ReservationViewModel(model, _userModule, _presentationModule);

			await reservation.ApplyUserFromUserModuleCommand.Execute();
			await reservation.ApplyPresentationFromPresentationModuleCommand.Execute();

			Reservations.Add(reservation);
			SelectedReservation = reservation;
		}

		/// <summary>
		///     Aktualisiert die Daten im Modul.
		///     Beispielsweise wenn sich die Daten verändert haben.
		/// </summary>
		public override void Refresh()
		{
			FilterChanged();
		}

		private static IEnumerable<ReservationModel> GetReservations(UserModel user)
		{
			return user != null
				? ReservationModels.Where(r => r.ReservatorId == user.UserId)
				: Enumerable.Empty<ReservationModel>();
		}

		private void FilterChanged()
		{
			if (ReservationModels != null)
			{
				var filteredData = ReservationFilterConfigurator.FilterData(ReservationModels);
				Reservations.Clear();

				foreach (var reservation in filteredData)
				{
					Reservations.Add(new ReservationViewModel(reservation, _userModule, _presentationModule));
				}
			}

			OnPropertyChanged(nameof(Enabled));
		}
	}
}