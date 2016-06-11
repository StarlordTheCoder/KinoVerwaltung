// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;
using System.Linq;
using CinemaManager.Modules;

namespace CinemaManager.Filter
{
	public class ComplexFilter<T, TM> : FilterBase<T>, IComplexFilter<T, TM> where TM : IModule
	{
		private readonly Func<TM, IEnumerable<T>> _valueToCompareTo;

		public ComplexFilter(string label, TM module, Func<TM, IEnumerable<T>> valueToCompareTo)
		{
			_valueToCompareTo = valueToCompareTo;
			Module = module;
			IsEnabled = true;
			Label = label;
		}

		/// <summary>
		///     Überprüft, ob die <paramref name="data" /> diesem Filter entsprechen.
		/// </summary>
		/// <param name="data">Daten, welche zu prüfen sind</param>
		/// <returns>True, wenn die Daten valid sind</returns>
		public override bool Check(T data)
		{
			return _valueToCompareTo.Invoke(Module).Contains(data);
		}

		public TM Module { get; }
	}
}