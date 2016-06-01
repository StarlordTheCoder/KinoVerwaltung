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
			var folder = Path.GetDirectoryName(Session.FullDataPath);
			if (folder != null && !Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}

			using (var stream = File.Open(Session.FullDataPath, FileMode.OpenOrCreate))
			{
				_serializer.Serialize(stream, CinemasModel);
			}
		}

		/// <summary>
		///     Loads the <see cref="CinemasModel"/> from the configured file.
		///     Calls <see cref="Save"/> if the file doesn't exist.
		/// </summary>
		public void Load()
		{
			if (File.Exists(Session.FullDataPath))
			{
				//Read file
				using (var stream = File.OpenRead(Session.FullDataPath))
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