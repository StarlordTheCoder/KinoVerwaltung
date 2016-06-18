// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

#if DEBUG

using System;
using System.Linq;
using System.Threading.Tasks;
using CinemaManager.Infrastructure;
using CinemaManager.MainView;
using Metrics;
using Microsoft.Owin.Hosting;

namespace CinemaManager.Metrics
{
	/// <summary>
	///     Wrapper für Metriken
	/// </summary>
	public class MetricsManager
	{
		private MainWindowViewModel _mainWindowViewModel;

		/// <summary>
		///     Starte die lokalen Metriken.
		/// </summary>
		/// <param name="mainWindowViewModel">Das allgemeine Viewmodel zum holend aller Daten.</param>
		public async void StartMetrics(MainWindowViewModel mainWindowViewModel)
		{
			_mainWindowViewModel = mainWindowViewModel;

			await Task.Run(() =>
			{
				var url = "http://localhost:1235/";

				WebApp.Start<Startup>(url);

				AddCustomValues();
				RegisterHealthChecks();

				Console.WriteLine($"Metrics ready: {url}metrics");
			});
		}

		private void AddCustomValues()
		{
			Metric.Gauge("# Open Modules", () => _mainWindowViewModel.Modules.Count(m => m.IsVisible), Unit.Items);
		}

		private static void RegisterHealthChecks()
		{
			HealthChecks.RegisterHealthCheck("CinemaModels loaded", () =>
			{
				if (Session.Instance.DataModel.CinemasModel != null)
				{
					return HealthCheckResult.Healthy("CinemaModels loaded");
				}
				return HealthCheckResult.Unhealthy("No CinemaModels loaded");
			});
		}
	}
}

#endif