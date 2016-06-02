// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.IO;
using System.Xml.Serialization;

namespace CinemaManager.Model
{
	public class DataModel : IDataModel
	{
		//XmlSerializer only serializes public properties & fields
		private readonly XmlSerializer _serializer;

		private static Session Session => Session.Instance;

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
			var folder = Path.GetDirectoryName(Session.DataPath);
			if (folder != null && !Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}

			using (var stream = File.Open(Session.DataPath, FileMode.OpenOrCreate))
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
			if (File.Exists(Session.DataPath))
			{
				//Read file
				using (var stream = File.OpenRead(Session.DataPath))
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