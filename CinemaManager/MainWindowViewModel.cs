// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CinemaManager.Modules;
using CinemaManager.Modules.Cinema;
using CinemaManager.Modules.User;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Win32;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace CinemaManager
{
	public class MainWindowViewModel
	{
		private readonly XmlLayoutSerializer _layoutSerializer;

		public MainWindowViewModel(DockingManager dockManager)
		{
			LoadLayoutCommand = new DelegateCommand(LoadLayout);
			SaveLayoutCommand = new DelegateCommand(SaveLayout);

			ChangeDataCommand = new DelegateCommand(ChangeData);

			AboutCommand = new DelegateCommand(() => MessageBox.Show(AboutMessage));

			_layoutSerializer = new XmlLayoutSerializer(dockManager);

			_layoutSerializer.LayoutSerializationCallback += (sender, e) =>
			{
				var module = Modules.First(m => m.Title == e.Model.Title);

				//TODO

				e.Cancel = true;
			};

			Modules = new ObservableCollection<IModule> {CinemaModule, UserModule};
		}

		#region Modules

		public IModule CinemaModule { get; } = new CinemaModule();
		public IModule MovieModule { get; }
		public IModule PresentationModule { get; }
		public IModule ReservationModule { get; }
		public IModule UserModule { get; } = new UserModule();

		#endregion

		#region Layout

		public void LoadLayout(string path)
		{
			try
			{
				_layoutSerializer.Deserialize(path);
			}
			catch (Exception)
			{
				MessageBox.Show("Error loading layout!");
			}
		}

		private void LoadLayout()
		{
			var dialog = new OpenFileDialog
			{
				DefaultExt = ".satan",
				Filter = "Satan's children (*.satan)|*.satan"
			};

			var result = dialog.ShowDialog();
			if (result.HasValue && result.Value)
			{
				LoadLayout(dialog.FileName);
			}
		}

		private void SaveLayout()
		{
			var dialog = new SaveFileDialog
			{
				DefaultExt = ".satan",
				Filter = "Satan's children (*.satan)|*.satan"
			};

			var result = dialog.ShowDialog();
			if (result.HasValue && result.Value)
			{
				_layoutSerializer.Serialize(dialog.FileName);
			}
		}

		public ICommand LoadLayoutCommand { get; }
		public ICommand SaveLayoutCommand { get; }

		public ObservableCollection<IModule> Modules { get; }

		#endregion

		#region Data

		private void ChangeData()
		{
			throw new NotImplementedException();
		}

		public ICommand ChangeDataCommand { get; }

		#endregion

		#region About

		public ICommand AboutCommand { get; }

		private static readonly string AboutMessage =
			"Created by Seraphin Rihm, Pascal Honegger & Alain Keller" + Environment.NewLine
			+ "Version " + ApplicationVersion;

		private static string ApplicationVersion
		{
			get
			{
				var assembly = System.Reflection.Assembly.GetExecutingAssembly();
				var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
				return fvi.FileVersion;
			}
		}

		#endregion
	}
}