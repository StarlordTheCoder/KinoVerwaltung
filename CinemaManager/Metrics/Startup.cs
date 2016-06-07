// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Text.RegularExpressions;
using Metrics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Owin.Metrics;

namespace CinemaManager.Metrics
{
	/// <summary>
	/// Konfiguriert die Daten beim^Starten der Metriken.
	/// </summary>
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			JsonConvert.DefaultSettings = () => new JsonSerializerSettings
			{
				Formatting = Formatting.Indented,
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			};

			Metric.Config
				.WithInternalMetrics()
				.WithReporting(r => r.WithConsoleReport(TimeSpan.FromSeconds(30)))
				.WithOwin(middleware => app.Use(middleware), config => config
					.WithRequestMetricsConfig(c => c.WithAllOwinMetrics(), new[]
					{
						new Regex("(?s).*")
					})
					.WithMetricsEndpoint()
				);

			try
			{
				Metric.Config.WithAllCounters();
			}
			catch (Exception)
			{
				//Ignore
			}
		}
	}
}