// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CinemaManager.Filter;
using CinemaManager.Infrastructure;
using CinemaManager.Model;
using Microsoft.Practices.Prism.Commands;

namespace CinemaManager.Modules.User
{
	/// <summary>
	///     Modul zum Anzeigen der User.
	/// </summary>
	public class UserModule : ModuleBase
	{
		private UserModel _selectedUser;

		/// <summary>
		/// Modul zum Anzeigen des Users
		/// </summary>
		public UserModule()
		{
			AddUserCommand = new DelegateCommand(AddUser);
			RemoveUserCommand = new DelegateCommand(RemoveUser, () => ValueSelected);

			UserFilterConfigurator
				.StringFilter("Name", u => u.Name)
				.StringFilter("Phone", u => u.PhoneNumber)
				.NumberFilter("ID", u => u.UserId);

			UserFilterConfigurator.FilterChanged += (sender, e) => FilterChanged();
		}

		/// <summary>
		/// Command für <see cref="RemoveUser"/>
		/// </summary>
		public DelegateCommand RemoveUserCommand { get; set; }

		/// <summary>
		/// Command für <see cref="AddUser"/>
		/// </summary>
		public ICommand AddUserCommand { get; set; }

		/// <summary>
		///     Titel für das Dockingframework
		/// </summary>
		public override string Title => "User Module";

		/// <summary>
		///     True, wenn das Modul aktiv ist.
		/// </summary>
		public override bool Enabled => UserModels != null;

		/// <summary>
		/// Filter des Benutzermodules
		/// </summary>
		public IFilterConfigurator<UserModel> UserFilterConfigurator { get; set; } = new FilterConfigurator<UserModel>();

		/// <summary>
		/// Liste aller <see cref="UserModel"/>
		/// </summary>
		public ObservableCollection<UserModel> Users { get; } = new ObservableCollection<UserModel>();

		/// <summary>
		/// <see cref="UserModel"/> des ausgewählten Benutzers
		/// </summary>
		public UserModel SelectedUser
		{
			get { return _selectedUser; }
			set
			{
				if (Equals(_selectedUser, value)) return;
				_selectedUser = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(ValueSelected));
				OnModuleDataChanged();
				RemoveUserCommand.RaiseCanExecuteChanged();
			}
		}

		private static IList<UserModel> UserModels => Session.Instance.SelectedCinemaModel?.Users;

		/// <summary>
		/// Gibt den Ausgewählten Benutzer zurück
		/// </summary>
		public bool ValueSelected => SelectedUser != null;

		/// <summary>
		/// Entfernt den ausgewählten User
		/// </summary>
		public void RemoveUser()
		{
			UserModels.Remove(SelectedUser);

			Users.Remove(SelectedUser);
			SelectedUser = Users.FirstOrDefault();
		}

		/// <summary>
		/// Fügt einen neuen User hinzu
		/// </summary>
		public void AddUser()
		{
			var user = new UserModel
			{
				Name = string.Empty,
				PhoneNumber = string.Empty,
				UserId = UserModels.Any() ? UserModels.Max(u => u.UserId) + 1 : 1
			};

			UserModels.Add(user);

			Users.Add(user);
			SelectedUser = user;
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
			if (UserModels != null)
			{
				var filteredData = UserFilterConfigurator.FilterData(UserModels);
				Users.Clear();

				foreach (var user in filteredData)
				{
					Users.Add(user);
				}
			}

			OnPropertyChanged(nameof(Enabled));
		}
	}
}