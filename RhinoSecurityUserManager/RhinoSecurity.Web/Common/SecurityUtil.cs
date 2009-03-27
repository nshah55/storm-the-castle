namespace RhinoSecurity.Web.Common
{
	using System.Collections.Specialized;
	using System.Web.Security;
	using Castle.MonoRail.Framework;
	using Models;

	public static class SecurityUtil
	{

		public static User GetCurrentUser(IEngineContext context)
		{
			string token = context.Request.ReadCookie("APP_00101");
			if (string.IsNullOrEmpty(token))
			{
				return null;
			}
			else
			{
				FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(token);
				return SecurityRepository.GetUserByEmail(ticket.UserData);
			}			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public static void RedirectToLoginAction(IEngineContext context)
		{
			// redirect to login or automatically relogin using cookie.
			NameValueCollection queryStringParameters = new NameValueCollection();
			queryStringParameters.Add("returnUrl", context.Request.Url);
			context.Response.Redirect("authentication", "login", queryStringParameters);
		}

	}
}