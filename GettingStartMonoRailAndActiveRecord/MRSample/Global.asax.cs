using System;

namespace MRSample
{
	using System.Reflection;
	using Castle.ActiveRecord;
	using Castle.ActiveRecord.Framework;
	using Castle.ActiveRecord.Framework.Config;

	public class Global : System.Web.HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			IConfigurationSource config = ActiveRecordSectionHandler.Instance;

			Assembly assm = Assembly.Load("MRSample");
			ActiveRecordStarter.Initialize(assm, config);
		}
	}
}