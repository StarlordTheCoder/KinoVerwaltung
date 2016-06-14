// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CinemaManager.Filter;
using CinemaManager.Model;

namespace CinemaManager.Modules.Room
{
	public class RoomModule : ModuleBase
	{
		private RoomModel _selectetRoom;

		public RoomModule()
		{
			RoomFilterConfigurator
				.StringFilter(new StringFilter<RoomModel>("RoomNumber", c => c.RoomNumber.ToString()));

			RoomFilterConfigurator.FilterChanged += (sender, e) => FilterChanged();

			Refresh();
		}

		public override string Title => "Room Module";
		public ObservableCollection<RoomModel> RoomList { get; } = new ObservableCollection<RoomModel>();
		public IFilterConfigurator<RoomModel> RoomFilterConfigurator { get; } = new FilterConfigurator<RoomModel>();

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

		private IList<RoomModel> RoomModels => Session.Instance.SelectedCinemaModel.Rooms;

		public override void Refresh()
		{
			var list = Session.Instance.SelectedCinemaModel?.Rooms;

			RoomList.Clear();
			if (list?.Any() ?? false)
			{
				list.ForEach(l => RoomList.Add(l));
			}
		}

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