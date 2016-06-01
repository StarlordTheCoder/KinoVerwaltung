// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using CinemaManager.Model;
using CinemaManager.Modules;
using CinemaManager.Modules.Cinema;
using CinemaManager.Modules.User;
using CinemaManager.Properties;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Win32;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace CinemaManager
{
	public class MainWindowViewModel
	{
		private readonly XmlLayoutSerializer _layoutSerializer;
		private readonly IDataModel _data;

		public MainWindowViewModel(DockingManager dockManager)
		{
			LoadLayoutCommand = new DelegateCommand(LoadLayout);
			SaveLayoutCommand = new DelegateCommand(SaveLayout);
			OpenFileCommand = new DelegateCommand(OpenFile);
			RefreshCommand = new DelegateCommand(LoadData);
			SaveFileCommand = new DelegateCommand(SaveFile);
			AboutCommand = new DelegateCommand(() => MessageBox.Show(AboutMessage));

			_data = new DataModel();
			LoadData();

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
				Filter = "Satan's layout (*.satan)|*.satan"
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
				Filter = "Satan's layout (*.satan)|*.satan"
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

		public void SaveFile()
		{
			var dialog = new SaveFileDialog
			{
				DefaultExt = ".satan",
				Filter = "Satan's children (*.satanData)|*.satanData",
				InitialDirectory = Path.GetDirectoryName(Session.FullDataPath)
			};

			var result = dialog.ShowDialog();
			if (result.HasValue && result.Value)
			{
				if (Settings.Default.DataPath != dialog.FileName)
				{
					Settings.Default.DataPath = dialog.FileName;
					Settings.Default.Save();
				}
				_data.Save();
			}
		}

		public void LoadData()
		{
			LoadData(Session.FullDataPath);
		}

		public void LoadData(string fileName)
		{
			if (Settings.Default.DataPath != fileName)
			{
				Settings.Default.DataPath = fileName;
				Settings.Default.Save();
			}

			try
			{
				_data.Load();
			}
			catch
			{
				MessageBox.Show("Cannot interpret data!");
			}
		}

		private void OpenFile()
		{
			var dialog = new OpenFileDialog
			{
				DefaultExt = ".satan",
				Filter = "Satan's children (*.satanData)|*.satanData",
				InitialDirectory = Path.GetDirectoryName(Session.FullDataPath)
			};

			var result = dialog.ShowDialog();
			if (result.HasValue && result.Value)
			{
				LoadData(dialog.FileName);
			}
		}

		public ICommand OpenFileCommand { get; }
		public ICommand RefreshCommand { get; }
		public ICommand SaveFileCommand { get; }

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
				var assembly = Assembly.GetExecutingAssembly();
				var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
				return fvi.FileVersion;
			}
		}

		#endregion

		public void LoadFile(string startupFile)
		{
			if (!string.IsNullOrEmpty(startupFile))
			{
				try
				{
					LoadData(startupFile);
				}
				catch
				{
					try
					{
						LoadLayout(startupFile);
					}
					catch
					{
						//File invalid
						MessageBox.Show("Cannot read File content!");
					}
				}

				
			}
		}
	}
}