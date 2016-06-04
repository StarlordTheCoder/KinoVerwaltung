using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CinemaManager.Filter
{
	public interface IFilterConfigurator
	{
		ObservableCollection<IFilterAcceptor> ComplexAcceptors { get; }
		ObservableCollection<IFilterAcceptor> DateAcceptors { get; }
		ObservableCollection<IFilterAcceptor> StringAcceptors { get; }

		IFilterConfigurator ComplexFilter(IFilterAcceptor acceptor);
		IFilterConfigurator DateFilter(IFilterAcceptor acceptor);
		IFilterConfigurator StringFilter(IFilterAcceptor acceptor);

		void FilterData(IFilterVisitor visitor);
	}
}