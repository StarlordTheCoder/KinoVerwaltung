using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CinemaManager.Filter
{
	public class FilterConfigurator : IFilterConfigurator
	{
		public ObservableCollection<IFilterAcceptor> StringAcceptors { get; } = new ObservableCollection<IFilterAcceptor>();
		public ObservableCollection<IFilterAcceptor> DateAcceptors { get; } = new ObservableCollection<IFilterAcceptor>();
		public ObservableCollection<IFilterAcceptor> ComplexAcceptors { get; } = new ObservableCollection<IFilterAcceptor>();

		public IFilterConfigurator StringFilter(IFilterAcceptor acceptor)
		{
			StringAcceptors.Add(acceptor);

			return this;
		}

		public IFilterConfigurator DateFilter(IFilterAcceptor acceptor)
		{
			DateAcceptors.Add(acceptor);

			return this;
		}

		public IFilterConfigurator ComplexFilter(IFilterAcceptor acceptor)
		{
			ComplexAcceptors.Add(acceptor);

			return this;
		}

		public void FilterData(IFilterVisitor visitor)
		{
			foreach (var acceptor in DateAcceptors.Concat(ComplexAcceptors).Concat(StringAcceptors))
			{
				acceptor.Accept(visitor);
			}
		}
	}
}
