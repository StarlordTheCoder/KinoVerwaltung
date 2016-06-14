// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CinemaManager.Filter;
using CinemaManager.Model;

namespace CinemaManager.Modules.User
{
	/// <summary>
	///     Modul zum Anzeigen der User.
	/// </summary>
	public class UserModule : ModuleBase
	{
		/// <summary>
		///     Titel für das Dockingframework
		/// </summary>
		public override string Title => "User";

		public IFilterConfigurator<UserModel> UserFilterConfigurator { get; } = new FilterConfigurator<UserModel>();

		public UserModule()
		{
			UserFilterConfigurator
				.StringFilter(new StringFilter<UserModel>("Name", u => u.Name))
				.StringFilter(new StringFilter<UserModel>("Phone", u => u.PhoneNumber));

			UserFilterConfigurator.FilterChanged += (sender, e) => FilterChanged();
		}

		public ObservableCollection<UserModel> Users { get; } = new ObservableCollection<UserModel>();

		/// <summary>
		///     Aktualisiert die Daten im Modul.
		///     Beispielsweise wenn sich die Daten verändert haben.
		/// </summary>
		public override void Refresh()
		{
			FilterChanged();
		}

		private static IEnumerable<UserModel> UserModels => Session.Instance.SelectedCinemaModel.Users;

		private void FilterChanged()
		{
			var filteredData = UserFilterConfigurator.FilterData(UserModels);
			Users.Clear();

			foreach (var cinema in filteredData)
			{
				Users.Add(cinema);
			}
		}
	}
}