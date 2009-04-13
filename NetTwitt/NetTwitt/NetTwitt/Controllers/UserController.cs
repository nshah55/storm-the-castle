using System.Web;
using Castle.ActiveRecord;
using Castle.MonoRail.ActiveRecordSupport;
using Castle.MonoRail.Framework;
using NetTwitt.Filters;
using NetTwitt.Helpers;
using NetTwitt.Model;
using NetTwitt.Security;
using NHibernate.Criterion;

namespace NetTwitt.Controllers
{
	[Filter(ExecuteWhen.BeforeAction, typeof(AuthenticationFilter))]
	[Layout("Default"), Rescue("Default")]
	public class UserController : SmartDispatcherController
	{

		public void Search()
		{}

//		[AccessibleThrough(Verb.Post)]
		public void Search(string username)
		{
			PropertyBag["users"] = ActiveRecordBase<User>.FindAll(
										Restrictions.Or(
											Restrictions.Like("Username", username).IgnoreCase(),
											Restrictions.Like("Name", username).IgnoreCase()));
		}

		[AccessibleThrough(Verb.Post)]
		public void Follow([ARFetch("user.Id", false, false)] User userToFollow)
		{
			User user = GetLoggedInUser();
			if (null != userToFollow)
			{
				user.Follows.Add(userToFollow);
				userToFollow.Followers.Add(user);
				user.UpdateAndFlush();
			}
			Redirect("Home", "Index");
		}

		[SkipFilter]
		public void Twitts([ARFetch("Id", false, false)] User user)
		{
			PropertyBag["user"] = user;
		}

		[SkipFilter]
		public void Register()
		{
		}

		[SkipFilter]
		public void Save([DataBind("User")]User user)
		{
			User existingUser = ActiveRecordBase<User>.FindOne(Restrictions.Like("Email", user.Email, MatchMode.Exact).IgnoreCase());
			if (null == existingUser)
			{
				user.SaveAndFlush();
				var userToLog = new LoggedInUser(user.Name, null, user.Id);
				AuthenticationHelper.AddSessionToContext(Context, userToLog);
				Redirect("Home", "Index");
			}	
			else
			{
				Flash["User"] = user;
				Flash["error"] = "OMG! This email has already been used!!.";
				RedirectToAction("Register");
			}
			
		}

		#region PrivateMethods
		private User GetLoggedInUser()
		{
			var loggedInUser = (LoggedInUser)Session["User"];
			User user = null;
			if (null != loggedInUser)
			{
				user = ActiveRecordBase<User>.FindOne(Restrictions.Eq("Id", loggedInUser.UserId));
			}
			return user;
		}
		#endregion
	}
}
