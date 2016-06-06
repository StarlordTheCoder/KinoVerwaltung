﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using CinemaManager.Properties;

namespace CinemaManager.Filter
{
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

		/// <summary>Tritt ein, wenn sich ein Eigenschaftswert ändert.</summary>
		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
