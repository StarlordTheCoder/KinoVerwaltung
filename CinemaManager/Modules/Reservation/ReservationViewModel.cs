// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Linq;
using CinemaManager.Infrastructure;
using CinemaManager.Model;
using CinemaManager.Modules.Presentation;
using CinemaManager.Modules.Room;
using CinemaManager.Modules.User;
using Microsoft.Practices.Prism.Commands;

namespace CinemaManager.Modules.Reservation
{
	public class ReservationViewModel : NotifyPropertyChangedBase
	{
		private PresentationViewModel _presentation;
		private UserModel _reservator;

		public ReservationViewModel(ReservationModel model, UserModule userModule, PresentationModule presentationModule)
		{
			Model = model;

			Reservator = Model.ReservatorId != 0
				? Cinema.Users.First(u => u.UserId == Model.ReservatorId)
				: userModule.SelectedUser;

			Presentation = new PresentationViewModel(Cinema.Presentations.FirstOrDefault(p => p.Reservations.Contains(Model)));

			ApplyUserFromUserModule = new DelegateCommand(() => Reservator = userModule.SelectedUser,
				() => userModule.ValueSelected);
			userModule.ModuleDataChanged += (sender, e) => ApplyUserFromUserModule.RaiseCanExecuteChanged();

			ApplyPresentationFromPresentationModule =
				new DelegateCommand(() => Presentation = presentationModule.SelectedPresentation,
					() => presentationModule.ValueSelected);
			presentationModule.ModuleDataChanged +=
				(sender, e) => ApplyPresentationFromPresentationModule.RaiseCanExecuteChanged();
		}

		public ReservationModel Model { get; }

		/// <summary>
		///     Usermodel des Reservierers
		/// </summary>
		public UserModel Reservator
		{
			get { return _reservator; }
			set
			{
				if (Equals(_reservator, value)) return;
				_reservator = value;
				Model.ReservatorId = _reservator.UserId;
				OnPropertyChanged();
			}
		}

		private static CinemaModel Cinema => Session.Instance.SelectedCinemaModel;

		/// <summary>
		///     Model der Präseantation
		/// </summary>
		public PresentationViewModel Presentation
		{
			get { return _presentation; }
			set
			{
				if (Equals(_presentation, value)) return;
				_presentation?.Model.Reservations?.Remove(Model);

				_presentation = value;

				if (!_presentation.Model.Reservations.Contains(Model))
				{
					_presentation.Model.Reservations.Add(Model);
				}
				OnPropertyChanged();
			}
		}

		public DelegateCommand ApplyUserFromUserModule { get; }
		public DelegateCommand ApplyPresentationFromPresentationModule { get; }
	}
}