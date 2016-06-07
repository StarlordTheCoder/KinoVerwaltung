// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Linq;
using System.Threading.Tasks;
using CinemaManager.MainView;
using Metrics;
using Microsoft.Owin.Hosting;

namespace CinemaManager.Metrics
{
	/// <summary>
	/// Wrapper für Metriken
	/// </summary>
	public class MetricsManager
	{
		private MainWindowViewModel _mainWindowViewModel;

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
			Metric.Gauge("# Open Modules", () => _mainWindowViewModel.Modules.Count(m => m?.IsVisible ?? false), Unit.Items);
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