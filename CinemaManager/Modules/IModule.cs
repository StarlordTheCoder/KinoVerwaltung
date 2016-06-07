// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.ComponentModel;
using System.Windows.Input;

namespace CinemaManager.Modules
{
	/// <summary>
	/// Ermöglicht einer Klasse ein Modul im GUI zu sein.
	/// </summary>
	public interface IModule : INotifyPropertyChanged
	{
		bool IsVisible { get; set; }
		string Title { get; }

		ICommand CloseCommand { get; }
	}
}