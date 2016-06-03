// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Windows.Input;

namespace CinemaManager.MainView
{
	public interface IDataSourceService
	{
		IList<CommandBinding> CommandBindings { get; }
		RoutedUICommand OpenFileCommand { get; }
		RoutedUICommand SaveFileCommand { get; }
		RoutedUICommand SynchronizeCommand { get; }

		/// <summary>
		///     Try to load the Datafile at the specified <paramref name="path"/>/>
		/// </summary>
		/// <param name="path">Path to load data from</param>
		void LoadData(string path);
	}
}