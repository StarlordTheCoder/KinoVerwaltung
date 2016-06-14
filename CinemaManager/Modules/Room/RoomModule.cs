using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaManager.Filter;
using CinemaManager.Model;
using CinemaManager.Properties;

namespace CinemaManager.Modules.Room
{
	public class RoomModule : ModuleBase
	{
		private RoomModel _selectetRoom;
		public override string Title => "Room Module";
		public ObservableCollection<RoomModel> RoomList { get; } = new ObservableCollection<RoomModel>();
		public IFilterConfigurator<RoomModel> RoomFilterConfigurator { get; } = new FilterConfigurator<RoomModel>();
		public override void Refresh()
		{
			var list = Session.Instance.SelectedCinemaModel?.Rooms;

			RoomList.Clear();
			if (list?.Any() ?? false)
			{
				list.ForEach(l => RoomList.Add(l));
			}
		}

		public RoomModel SelectetRoom
		{
			get { return _selectetRoom; }
			set
			{
				if (Equals(value, _selectetRoom)) return;
				_selectetRoom = value;
				OnPropertyChanged();
			}
		}

		public RoomModule()
		{
			RoomFilterConfigurator
				.StringFilter(new StringFilter<RoomModel>("RoomNumber", c => c.RoomNumber.ToString()));

			RoomFilterConfigurator.FilterChanged += (sender, e) => FilterChanged();

			Refresh();
		}

		private IList<RoomModel> RoomModels => Session.Instance.SelectedCinemaModel.Rooms;

		private void FilterChanged()
		{
			var filteredData = RoomFilterConfigurator.FilterData(RoomModels);
			RoomList.Clear();

			foreach (var room in filteredData)
			{
				RoomList.Add(room);
			}
		}
	}
}
