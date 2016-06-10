// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Linq;

namespace CinemaManager.Filter
{
	/// <summary>
	///     Filter
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class DateFilter<T> : FilterBase<T>, IDateFilter<T>
	{
		private readonly Func<T, DateTime?>[] _valueToCompareTo;
		private DateTime? _endtDate;
		private DateTime? _startDate;

		public DateFilter(string label, params Func<T, DateTime?>[] valueToCompareTo)
		{
			_valueToCompareTo = valueToCompareTo;
			StartDate = DateTime.UtcNow - TimeSpan.FromDays(1);
			StartDate = DateTime.UtcNow;
			IsEnabled = true;
			Label = label;
		}

		public DateTime? StartDate
		{
			get { return _startDate; }
			set
			{
				//TODO Check if EndDate still valid
				if (Equals(value, _startDate)) return;
				_startDate = value;
				OnPropertyChanged();
			}
		}

		public DateTime? EndDate
		{
			get { return _endtDate; }
			set
			{
				//TODO Check if StartDate still valid
				if (Equals(value, _endtDate)) return;
				_endtDate = value;
				OnPropertyChanged();
			}
		}

		public override bool Check(T data)
		{
			return _valueToCompareTo.Any(v =>
			{
				var result = v.Invoke(data);

				return
					result?.Date <= StartDate?.Date &&
					result.Value.Date >= StartDate?.Date;
			});
		}
	}
}