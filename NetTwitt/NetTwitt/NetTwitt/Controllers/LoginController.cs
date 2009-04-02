using Castle.ActiveRecord;
using Castle.MonoRail.Framework;
using NetTwitt.Helpers;
using NetTwitt.Model;
using NetTwitt.Security;
using NHibernate.Criterion;

namespace NetTwitt.Controllers
{
	public class LoginController: SmartDispatcherController
	{
		public void Index()
		{
			
		}

		[AccessibleThrough(Verb.Post)]
		public void Login(string username, string password)
		{
			//obviosuly this is not production code.
			User user = ActiveRecordBase<User>.FindOne(Restrictions.And(
				Restrictions.Eq("Username", username), 
				Restrictions.Eq("Password", password))
				);

			if (null != user)
			{
				var loggedInUser = new LoggedInUser(user.Name, null, user.Id);
				AuthenticationHelper.AddSessionToContext(Context , loggedInUser);
				Redirect("Home","Index");

			}
			else
			{
				Flash["error"] = string.Format("Are you sure {0} is your username and {1} is your password", 
					username, password);
				RedirectToAction("Index");

			}

		}

		public void Logout()
		{
			Context.Session["User"] = null;
			Context.CurrentUser = null;
			Redirect("Home", "Index");
		}
	}
}
