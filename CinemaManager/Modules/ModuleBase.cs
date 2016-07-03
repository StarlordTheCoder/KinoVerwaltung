// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Windows.Input;
using CinemaManager.Infrastructure;
using Microsoft.Practices.Prism.Commands;

namespace CinemaManager.Modules
{
	/// <summary>
	///     Grundimplementation von <see cref="IModule" />
	/// </summary>
	public abstract class ModuleBase : NotifyPropertyChangedBase, IModule
	{
		private bool _isVisible = true;

		/// <summary>
		///     Initialisiert Commands
		/// </summary>
		protected ModuleBase()
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
		///     True, wenn das Modul aktiv ist.
		/// </summary>
		public abstract bool Enabled { get; }

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

		/// <summary>
		///     Die Moduldate, welche für die Filter der anderen Module relevant sind, haben sich verändert.
		/// </summary>
		public event EventHandler ModuleDataChanged;

		/// <summary>
		///     True if there is a selected value. True if no value can be selected
		/// </summary>
		public virtual bool ValueSelected => true;

		/// <summary>
		///     Event invokator for <see cref="ModuleDataChanged" />
		/// </summary>
		protected void OnModuleDataChanged()
		{
			ModuleDataChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}