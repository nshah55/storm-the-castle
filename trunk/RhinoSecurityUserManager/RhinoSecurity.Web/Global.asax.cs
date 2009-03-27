namespace Basic.Web
{
	using System;
	using Castle.MonoRail.Framework;
	using Castle.MonoRail.Framework.Configuration;
	using Castle.MonoRail.Framework.Helpers.ValidationStrategy;
	using Castle.MonoRail.Framework.JSGeneration;
	using Castle.MonoRail.Framework.JSGeneration.jQuery;
	using Castle.MonoRail.Framework.Routing;
	using Rhino.Commons.HttpModules;

	public class Global : UnitOfWorkApplication, IMonoRailConfigurationEvents
	{

		public override void Application_Start(object sender, EventArgs e)
		{

			base.Application_Start(sender, e);

			RoutingRules.Register(RoutingModuleEx.Engine);
		
		}

		/// <summary>
		///             Implementors can take a chance to change MonoRail's configuration.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		public void Configure(IMonoRailConfiguration configuration)
		{
			configuration.JSGeneratorConfiguration.AddLibrary("jquery-1.6", typeof(JQueryGenerator))
					.AddExtension(typeof(CommonJSExtension))
					.BrowserValidatorIs(typeof(JQueryValidator))
					.SetAsDefault();
		}
	}
}