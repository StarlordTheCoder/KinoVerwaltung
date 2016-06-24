// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

namespace CinemaManager.Model
{
	/// <summary>
	///     Enthält daten der User Serialisierbar.
	/// </summary>
	public class UserModel
	{
		/// <summary>
		/// ID des Users
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		///     Name des Kundens
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///     Telefonnummer des Kundens
		/// </summary>
		public string PhoneNumber { get; set; }
	}
}