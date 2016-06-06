using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CinemaManager.Model;
using CinemaManager.Properties;

namespace CinemaManager.Filter
{
	public class DateFilter<T> : FilterBase<T>
	{
		private readonly Func<T, DateTime?> _valueToCompareTo;
		private DateTime? _date;

		public DateFilter(Func<T, DateTime?> valueToCompareTo, string label, bool isEnabled = true)
		{
			_valueToCompareTo = valueToCompareTo;
			Date = DateTime.UtcNow;
			IsEnabled = isEnabled;
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
			return Equals(_valueToCompareTo.Invoke(data), Date);
		}
	}
}
