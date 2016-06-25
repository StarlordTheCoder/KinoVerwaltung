// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Windows.Input;
using Xceed.Wpf.AvalonDock;

namespace CinemaManager.MainView
{
	/// <summary>
	///     Ermöglicht das Speichern und Laden von Layoutdateien
	/// </summary>
	public interface ILayoutService
	{
		/// <summary>
		///     Die Command-Binding. Verbindet alle Command mit den dazugehörigen Aktionen
		/// </summary>
		IList<CommandBinding> CommandBindings { get; }

		/// <summary>
		///     Command for opening a layout
		/// </summary>
		RoutedUICommand OpenLayoutCommand { get; }

		/// <summary>
		///     Command for saving a layout under a new path
		/// </summary>
		RoutedUICommand SaveAsLayoutCommand { get; }

		/// <summary>
		///     Command for saving a layout under the current path
		/// </summary>
		RoutedUICommand SaveLayoutCommand { get; }

		/// <summary>
		///     Initializes this Service fully
		/// </summary>
		/// <param name="dockingManager">Used to save the layout</param>
		void Initialize(DockingManager dockingManager);
	}
}