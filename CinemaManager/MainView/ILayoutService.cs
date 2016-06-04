// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Windows.Input;
using Xceed.Wpf.AvalonDock;

namespace CinemaManager.MainView
{
	public interface ILayoutService
	{
		IList<CommandBinding> CommandBindings { get; }
		RoutedUICommand OpenLayoutCommand { get; }
		RoutedUICommand SaveAsLayoutCommand { get; }
		RoutedUICommand SaveLayoutCommand { get; }

		void Initialize(DockingManager dockingManager);
	}
}