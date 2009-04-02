using System;
using System.Security.Principal;

namespace NetTwitt.Security
{
	[Serializable]
	public class LoggedInUser : IPrincipal
	{
		public IIdentity Identity{ get; private set;}
		public int UserId{ get; set;}
		private string[] roles;

		public LoggedInUser(string name, string[] roles, int userId)
		{
			Identity = new GenericIdentity(name, "Monorail Type");
			this.roles = roles;
			UserId = userId;
		}

		public bool IsInRole(string role)
		{
			return Array.IndexOf(roles, role) >= 0;
		}

		
	}
}