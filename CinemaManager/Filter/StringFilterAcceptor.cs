using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace CinemaManager.Filter
{
	public class StringFilterAcceptor : IFilterAcceptor
	{
		public StringFilterAcceptor(Action<IFilterAcceptor> removeFilterAction)
		{
			RemoveFilterCommand = new DelegateCommand<IFilterAcceptor>(removeFilterAction);
			Value = string.Empty;
		}

		public object Value { get; set; }
		public ICommand RemoveFilterCommand { get; }
		public void Accept(IFilterVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
