// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Linq;

namespace CinemaManager.Filter.Number
{
	/// <summary>
	///     Ermöglicht das Filtern nach einem String.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class NumberFilter<T> : FilterBase<T>, INumberFilter<T>
	{
		private readonly Func<T, int>[] _valueToCompareTo;
		private int? _number;

		public NumberFilter(string label, params Func<T, int>[] valueToCompareTo)
		{
			_valueToCompareTo = valueToCompareTo;
			IsEnabled = true;
			Label = label;
		}

		/// <summary>
		///     Text, nach welchem gefiltert wird
		/// </summary>
		public int? Number
		{
			get { return _number; }
			set
			{
				if (Equals(value, _number)) return;
				_number = value;
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
			return !Number.HasValue || _valueToCompareTo.Any(v => v.Invoke(data) == Number.Value);
		}
	}
}