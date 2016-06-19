// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CinemaManager.Properties;

namespace CinemaManager.Filter
{
	/// <summary>
	///     Contains the Methods and Properties, every Filter must have
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class FilterBase<T> : IFilter<T>, INotifyPropertyChanged
	{
		private bool _isEnabled;
		private string _label;

		/// <summary>
		///     Ob der Filter aktiv ist
		/// </summary>
		public bool IsEnabled
		{
			get { return _isEnabled; }
			set
			{
				if (Equals(value, _isEnabled)) return;
				_isEnabled = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		///     Wird im GUI neben dem Filter angezeigt
		/// </summary>
		public string Label
		{
			get { return _label; }
			protected set
			{
				if (Equals(_label, value)) return;
				_label = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		///     Überprüft, ob die <paramref name="data" /> diesem Filter entsprechen.
		/// </summary>
		/// <param name="data">Daten, welche zu prüfen sind</param>
		/// <returns>True, wenn die Daten valid sind</returns>
		public abstract bool Check(T data);

		/// <summary>
		///     Der Filter hat sich verändert
		/// </summary>
		public event EventHandler FilterChanged;

		/// <summary>Tritt ein, wenn sich ein Eigenschaftswert ändert.</summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		///     Event invokator for <see cref="PropertyChanged" />
		/// </summary>
		/// <param name="propertyName">Property that changed</param>
		[NotifyPropertyChangedInvocator]
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			OnFilterChanged();
		}

		/// <summary>
		///     Event invokator for <see cref="FilterChanged" />
		/// </summary>
		protected void OnFilterChanged()
		{
			FilterChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}