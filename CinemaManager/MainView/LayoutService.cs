using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using CinemaManager.Properties;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Win32;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace CinemaManager.MainView
{
	public class LayoutService
	{
		private readonly DockingManager _dockingManager;

		public LayoutService(DockingManager dockingManager)
		{
			_dockingManager = dockingManager;

			OpenLayoutCommand = new RoutedUICommand("Open...", "Open...", typeof(MainWindow), new InputGestureCollection
			{
				new KeyGesture(Key.L, ModifierKeys.Control | ModifierKeys.Shift)
			});
			SaveAsLayoutCommand = new RoutedUICommand("Save as...", "Save as...", typeof(MainWindow), new InputGestureCollection
			{
				new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift | ModifierKeys.Alt)
			});

			CommandBindings = new List<CommandBinding>
			{
				new CommandBinding(OpenLayoutCommand, (sender, e) => LoadLayoutFile()),
				new CommandBinding(SaveAsLayoutCommand, (sender, e) => SaveLayoutFile())
			};
		}

		public void LoadLayout(string path)
		{
			if (Session.FullLayoutPath != path)
			{
				Settings.Default.LayoutPath = path;
				Settings.Default.Save();
			}

			var folder = Path.GetDirectoryName(Session.FullDataPath);
			if (folder != null && !Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}

			try
			{
				using (var stream = File.Open(Session.FullDataPath, FileMode.OpenOrCreate))
				{
					new XmlLayoutSerializer(_dockingManager).Serialize(stream);
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Error loading layout!");
			}
		}

		private void LoadLayout()
		{
			LoadLayout(Session.FullLayoutPath);
		}

		private void LoadLayoutFile()
		{
			var dialog = new OpenFileDialog
			{
				DefaultExt = ".satan",
				Filter = "Satan's layout (*.satan)|*.satan",
				InitialDirectory = Path.GetDirectoryName(Session.FullLayoutPath)
			};

			var result = dialog.ShowDialog();
			if (result.HasValue && result.Value)
			{
				LoadLayout(dialog.FileName);
			}
		}

		private void SaveLayoutFile()
		{
			var dialog = new SaveFileDialog
			{
				DefaultExt = ".satan",
				Filter = "Satan's layout (*.satan)|*.satan",
				InitialDirectory = Path.GetDirectoryName(Session.FullLayoutPath)
			};

			var result = dialog.ShowDialog();
			if (result.HasValue && result.Value)
			{
				new XmlLayoutSerializer(_dockingManager).Serialize(dialog.FileName);
			}
		}

		public RoutedUICommand OpenLayoutCommand { get; }
		public RoutedUICommand SaveAsLayoutCommand { get; }
		public IList<CommandBinding> CommandBindings { get; }
	}
}
