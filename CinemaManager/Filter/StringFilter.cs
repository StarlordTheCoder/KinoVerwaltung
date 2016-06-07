// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;

namespace CinemaManager.Filter
{
	/// <summary>
	/// Ermöglicht das Filtern nach einem String.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class StringFilter<T> : FilterBase<T>, IStringFilter<T>
	{
		private readonly Func<T, string> _valueToCompareTo;
		private string _text;

		public StringFilter(Func<T, string> valueToCompareTo, string label, bool isEnabled = true)
		{
			_valueToCompareTo = valueToCompareTo;
			Text = string.Empty;
			IsEnabled = isEnabled;
			Label = label;
		}

		public string Text
		{
			get { return _text; }
			set
			{
				if (Equals(value, _text)) return;
				_text = value;
				OnPropertyChanged();
			}
		}

		public override bool Check(T data)
		{
			return _valueToCompareTo.Invoke(data).ToLower().Contains(Text.ToLower());
		}
	}
}