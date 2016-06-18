// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;
using System.Linq;
using CinemaManager.Modules;

namespace CinemaManager.Filter.Complex
{
	public class ComplexFilter<T, TM> : FilterBase<T>, IComplexFilter<T, TM> where TM : IModule
	{
		private readonly Func<TM, IEnumerable<T>> _valueToCompareTo;

		public ComplexFilter(TM module, Func<TM, IEnumerable<T>> valueToCompareTo)
		{
			_valueToCompareTo = valueToCompareTo;
			Module = module;
			Module.ModuleDataChanged += (sender, e) => OnFilterChanged();
			IsEnabled = true;
			Label = module.Title;
		}

		/// <summary>
		///     Überprüft, ob die <paramref name="data" /> diesem Filter entsprechen.
		/// </summary>
		/// <param name="data">Daten, welche zu prüfen sind</param>
		/// <returns>True, wenn die Daten valid sind</returns>
		public override bool Check(T data)
		{
			var list = _valueToCompareTo.Invoke(Module)?.ToList();
			return list != null && list.Contains(data);
		}

		/// <summary>
		///     Referenz zum Modul, welches gefilter wird
		/// </summary>
		public TM Module { get; }
	}
}