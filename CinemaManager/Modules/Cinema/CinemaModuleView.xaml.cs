// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Windows;
using CinemaManager.Model;

namespace CinemaManager.Modules.Cinema
{
	/// <summary>
	///     Interaction logic for CinemaModuleView.xaml
	/// </summary>
	public partial class CinemaModuleView
	{
		public CinemaModuleView()
		{
			InitializeComponent();
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			var data = new List<CinemaModel>
			{
				new CinemaModel
				{
					Name = "Pascals Kinö",
					Address = "Beschtee"
				}
			};

			var module = DataContext as CinemaModule;

			var configurator = module?.CinemaFilterConfigurator;
			var result = configurator?.FilterData(data);
		}

		private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{

		}
	}
}