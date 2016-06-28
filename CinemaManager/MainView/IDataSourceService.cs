// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Windows.Input;

namespace CinemaManager.MainView
{
	/// <summary>
	///     Ermöglicht das Verwalten der aktuell geöffneten Datei.
	/// </summary>
	public interface IDataSourceService
	{
		/// <summary>
		///     Try to load the Datafile at the specified <paramref name="path" />
		/// </summary>
		/// <param name="path">Path to load data from</param>
		void LoadData(string path);

		/// <summary>
		///     List of the used <see cref="CommandBindings" />
		///     Binds the commands to the associated actions
		/// </summary>
		IList<CommandBinding> CommandBindings { get; }

		/// <summary>
		///     Command to open a file
		/// </summary>
		RoutedUICommand OpenFileCommand { get; }

		/// <summary>
		///     Command to save a file (as)
		/// </summary>
		RoutedUICommand SaveFileCommand { get; }

		/// <summary>
		///     Command to save the current file and reload it
		/// </summary>
		RoutedUICommand SynchronizeCommand { get; }
	}
}