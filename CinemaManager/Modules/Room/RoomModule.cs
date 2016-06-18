// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Collections.ObjectModel;
using CinemaManager.Filter;
using CinemaManager.Filter.Number;
using CinemaManager.Infrastructure;
using CinemaManager.Model;

namespace CinemaManager.Modules.Room
{
	public class RoomModule : ModuleBase
	{
		private RoomViewModel _selectedRoom;

		public RoomModule()
		{
			RoomFilterConfigurator
				.NumberFilter(new NumberFilter<RoomModel>("Room Number", c => c.RoomNumber));

			RoomFilterConfigurator.FilterChanged += (sender, e) => FilterChanged();
		}

		/// <summary>
		///     True, wenn das Modul aktiv ist.
		/// </summary>
		public override bool Enabled => RoomModels != null;

		/// <summary>
		///     Titel für das Dockingframework
		/// </summary>
		public override string Title => "Room Module";

		/// <summary>
		///     Alle gefilterten Räume
		/// </summary>
		public ObservableCollection<RoomViewModel> Rooms { get; } = new ObservableCollection<RoomViewModel>();

		/// <summary>
		///     Filter-Configurator für die Räume
		/// </summary>
		public IFilterConfigurator<RoomModel> RoomFilterConfigurator { get; } = new FilterConfigurator<RoomModel>();

		/// <summary>
		///     Ausgewählter Raum
		/// </summary>
		public RoomViewModel SelectedRoom
		{
			get { return _selectedRoom; }
			set
			{
				if (Equals(value, _selectedRoom)) return;
				_selectedRoom = value;
				OnPropertyChanged();
			}
		}

		private static IList<RoomModel> RoomModels => Session.Instance.SelectedCinemaModel?.Rooms;

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
			if (RoomModels != null)
			{
				var filteredData = RoomFilterConfigurator.FilterData(RoomModels);
				Rooms.Clear();

				foreach (var room in filteredData)
				{
					Rooms.Add(new RoomViewModel(room));
				}
			}

			OnPropertyChanged(nameof(Enabled));
		}
	}
}