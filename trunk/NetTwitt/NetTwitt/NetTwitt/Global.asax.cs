using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Framework.Config;
using Castle.MicroKernel.Releasers;
using Castle.MonoRail.Framework;
using Castle.MonoRail.WindsorExtension;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

namespace NetTwitt
{
	public class Global : System.Web.HttpApplication, IContainerAccessor
	{
		private static IWindsorContainer m_container;

		protected void Application_Start(object sender, EventArgs e)
		{
			IConfigurationSource config = ActiveRecordSectionHandler.Instance;
			Assembly assm = Assembly.GetExecutingAssembly();
			ActiveRecordStarter.Initialize(assm, config);	
		
			//for windsor integration, adds all from web.config inside the castle section
			m_container = new WindsorContainer(new XmlInterpreter());
			m_container.Kernel.ReleasePolicy = new NoTrackingReleasePolicy();
			m_container.AddFacility<MonoRailFacility>();

			RegisterControllers();
			RegisterViewComponents();
		}

		private void RegisterControllers()
		{
			RegisterAllAssignableFrom<Controller>(Assembly.GetExecutingAssembly(), Container);
		}
		private void RegisterViewComponents()
		{
			RegisterAllAssignableFrom<ViewComponent>(Assembly.GetExecutingAssembly(), Container);
		}

		private static void RegisterAllAssignableFrom<TAssignableFrom>(
			Assembly assembly, IWindsorContainer container)
		{
			foreach (Type t in assembly.GetTypes())
			{
				if (typeof(TAssignableFrom).IsAssignableFrom(t))
				{
					container.AddComponent(t.Name, t);
				}
			}
		}
		protected void Application_BeginRequest(object sender, EventArgs e)
		{
#if DEBUG
			//just for tests
			if (Request["recreatedb"] != null)
			{
				ActiveRecordStarter.GenerateCreationScripts(@"c:\temp\DatabaseGenertationScript");
				ActiveRecordStarter.DropSchema();
				ActiveRecordStarter.CreateSchema();
			}
#endif

		}

		public void Application_OnEnd()
		{
			m_container.Dispose();
		}

		public IWindsorContainer Container
		{
			get { return m_container; }
		}
	}
}