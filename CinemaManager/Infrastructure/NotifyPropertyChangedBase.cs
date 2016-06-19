// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.ComponentModel;
using System.Runtime.CompilerServices;
using CinemaManager.Properties;

namespace CinemaManager.Infrastructure
{
	/// <summary>
	///     Basisklasse für die <see cref="INotifyPropertyChanged" /> Implementation
	/// </summary>
	public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
	{
		/// <summary>Tritt ein, wenn sich ein Eigenschaftswert ändert.</summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		///     Event invokator for <see cref="PropertyChanged" />
		/// </summary>
		/// <param name="propertyName">Property that changed</param>
		[NotifyPropertyChangedInvocator]
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}