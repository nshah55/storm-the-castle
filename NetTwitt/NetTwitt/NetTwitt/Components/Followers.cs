using System.Security.Principal;
using Castle.ActiveRecord;
using Castle.MonoRail.Framework;
using NetTwitt.Model;
using NetTwitt.Security;
using NHibernate.Criterion;

namespace NetTwitt.Components
{
	public class Followers: ViewComponent
	{
		public override void Render()
		{
			IPrincipal loggedUser = EngineContext.CurrentUser;
			if ((loggedUser.Identity.AuthenticationType == "Monorail Type") && (loggedUser.Identity.IsAuthenticated))
			{
				LoggedInUser user = (LoggedInUser)Session["User"];
				PropertyBag["loggedUser"] = ActiveRecordBase<User>.FindOne(Restrictions.Eq("Id", user.UserId));
				RenderView("Followers");
			}
			

		}
	}
}
