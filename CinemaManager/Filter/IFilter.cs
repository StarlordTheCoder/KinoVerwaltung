using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CinemaManager.Model;

namespace CinemaManager.Filter
{
	public interface IFilter<in T>
	{
		bool IsEnabled { get; set; }

		string Label { get; }

		bool Check(T data);
	}
}
