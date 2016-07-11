// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.ObjectModel;
using CinemaManager.Model;

namespace CinemaManager.Modules.User
{
	/// <summary>
	///     Interface for <see cref="UserModule" />
	/// </summary>
	public interface IUserModule : IModule
	{
		/// <summary>
		///     <see cref="UserModel" /> des ausgewählten Benutzers
		/// </summary>
		UserModel SelectedUser { get; set; }

		/// <summary>
		///     Liste aller <see cref="UserModel" />
		/// </summary>
		ObservableCollection<UserModel> Users { get; }

		/// <summary>
		///     Fügt einen neuen User hinzu
		/// </summary>
		void AddUser();

		/// <summary>
		///     Entfernt den ausgewählten User
		/// </summary>
		void RemoveUser();
	}
}