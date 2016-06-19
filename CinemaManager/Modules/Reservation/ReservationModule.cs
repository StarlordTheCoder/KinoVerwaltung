// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CinemaManager.Filter;
using CinemaManager.Filter.Complex;
using CinemaManager.Filter.Number;
using CinemaManager.Infrastructure;
using CinemaManager.Model;
using CinemaManager.Modules.Presentation;
using CinemaManager.Modules.User;
using Microsoft.Practices.Prism.Commands;

namespace CinemaManager.Modules.Reservation
{
	public class ReservationModule : ModuleBase
	{
		private readonly PresentationModule _presentationModule;
		private readonly UserModule _userModule;
		private ReservationViewModel _selectedReservation;

		public ReservationModule(PresentationModule presentationModule, UserModule userModule)
		{
			_presentationModule = presentationModule;
			_userModule = userModule;
			AddReservationCommand = new DelegateCommand(AddReservation);
			RemoveReservationCommand = new DelegateCommand(RemoveReservation, () => ValueSelected);

			ReservationFilterConfigurator
				.NumberFilter("ID", c => c.ReservatorId)
				.ComplexFilter(presentationModule, p => p.SelectedPresentation?.Reservations)
				.ComplexFilter(userModule, u => GetReservations(u.SelectedUser));

			ReservationFilterConfigurator.FilterChanged += (sender, e) => FilterChanged();
		}

		public bool ValueSelected => SelectedReservation != null;

		/// <summary>
		///     True, wenn das Modul aktiv ist.
		/// </summary>
		public override bool Enabled => ReservationModels != null;

		/// <summary>
		///     Titel für das Dockingframework
		/// </summary>
		public override string Title => "Reservation Module";

		/// <summary>
		///     Alle gefilterten Resrvationen
		/// </summary>
		public ObservableCollection<ReservationViewModel> Reservations { get; } =
			new ObservableCollection<ReservationViewModel>();

		/// <summary>
		///     Filter-Configurator für die Reservationen
		/// </summary>
		public IFilterConfigurator<ReservationModel> ReservationFilterConfigurator { get; } =
			new FilterConfigurator<ReservationModel>();

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
			}
		}

		private static IEnumerable<ReservationModel> ReservationModels
			=> Session.Instance.SelectedCinemaModel?.Presentations.SelectMany(p => p.Reservations);

		public ICommand AddReservationCommand { get; }

		public DelegateCommand RemoveReservationCommand { get; }

		private void RemoveReservation()
		{
			SelectedReservation.Presentation.Reservations.Remove(SelectedReservation.Model);

			Reservations.Remove(SelectedReservation);

			SelectedReservation = null;
		}

		private void AddReservation()
		{
			var reservation = new ReservationViewModel(new ReservationModel(), _userModule, _presentationModule);

			Reservations.Add(reservation);
			SelectedReservation = reservation;
		}

		private static IEnumerable<ReservationModel> GetReservations(UserModel user)
		{
			return user != null
				? ReservationModels.Where(r => r.ReservatorId == user.UserId)
				: Enumerable.Empty<ReservationModel>();
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