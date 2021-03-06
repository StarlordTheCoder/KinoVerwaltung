﻿// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using CinemaManager.Filter;
using CinemaManager.Infrastructure;
using CinemaManager.Model;
using Microsoft.Practices.Prism.Commands;

namespace CinemaManager.Modules.Room
{
	/// <summary>
	///     Module zum Verwalten der Säle
	/// </summary>
	public class RoomModule : ModuleBase, IRoomModule
	{
		private const int AllowedSelection = 1;
		private RoomViewModel _selectedRoom;

		/// <summary>
		///     Manages The Gui of the Rooms
		/// </summary>
		public RoomModule()
		{
			AddRoomCommand = new DelegateCommand(AddRoom);
			RemoveRoomCommand = new DelegateCommand(RemoveRoom, () => ValueSelected);
			AddRowCommand = new DelegateCommand(() => SelectedRoom.AddRow(), () => ValueSelected);
			RemoveRowCommand = new DelegateCommand(() => SelectedRoom.RemoveRow(), () => SelectedRoom?.SelectedRow != null);
			AddSeatCommand = new DelegateCommand(() => SelectedRoom.AddSeat(), () => SelectedRoom?.SelectedRow != null);
			RemoveSeatCommand = new DelegateCommand(() => SelectedRoom.RemoveSeat(),
				() => SelectedRoom?.SelectedSeats.Any() ?? false);

			RoomFilterConfigurator
				.NumberFilter("Room Number", c => c.RoomNumber);

			RoomFilterConfigurator.FilterChanged += (sender, e) => FilterChanged();
		}

		/// <summary>
		///     Raum Hunzufügen
		/// </summary>
		public ICommand AddRoomCommand { get; }

		/// <summary>
		///     Raum entfernen
		/// </summary>
		public DelegateCommand RemoveRoomCommand { get; }

		/// <summary>
		///     Sitzreie hinzufügen
		/// </summary>
		public DelegateCommand AddRowCommand { get; }

		/// <summary>
		///     Sizreihe Entfernen
		/// </summary>
		public DelegateCommand RemoveRowCommand { get; }

		/// <summary>
		///     Sitz hinzufügen
		/// </summary>
		public DelegateCommand AddSeatCommand { get; }

		/// <summary>
		///     Sitz entfernen
		/// </summary>
		public DelegateCommand RemoveSeatCommand { get; }

		/// <summary>
		///     Filter-Configurator für die Räume
		/// </summary>
		public IFilterConfigurator<RoomModel> RoomFilterConfigurator { get; set; } = new FilterConfigurator<RoomModel>();

		private static IList<RoomModel> RoomModels => Session.Instance.SelectedCinemaModel?.Rooms;

		/// <summary>
		///     Seattypes for showing in GUI
		/// </summary>
		public static IEnumerable<SeatType> SeatTypes => Session.Instance.SelectedCinemaModel?.SeatTypes;

		/// <summary>
		///     True, wenn das Modul aktiv ist.
		/// </summary>
		public override bool Enabled => RoomModels != null;

		/// <summary>
		///     Shows if there is a selected Room
		/// </summary>
		public override bool ValueSelected => SelectedRoom != null;

		/// <summary>
		///     Titel für das Dockingframework
		/// </summary>
		public override string Title => "Rooms";

		/// <summary>
		///     Alle gefilterten Räume
		/// </summary>
		public ObservableCollection<RoomViewModel> Rooms { get; } = new ObservableCollection<RoomViewModel>();

		/// <summary>
		///     Ausgewählter Raum
		/// </summary>
		public RoomViewModel SelectedRoom
		{
			get { return _selectedRoom; }
			set
			{
				if (Equals(value, _selectedRoom)) return;
				if (_selectedRoom != null)
				{
					_selectedRoom.PropertyChanged -= SelectedRoomOnPropertyChanged;
					_selectedRoom.SelectedSeats.CollectionChanged -= SelectedSeatsOnCollectionChanged;
				}
				_selectedRoom = value;
				if (_selectedRoom != null)
				{
					_selectedRoom.PropertyChanged += SelectedRoomOnPropertyChanged;
					_selectedRoom.SelectedSeats.CollectionChanged += SelectedSeatsOnCollectionChanged;
				}
				OnPropertyChanged();
				OnPropertyChanged(nameof(ValueSelected));
				OnPropertyChanged(nameof(SelectedSeat));
				OnModuleDataChanged();
				RaiseCanExecuteChanged();
			}
		}

		/// <summary>
		///     Der ausgewählte Sitz
		/// </summary>
		public SeatViewModel SelectedSeat
		{
			get { return SelectedRoom?.SelectedSeats.FirstOrDefault(); }
			set
			{
				if (SelectedRoom?.SelectedSeats.FirstOrDefault() == null) return;
				SelectedRoom.SelectedSeats[0] = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		///     Fügt einen neuen Raum hinzu
		/// </summary>
		public void AddRoom()
		{
			var room = new RoomModel
			{
				RoomNumber = RoomModels.Any() ? RoomModels.Max(r => r.RoomNumber) + 1 : 1
			};
			RoomModels.Add(room);
			var roomRoomModel = new RoomViewModel(room, AllowedSelection);

			Rooms.Add(roomRoomModel);
			SelectedRoom = roomRoomModel;
		}

		/// <summary>
		///     Entfernt den ausgewählten Raum
		/// </summary>
		public void RemoveRoom()
		{
			RoomModels.Remove(SelectedRoom?.Model);
			Rooms.Remove(SelectedRoom);
			SelectedRoom = Rooms.FirstOrDefault();
		}

		/// <summary>
		///     Aktualisiert die Daten im Modul.
		///     Beispielsweise wenn sich die Daten verändert haben.
		/// </summary>
		public override void Refresh()
		{
			FilterChanged();
		}

		private void SelectedSeatsOnCollectionChanged(object sender,
			NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
		{
			RaiseCanExecuteChanged();
			OnPropertyChanged(nameof(SelectedSeat));
		}

		private void SelectedRoomOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			RaiseCanExecuteChanged();
		}

		private void RaiseCanExecuteChanged()
		{
			AddSeatCommand.RaiseCanExecuteChanged();
			RemoveSeatCommand.RaiseCanExecuteChanged();
			RemoveRowCommand.RaiseCanExecuteChanged();
			RemoveRoomCommand.RaiseCanExecuteChanged();
			AddRowCommand.RaiseCanExecuteChanged();
		}

		private void FilterChanged()
		{
			if (RoomModels != null)
			{
				var filteredData = RoomFilterConfigurator.FilterData(RoomModels);
				Rooms.Clear();

				foreach (var room in filteredData.OrderBy(r => r.RoomNumber))
				{
					Rooms.Add(new RoomViewModel(room, AllowedSelection));
				}
			}

			OnPropertyChanged(nameof(Enabled));
		}
	}
}