// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Linq;

namespace CinemaManager.Filter.String
{
	/// <summary>
	///     Ermöglicht das Filtern nach einem String.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class StringFilter<T> : FilterBase<T>, IStringFilter<T>
	{
		private readonly Func<T, string>[] _valueToCompareTo;
		private string _text;

		/// <summary>
		///     Constructor
		/// </summary>
		/// <param name="label">Label for GUI</param>
		/// <param name="valueToCompareTo">Configurable values, which the User-Input is compared to</param>
		public StringFilter(string label, params Func<T, string>[] valueToCompareTo)
		{
			_valueToCompareTo = valueToCompareTo;
			Text = string.Empty;
			IsEnabled = true;
			Label = label;
		}

		/// <summary>
		///     Text, nach welchem gefiltert wird
		/// </summary>
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

		/// <summary>
		///     Überprüft, ob die <paramref name="data" /> diesem Filter entsprechen.
		/// </summary>
		/// <param name="data">Daten, welche zu prüfen sind</param>
		/// <returns>True, wenn die Daten valid sind</returns>
		public override bool Check(T data)
		{
			return _valueToCompareTo.Any(v =>
			{
				var value = v.Invoke(data);

				if (string.IsNullOrEmpty(value) && string.IsNullOrEmpty(Text))
				{
					return true;
				}

				return value != null && value.ToLower().Contains(Text.ToLower());
			});
		}
	}
}