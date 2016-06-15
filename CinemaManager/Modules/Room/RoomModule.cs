// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Collections.ObjectModel;
using CinemaManager.Filter;
using CinemaManager.Filter.Number;
using CinemaManager.Model;

namespace CinemaManager.Modules.Room
{
	public class RoomModule : ModuleBase
	{
		private RoomModel _selectetRoom;

		public RoomModule()
		{
			RoomFilterConfigurator
				.NumberFilter(new NumberFilter<RoomModel>("Room Number", c => c.RoomNumber));

			RoomFilterConfigurator.FilterChanged += (sender, e) => FilterChanged();
		}

		/// <summary>
		///     Titel für das Dockingframework
		/// </summary>
		public override string Title => "Room Module";
		/// <summary>
		///     Alle gefilterten Räume
		/// </summary>
		public ObservableCollection<RoomModel> Rooms { get; } = new ObservableCollection<RoomModel>();

		/// <summary>
		///     Filter-Configurator für die Räume
		/// </summary>
		public IFilterConfigurator<RoomModel> RoomFilterConfigurator { get; } = new FilterConfigurator<RoomModel>();

		/// <summary>
		///     Ausgewählter Raum
		/// </summary>
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

		private static IList<RoomModel> RoomModels => Session.Instance.SelectedCinemaModel.Rooms;

		/// <summary>
		///     Aktualisiert die Daten im Modul.
		///     Beispielsweise wenn sich die Daten verändert haben.
		/// </summary>
		public override void Refresh()
		{
			FilterChanged();
		}

		private void FilterChanged()
		{
			var filteredData = RoomFilterConfigurator.FilterData(RoomModels);
			Rooms.Clear();

			foreach (var room in filteredData)
			{
				Rooms.Add(room);
			}
		}
	}
}