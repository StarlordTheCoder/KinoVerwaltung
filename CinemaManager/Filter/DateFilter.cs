// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Linq;

namespace CinemaManager.Filter
{
	/// <summary>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DateFilter<T> : FilterBase<T>, IDateFilter<T>
	{
		private readonly Func<T, DateTime?>[] _valueToCompareTo;
		private DateTime? _date;

		public DateFilter(string label, params Func<T, DateTime?>[] valueToCompareTo)
		{
			_valueToCompareTo = valueToCompareTo;
			Date = DateTime.UtcNow;
			IsEnabled = true;
			Label = label;
		}

		public DateTime? Date
		{
			get { return _date; }
			set
			{
				if (Equals(value, _date)) return;
				_date = value;
				OnPropertyChanged();
			}
		}

		public override bool Check(T data)
		{
			return _valueToCompareTo.Any(v => Equals(v.Invoke(data), Date));
		}
	}
}