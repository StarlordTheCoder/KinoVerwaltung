using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CinemaManager.Filter
{
	public interface IFilterAcceptor
	{
		object Value { get; set; }

		ICommand RemoveFilterCommand { get; }

		void Accept(IFilterVisitor visitor);
	}
}
