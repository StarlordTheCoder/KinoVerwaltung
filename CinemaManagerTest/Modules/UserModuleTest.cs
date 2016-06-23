// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using CinemaManager.Model;
using CinemaManager.Modules.User;

namespace CinemaManagerTest.Modules
{
	public class UserModuleTest : UnitTestBase<UserModule>
	{
		protected override void DoSetup()
		{
			base.DoSetup();
			UnitUnderTest = new UserModule()
			{
				UserFilterConfigurator = CreateTrueFilterConfigurator<UserModel>()
			};
		}


	}
}