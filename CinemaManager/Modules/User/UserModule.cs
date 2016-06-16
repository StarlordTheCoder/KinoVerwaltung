// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CinemaManager.Filter;
using CinemaManager.Filter.Number;
using CinemaManager.Filter.String;
using CinemaManager.Model;

namespace CinemaManager.Modules.User
{
	/// <summary>
	///     Modul zum Anzeigen der User.
	/// </summary>
	public class UserModule : ModuleBase
	{
		private UserModel _selectedUser;

		public UserModule()
		{
			UserFilterConfigurator
				.StringFilter(new StringFilter<UserModel>("Name", u => u.Name))
				.StringFilter(new StringFilter<UserModel>("Phone", u => u.PhoneNumber))
				.NumberFilter(new NumberFilter<UserModel>("ID", u => u.UserId));

			UserFilterConfigurator.FilterChanged += (sender, e) => FilterChanged();
		}

		/// <summary>
		///     Titel für das Dockingframework
		/// </summary>
		public override string Title => "User Module";

		public bool DataAvailable => UserModels != null;

		public IFilterConfigurator<UserModel> UserFilterConfigurator { get; } = new FilterConfigurator<UserModel>();

		public ObservableCollection<UserModel> Users { get; } = new ObservableCollection<UserModel>();

		public UserModel SelectedUser
		{
			get { return _selectedUser; }
			set
			{
				if (Equals(_selectedUser, value)) return;
				_selectedUser = value;
				OnPropertyChanged();
			}
		}

		private static IList<UserModel> UserModels => Session.Instance.SelectedCinemaModel?.Users;

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

				foreach (var cinema in filteredData)
				{
					Users.Add(cinema);
				}
			}

			OnPropertyChanged(nameof(DataAvailable));
		}
	}
}