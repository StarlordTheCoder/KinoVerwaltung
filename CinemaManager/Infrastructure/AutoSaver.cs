using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManager.Infrastructure
{
	public class AutoSaver
	{
		Session session;

		private void Save(Object s , EventArgs e)
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


		public AutoSaver()
		{
			session = Session.Instance;
			session.Ticker.Start();

		}
	}
}
