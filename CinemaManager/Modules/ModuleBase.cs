// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CinemaManager.Properties;
using Microsoft.Practices.Prism.Commands;

namespace CinemaManager.Modules
{
	public abstract class ModuleBase : IModule
	{
		private bool _isVisible;

		public ModuleBase()
		{
			CloseCommand = new DelegateCommand(() => IsVisible = false);
		}

		public bool IsVisible
		{
			get { return _isVisible; }
			set
			{
				if (value == _isVisible) return;

				_isVisible = value;
				OnPropertyChanged();
			}
		}

		public abstract string Title { get; }
		public ICommand CloseCommand { get; }


		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}