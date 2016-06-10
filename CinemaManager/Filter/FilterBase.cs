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

		public abstract bool Check(T data);
		public event EventHandler FilterChangedEvent;

		/// <summary>Tritt ein, wenn sich ein Eigenschaftswert ändert.</summary>
		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			OnFilterChangedEvent();
		}

		protected void OnFilterChangedEvent()
		{
			FilterChangedEvent?.Invoke(this, EventArgs.Empty);
		}
	}
}