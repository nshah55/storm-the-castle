namespace MRSample.Controllers
{
	using System;
	using System.IO;
	using Castle.ActiveRecord;
	using Castle.MonoRail.Framework;

	[Rescue("generalerror")]
	public class SetupController: SmartDispatcherController
	{

		public void Index()
		{
		}

		public void Install(bool confirm)
		{
			if (Request.IsLocal && confirm)
			{
				try
				{
					ActiveRecordStarter.CreateSchema();
					Flash["msg"] = "Schema generated.";
				}
				catch (Exception ex)
				{
					throw new Exception("Schema geneneration fail !", ex);
				}
			}
			else
			{
				return;
			}
		}

		public void Uninstall(bool confirm)
		{
			Flash["msg"] = "";
			if (Request.IsLocal && confirm)
			{
				try
				{
					ActiveRecordStarter.DropSchema();

					Flash["msg"] = "Schema dropped.";
				}
				catch (Exception ex)
				{
					throw new Exception("Drop fail !", ex);
				}
			}
			else
			{
				return;
			}
		}
	}
}
