// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CinemaManager.Properties;
using Microsoft.Practices.Prism.Commands;

namespace CinemaManager.Modules
{
	/// <summary>
	///     Grundimplementation von <see cref="IModule" />
	/// </summary>
	public abstract class ModuleBase : IModule
	{
		private bool _isVisible = true;

		/// <summary>
		///     Initialisiert Commands
		/// </summary>
		public ModuleBase()
		{
			CloseCommand = new DelegateCommand(() => IsVisible = false);
		}

		/// <summary>
		///     Wird das Modul angezeigt
		/// </summary>
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

		/// <summary>
		///     Titel für das Dockingframework
		/// </summary>
		public abstract string Title { get; }

		/// <summary>
		///     Command für das Dockingframework
		/// </summary>
		public ICommand CloseCommand { get; }

		/// <summary>
		///     Aktualisiert die Daten im Modul. 
		///     Beispielsweise wenn sich die Daten verändert haben.
		/// </summary>
		public abstract void Refresh();

		/// <summary>Tritt ein, wenn sich ein Eigenschaftswert ändert.</summary>
		public event PropertyChangedEventHandler PropertyChanged;


		/// <summary>
		///     Event invokator for <see cref="PropertyChanged"/>
		/// </summary>
		/// <param name="propertyName">Property that changed</param>
		[NotifyPropertyChangedInvocator]
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}