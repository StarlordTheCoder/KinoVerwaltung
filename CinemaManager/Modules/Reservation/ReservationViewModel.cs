﻿// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

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
		private readonly PresentationModule _presentationModule;
		private readonly UserModule _userModule;
		private PresentationViewModel _presentation;
		private UserModel _reservator;

		/// <summary>
		///     Constructor
		/// </summary>
		/// <param name="model">ReservationModel</param>
		/// <param name="userModule"></param>
		/// <param name="presentationModule"></param>
		public ReservationViewModel(ReservationModel model, UserModule userModule, PresentationModule presentationModule)
		{
			_userModule = userModule;
			_presentationModule = presentationModule;
			Model = model;

			Reservator = Cinema.Users.FirstOrDefault(u => u.UserId == Model.ReservatorId);

			Presentation = new PresentationViewModel(Cinema.Presentations.FirstOrDefault(p => p.Reservations.Contains(Model)));

			ApplyUserFromUserModuleCommand = new DelegateCommand(ApplyUserFromUserModule, CanApplyUserFromUserModule);
			userModule.ModuleDataChanged += (sender, e) => ApplyUserFromUserModuleCommand.RaiseCanExecuteChanged();

			ApplyPresentationFromPresentationModuleCommand =
				new DelegateCommand(ApplyPresentationFromPresentationModule, CanApplyPresentationFromPresentationModule);
			presentationModule.ModuleDataChanged +=
				(sender, e) => ApplyPresentationFromPresentationModuleCommand.RaiseCanExecuteChanged();
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
				_presentation?.Model.Reservations?.Remove(Model);

				_presentation = value;

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
		///     Command for <see cref="ApplyPresentationFromPresentationModule" />
		/// </summary>
		public DelegateCommand ApplyPresentationFromPresentationModuleCommand { get; }

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