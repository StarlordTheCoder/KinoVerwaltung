// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace CinemaManager.MainView
{
	public class DataSourceService : IDataSourceService
	{
		public DataSourceService()
		{
			LoadData(Session.DataPath);

			OpenFileCommand = new RoutedUICommand("Open...", "Open...", typeof(MainWindow), new InputGestureCollection
			{
				new KeyGesture(Key.O, ModifierKeys.Control)
			});
			SynchronizeCommand = new RoutedUICommand("Synchronize", "Synchronize", typeof(MainWindow),
				new InputGestureCollection
				{
					new KeyGesture(Key.S, ModifierKeys.Control)
				});
			SaveFileCommand = new RoutedUICommand("Save as...", "Save as...", typeof(MainWindow),
				new InputGestureCollection
				{
					new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift)
				});

			CommandBindings = new List<CommandBinding>
			{
				new CommandBinding(OpenFileCommand, (sender, e) => OpenDataFile()),
				new CommandBinding(SynchronizeCommand, (sender, e) => SynchronizeData()),
				new CommandBinding(SaveFileCommand, (sender, e) => SaveDataFile())
			};
		}

		private static Session Session => Session.Instance;

		public IList<CommandBinding> CommandBindings { get; }
		public RoutedUICommand OpenFileCommand { get; }
		public RoutedUICommand SynchronizeCommand { get; }
		public RoutedUICommand SaveFileCommand { get; }

		public void LoadData(string path)
		{
			Session.DataPath = path;

			try
			{
				Session.DataModel.Load();
			}
			catch
			{
				MessageBox.Show("Cannot interpret data!");
			}
		}

		private void SaveDataFile()
		{
			var dialog = new SaveFileDialog
			{
				DefaultExt = ".satan",
				Filter = "Satan's children (*.satanData)|*.satanData",
				InitialDirectory = Path.GetDirectoryName(Session.DataPath)
			};

			var result = dialog.ShowDialog();
			if (result.HasValue && result.Value)
			{
				Session.DataPath = dialog.FileName;
				Session.DataModel.Save();
			}
		}

		private void SynchronizeData()
		{
			Session.DataModel.Save();
			Session.DataModel.Load();
		}

		private void OpenDataFile()
		{
			var dialog = new OpenFileDialog
			{
				DefaultExt = ".satan",
				Filter = "Satan's children (*.satanData)|*.satanData",
				InitialDirectory = Path.GetDirectoryName(Session.DataPath)
			};

			var result = dialog.ShowDialog();
			if (result.HasValue && result.Value)
			{
				LoadData(dialog.FileName);
			}
		}
	}
}