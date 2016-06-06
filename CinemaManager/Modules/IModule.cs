// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.ComponentModel;
using System.Windows.Input;

namespace CinemaManager.Modules
{
	public interface IModule : INotifyPropertyChanged
	{
		bool IsVisible { get; set; }
		string Title { get; }

		ICommand CloseCommand { get; }
	}
}