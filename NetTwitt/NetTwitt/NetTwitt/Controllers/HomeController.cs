using Castle.ActiveRecord;
using Castle.MonoRail.Framework;
using NetTwitt.Filters;
using NetTwitt.Model;
using NetTwitt.Security;
using NetTwitt.Services;
using NHibernate.Criterion;

namespace NetTwitt.Controllers
{
	[Filter(ExecuteWhen.BeforeAction, typeof(AuthenticationFilter))]
	[Layout("Default"), Rescue("Default")]
	public class HomeController: SmartDispatcherController
	{

		private readonly ITwitterService twitterService;

		public HomeController(ITwitterService twitterService)
		{
			this.twitterService = twitterService;
		}

		public void Index()
		{
			User currentUser = GetLoggedInUser();
			#region Option1
//			OPTION 1 - Without Integration, no service
//			DetachedCriteria usersCriteria = DetachedCriteria.For<User>();
//
//			DetachedCriteria twittsCriteria = DetachedCriteria.For<Twitt>()
//				.CreateCriteria("User", "twittUser")
//				.Add(Subqueries.Exists(usersCriteria));
//
//			usersCriteria.CreateAlias("Followers", "followers")
//				.Add(Restrictions.Eq("followers.Id", currentUser.Id))
//				.SetProjection(Projections.Property("Id"))
//				.Add(Property.ForName("Id").EqProperty("twittUser.Id"));
//
//			PropertyBag["twittsFromFollowedUsers"] = ActiveRecordBase<Twitt>.FindAll(twittsCriteria,
//				new Order("Posted", false));
#endregion

			if (null != currentUser)
			{
				#region option2
				//				OPTION 2 - Without integraton, with Service. 
//				           But tightly coupled to it
//				var myservice = new MyTwitterService();
				//				PropertyBag["twittsFromFollowedUsers"] = myservice.GetTwittsFromFollowedUsers(currentUser, 30);
				#endregion
				#region option3
				//				OPTION 3 - Without Integration, but using Windsor
//						   to provide the service implementation
//						   Better, however this ties the controller to the container
//				var myservice = IoC.Resolve<ITwitterService>();
				//				PropertyBag["twittsFromFollowedUsers"] = myservice.GetTwittsFromFollowedUsers(currentUser, 30);
				#endregion
				// OPTION 4 -  With Windsor Integration :D
				PropertyBag["twittsFromFollowedUsers"] = twitterService.GetTwittsFromFollowedUsers(currentUser, 30);
			}
		}

		[AccessibleThrough(Verb.Post)]
		public void Post(string tweet)
		{
			twitterService.PostUpdate(GetLoggedInUser(), tweet);
			Flash["lastStatus"] = tweet;
			RedirectToAction("Index");
		}

		#region PrivateMethods
		private User GetLoggedInUser()
		{
			var loggedInUser = (LoggedInUser) Session["User"];
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
