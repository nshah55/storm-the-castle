using Castle.MonoRail.Framework;
using NetTwitt.Security;

namespace NetTwitt.Filters
{
	public class AuthenticationFilter : IFilter
	{
		public bool Perform(ExecuteWhen exec, IEngineContext context, IController controller, IControllerContext controllerContext)
		{
			context.CurrentUser = (LoggedInUser) context.Session["User"];
			if (null == context.CurrentUser || !context.CurrentUser.Identity.IsAuthenticated )
			{
				context.Response.Redirect("Login", "Index");
				return false;
			}
			return true;
		}
	}
}
