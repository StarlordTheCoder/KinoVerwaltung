// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;

namespace CinemaManager.Infrastructure
{
	public class AutoSaver
	{
		private readonly Session session;


		public AutoSaver()
		{
			session = Session.Instance;
			session.Ticker.Start();
		}

		private void Save(object s, EventArgs e)
		{
			session.DataModel.Save();
		}

		public void StartSave()
		{
			session.Ticker.Elapsed += Save;
		}

		public void StopSave()
		{
			session.Ticker.Elapsed -= Save;
		}
	}
}