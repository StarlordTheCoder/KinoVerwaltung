using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManager.Filter
{
	public interface IFilterVisitor
	{
		void Visit(IFilterAcceptor acceptor);
	}
}
