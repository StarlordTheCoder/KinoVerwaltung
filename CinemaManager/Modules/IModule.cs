// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.ComponentModel;
using System.Windows.Input;

namespace CinemaManager.Modules
{
	/// <summary>
	///     Ermöglicht einer Klasse ein Modul im GUI zu sein.
	/// </summary>
	public interface IModule : INotifyPropertyChanged
	{
		/// <summary>
		///     Wird das Modul angezeigt
		/// </summary>
		bool IsVisible { get; set; }

		/// <summary>
		///     Titel für das Dockingframework
		/// </summary>
		string Title { get; }

		/// <summary>
		///     Command für das Dockingframework
		/// </summary>
		ICommand CloseCommand { get; }

		/// <summary>
		///     Aktualisiert die Daten im Modul.
		///     Beispielsweise wenn sich die Daten verändert haben.
		/// </summary>
		void Refresh();

		/// <summary>
		///     Die Moduldate, welche für die Filter der anderen Module relevant sind, haben sich verändert.
		/// </summary>
		event EventHandler ModuleDataChanged;
	}
}