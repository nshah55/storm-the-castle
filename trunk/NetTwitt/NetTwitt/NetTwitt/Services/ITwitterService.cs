using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTwitt.Model;

namespace NetTwitt.Services
{
	public interface ITwitterService
	{
		void PostUpdate(User user, string message);

		IList<Twitt> GetTwittsFromFollowedUsers(User loggedUser,int? maxResults );
		
	}
}
