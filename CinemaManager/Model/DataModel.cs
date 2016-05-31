// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;
using CinemaManager.Properties;

namespace CinemaManager.Model
{
	public class DataModel : IDataModel
	{
		//XmlSerializer only serializes public properties & fields
		private readonly XmlSerializer _serializer;

		public DataModel()
		{
			_serializer = new XmlSerializer(typeof(CinemasModel));
		}

		/// <summary>
		///    The main instance, containing all data from the models.
		/// </summary>
		public CinemasModel CinemasModel { get; set; } = new CinemasModel();

		/// <summary>
		///     Save the <see cref="CinemasModel"/> to the configured file.
		///     Creates the file, if it doesn't exist.
		/// </summary>
		public void Save()
		{
			var path = Environment.ExpandEnvironmentVariables(Settings.Default.DataPath);

			var folder = Path.GetDirectoryName(path);
			if (folder != null && !Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}

			using (var stream = File.Open(path, FileMode.OpenOrCreate))
			{
				_serializer.Serialize(stream, CinemasModel);
			}
		}

		/// <summary>
		///     Loads the <see cref="CinemasModel"/> from the configured file.
		///     Calls <see cref="Save"/> ff the file doesn't exist.
		/// </summary>
		public void Load()
		{
			var path = Environment.ExpandEnvironmentVariables(Settings.Default.DataPath);

			if (File.Exists(path))
			{
				//Read file
				using (var stream = File.OpenRead(path))
				{
					CinemasModel = (CinemasModel)_serializer.Deserialize(stream);
				}
			}
			else
			{
				//Create new file
				Save();
			}
		}
	}
}