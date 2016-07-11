// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using CinemaManager.Infrastructure;
using Microsoft.Win32;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace CinemaManager.MainView
{
	/// <summary>
	///     Speichert und Ladet die Layoutdateien
	/// </summary>
	public class LayoutService : ILayoutService
	{
		private DockingManager _dockingManager;

		/// <summary>
		///     Constructor
		/// </summary>
		public LayoutService()
		{
			OpenLayoutCommand = new RoutedUICommand("Open...", "Open...", typeof(MainWindow), new InputGestureCollection
			{
				new KeyGesture(Key.O, ModifierKeys.Alt)
			});
			SaveAsLayoutCommand = new RoutedUICommand("Save as...", "Save as...", typeof(MainWindow),
				new InputGestureCollection
				{
					new KeyGesture(Key.S, ModifierKeys.Alt | ModifierKeys.Shift)
				});
			SaveLayoutCommand = new RoutedUICommand("Save", "Save", typeof(MainWindow),
				new InputGestureCollection
				{
					new KeyGesture(Key.S, ModifierKeys.Alt)
				});

			CommandBindings = new List<CommandBinding>
			{
				new CommandBinding(OpenLayoutCommand, (sender, e) => LoadLayoutFile()),
				new CommandBinding(SaveAsLayoutCommand, (sender, e) => SaveLayoutFile()),
				new CommandBinding(SaveLayoutCommand, (sender, e) => SaveLayout(LayoutPath))
			};
		}

		private static string LayoutPath
		{
			get { return Session.Instance.LayoutPath; }
			set { Session.Instance.LayoutPath = value; }
		}

		/// <summary>
		///     Command for <see cref="LoadLayout" />
		/// </summary>
		public RoutedUICommand OpenLayoutCommand { get; }

		/// <summary>
		///     Command for <see cref="SaveLayoutFile" />
		/// </summary>
		public RoutedUICommand SaveAsLayoutCommand { get; }

		/// <summary>
		///     Command für <see cref="SaveLayout" /> with the current DataPath
		/// </summary>
		public RoutedUICommand SaveLayoutCommand { get; }

		/// <summary>
		///     Die Command-Binding. Verbindet alle Command mit den dazugehörigen Aktionen
		/// </summary>
		public IList<CommandBinding> CommandBindings { get; }

		/// <summary>
		///     Initializes this Service fully
		/// </summary>
		/// <param name="dockingManager">Used to save the layout</param>
		public void Initialize(DockingManager dockingManager)
		{
			_dockingManager = dockingManager;

			LoadLayout(LayoutPath);
		}

		private void LoadLayout(string path)
		{
			LayoutPath = path;

			var folder = Path.GetDirectoryName(LayoutPath);
			if (folder != null && !Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}

			if (File.Exists(LayoutPath))
			{
				try
				{
					using (var stream = File.Open(LayoutPath, FileMode.OpenOrCreate))
					{
						new XmlLayoutSerializer(_dockingManager).Deserialize(stream);
					}
				}
				catch (Exception)
				{
					MessageBox.Show("Error loading layout!");
				}
			}
			else
			{
				SaveLayout(path);
			}
		}

		private void LoadLayoutFile()
		{
			var dialog = new OpenFileDialog
			{
				DefaultExt = ".satan",
				Filter = "Satan's layout (*.satan)|*.satan",
				InitialDirectory = Path.GetDirectoryName(LayoutPath)
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
				InitialDirectory = Path.GetDirectoryName(LayoutPath)
			};

			var result = dialog.ShowDialog();
			if (result.HasValue && result.Value)
			{
				SaveLayout(dialog.FileName);
			}
		}

		private void SaveLayout(string path)
		{
			new XmlLayoutSerializer(_dockingManager).Serialize(path);
		}
	}
}