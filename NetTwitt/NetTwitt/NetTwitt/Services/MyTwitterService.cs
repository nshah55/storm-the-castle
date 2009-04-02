using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Castle.ActiveRecord;
using NetTwitt.Model;
using NHibernate.Criterion;

namespace NetTwitt.Services
{
	public class MyTwitterService : ITwitterService
	{
		public void PostUpdate(User user, string message)
		{
			// will trucante message as original twitter
			if (message.Length >= 140)
			{
				message = message.Substring(0, 140);
			}
				
			if (null != user)
			{
				var twitt = new Twitt {Message = message, User = user, Posted = DateTime.Now};
				twitt.SaveAndFlush();
			}
		}

		public IList<Twitt> GetTwittsFromFollowedUsers(User loggedUser, int? maxResults)
		{

			DetachedCriteria usersCriteria = DetachedCriteria.For<User>();

			DetachedCriteria twittsCriteria = DetachedCriteria.For<Twitt>()
				.CreateCriteria("User", "twittUser")
				.Add(Subqueries.Exists(usersCriteria));

			usersCriteria.CreateAlias("Followers", "followers")
				.Add(Restrictions.Eq("followers.Id", loggedUser.Id))
				.SetProjection(Projections.Property("Id"))
				.Add(Property.ForName("Id").EqProperty("twittUser.Id"));

			if (maxResults.HasValue)
			{
				usersCriteria.SetMaxResults(maxResults.Value);
			}
			return ActiveRecordBase<Twitt>.FindAll(twittsCriteria,
				new Order("Posted", false));
		}

		
	}
}
