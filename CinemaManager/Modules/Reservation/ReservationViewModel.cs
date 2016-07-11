// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using CinemaManager.Infrastructure;
using CinemaManager.Model;
using CinemaManager.Modules.Presentation;
using CinemaManager.Modules.User;
using Microsoft.Practices.Prism.Commands;

namespace CinemaManager.Modules.Reservation
{
	/// <summary>
	///     ViewModel für <see cref="ReservationModel" />
	/// </summary>
	public class ReservationViewModel : NotifyPropertyChangedBase
	{
		private readonly IPresentationModule _presentationModule;
		private readonly Action _refreshReservationModuleAction;
		private readonly IUserModule _userModule;
		private PresentationViewModel _presentation;
		private UserModel _reservator;

		/// <summary>
		///     Constructor
		/// </summary>
		/// <param name="model">ReservationModel</param>
		/// <param name="userModule"></param>
		/// <param name="presentationModule"></param>
		/// <param name="refreshReservationModuleAction"></param>
		public ReservationViewModel(ReservationModel model, Action refreshReservationModuleAction, IUserModule userModule, IPresentationModule presentationModule)
		{
			_refreshReservationModuleAction = refreshReservationModuleAction;
			_userModule = userModule;
			_presentationModule = presentationModule;
			Model = model;

			Reservator = Cinema.Users.FirstOrDefault(u => u.UserId == Model.ReservatorId);

			Presentation = new PresentationViewModel(Cinema.Presentations.FirstOrDefault(p => p.Reservations.Contains(Model)));

			SaveReservationCommand = new DelegateCommand(SaveReservation, CanSaveReservation);

			ApplyUserFromUserModuleCommand = new DelegateCommand(ApplyUserFromUserModule, CanApplyUserFromUserModule);
			userModule.ModuleDataChanged += (sender, e) => ApplyUserFromUserModuleCommand.RaiseCanExecuteChanged();

			ApplyPresentationFromPresentationModuleCommand =
				new DelegateCommand(ApplyPresentationFromPresentationModule, CanApplyPresentationFromPresentationModule);
			presentationModule.ModuleDataChanged +=
				(sender, e) => ApplyPresentationFromPresentationModuleCommand.RaiseCanExecuteChanged();
		}

		/// <summary>
		///     Total price
		/// </summary>
		public string Price
		{
			get
			{
				return
					$"{Presentation.RoomViewModel.SelectedSeats.Sum(s => s.SelectedSeatType.PriceMultiplicator)*(double) Presentation.Film.BasePricePerSeat:C}";
			}
		}

		/// <summary>
		///     Das Original-Model
		/// </summary>
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

				if (_presentation != null)
				{
					_presentation.Model.Reservations?.Remove(Model);

					_presentation.PropertyChanged -= PresentationOnPropertyChanged;

					_presentation.RoomViewModel.SelectedSeats.CollectionChanged -= SelectedSeatsOnCollectionChanged;
				}

				_presentation = value;

				_presentation.PropertyChanged += PresentationOnPropertyChanged;
				_presentation.RoomViewModel.SelectedSeats.CollectionChanged += SelectedSeatsOnCollectionChanged;

				if (!_presentation.Model.Reservations.Contains(Model))
				{
					_presentation.Model.Reservations.Add(Model);
				}
				OnPropertyChanged();
			}
		}

		/// <summary>
		///     Command for <see cref="ApplyUserFromUserModule" />
		/// </summary>
		public DelegateCommand ApplyUserFromUserModuleCommand { get; }

		/// <summary>
		///     Command for <see cref="SaveReservation" />
		/// </summary>
		public DelegateCommand SaveReservationCommand { get; }

		/// <summary>
		///     Command for <see cref="ApplyPresentationFromPresentationModule" />
		/// </summary>
		public DelegateCommand ApplyPresentationFromPresentationModuleCommand { get; }

		private void SelectedSeatsOnCollectionChanged(object sender,
			NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
		{
			OnPropertyChanged(nameof(Price));
			SaveReservationCommand.RaiseCanExecuteChanged();
		}

		private void PresentationOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			if (Equals(propertyChangedEventArgs.PropertyName, nameof(PresentationViewModel.Film)))
			{
				OnPropertyChanged(nameof(Price));
			}
		}

		private void SaveReservation()
		{
			Model.Seats.Clear();

			Model.Seats.AddRange(Presentation.RoomViewModel.SelectedSeats.Select(s => s.Model.Place));

			_refreshReservationModuleAction.Invoke();
		}

		private bool CanSaveReservation()
		{
			return Presentation.RoomViewModel.SelectedSeats.Count == Presentation.RoomViewModel.MaximumSelected &&
			       Presentation.RoomViewModel.SelectedSeats.Any();
		}

		private bool CanApplyPresentationFromPresentationModule()
		{
			return _presentationModule.ValueSelected;
		}

		private void ApplyPresentationFromPresentationModule()
		{
			Presentation = _presentationModule.SelectedPresentation;
		}

		private bool CanApplyUserFromUserModule()
		{
			return _userModule.ValueSelected;
		}

		private void ApplyUserFromUserModule()
		{
			Reservator = _userModule.SelectedUser;
		}
	}
}