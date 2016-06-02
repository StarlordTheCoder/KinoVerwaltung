using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using CinemaManager.Model;
using CinemaManager.Properties;
using Microsoft.Win32;

namespace CinemaManager.MainView
{
	public class DataSourceService
	{
		private readonly IDataModel _data;

		private static Session Session => Session.Instance;

		public DataSourceService(IDataModel data)
		{
			_data = data;
			LoadData(Session.DataPath);

			OpenFileCommand = new RoutedUICommand("Open...", "Open...", typeof(MainWindow), new InputGestureCollection
			{
				new KeyGesture(Key.O, ModifierKeys.Control)
			});
			SynchronizeCommand = new RoutedUICommand("Synchronize", "Synchronize", typeof(MainWindow), new InputGestureCollection
			{
				new KeyGesture(Key.S, ModifierKeys.Control)
			});
			SaveFileCommand = new RoutedUICommand("Save as...", "Save as...", typeof(MainWindow), new InputGestureCollection
			{
				new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Alt)
			});

			CommandBindings = new List<CommandBinding>
			{
				new CommandBinding(OpenFileCommand, (sender, e) => OpenDataFile()),
				new CommandBinding(SynchronizeCommand, (sender, e) => SynchronizeData()),
				new CommandBinding(SaveFileCommand, (sender, e) => SaveDataFile()),
			};
		}

		public IList<CommandBinding> CommandBindings { get; }
		public RoutedUICommand OpenFileCommand { get; }
		public RoutedUICommand SynchronizeCommand { get; }
		public RoutedUICommand SaveFileCommand { get; }

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
				_data.Save();
			}
		}

		public void LoadData(string fileName)
		{
			Session.DataPath = fileName;

			try
			{
				_data.Load();
			}
			catch
			{
				MessageBox.Show("Cannot interpret data!");
			}
		}

		private void SynchronizeData()
		{
			_data.Save();
			_data.Load();
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