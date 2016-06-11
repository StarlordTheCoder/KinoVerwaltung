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
		private DateTime? _endDate;
		private DateTime? _startDate;

		public DateFilter(string label, params Func<T, DateTime?>[] valueToCompareTo)
		{
			_valueToCompareTo = valueToCompareTo;
			_startDate = DateTime.UtcNow - TimeSpan.FromDays(1);
			_endDate = DateTime.UtcNow;
			IsEnabled = true;
			Label = label;
		}

		/// <summary>
		///     Startdatum. Von-Datum
		/// </summary>
		public DateTime? StartDate
		{
			get { return _startDate; }
			set
			{
				if (!Equals(value, _startDate) && _endDate?.Date >= value?.Date)
				{
					_startDate = value;
				}

				OnPropertyChanged();
			}
		}

		/// <summary>
		///     Enddatum. Bis-Datum
		/// </summary>
		public DateTime? EndDate
		{
			get { return _endDate; }
			set
			{
				if (!Equals(value, _endDate) && value?.Date >= _startDate?.Date)
				{
					_endDate = value;
				}

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
				var result = v.Invoke(data);

				return
					result?.Date <= EndDate?.Date &&
					result.Value.Date >= StartDate?.Date;
			});
		}
	}
}