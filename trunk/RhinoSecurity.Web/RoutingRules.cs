namespace Basic.Web
{
	using Castle.MonoRail.Framework.Routing;

	public class RoutingRules
	{

		public static void Register(IRoutingRuleContainer rules)
		{
			rules.Add(new RedirectRoute("", "~/home"));
			rules.Add(new RedirectRoute("/", "~/home"));

			rules.Add(new PatternRoute("<controller>/[action]")
										.DefaultForArea().IsEmpty
										.DefaultForAction().Is("index"));

			rules.Add(new PatternRoute("/security/<controller>/[action]")
				.DefaultForArea().Is("security")
				.DefaultForAction().Is("index"));
		}

	}
}
