using System.Security.Principal;
using Castle.MonoRail.Framework;

namespace NetTwitt.Helpers
{
	public class AuthenticationHelper
	{
		public static void AddSessionToContext(IEngineContext context, IPrincipal loggedInUser)
		{
			context.Session["User"] = loggedInUser;
			context.CurrentUser = loggedInUser;
		}
	}
}
