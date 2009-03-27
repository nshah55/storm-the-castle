namespace RhinoSecurity.Web.Controllers
{
	using System;
	using System.Web.Security;
	using Basic.Web.Filters;
	using Castle.MonoRail.Framework;
	using Common;
	using Models;

	[AuthenticationFilter(RequiredAuth = false)]
	public class AuthenticationController: SmartDispatcherController
	{

		public void Login()
		{
			User user = SecurityUtil.GetCurrentUser(Context);
			if(user != null)
			{
				// com'on you must be kidding me; do you want to login as another user?
				RenderView("alreadyLogin");
				return;
			}
		}

		public void Authenticate(string username, string password, string returnUrl)
		{
			User user = SecurityRepository.Authenticate(username, password);
			if (user != null)
			{
				Context.CurrentUser = user;
				FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.Name, DateTime.Now, DateTime.Now.AddMinutes(30), true, user.Email, "Rhino.Security");
				string token = FormsAuthentication.Encrypt(ticket);
				Context.Response.CreateCookie("APP_00101", token, DateTime.Now.AddMinutes(80));

				if (!string.IsNullOrEmpty(returnUrl)) Context.Response.RedirectToUrl(returnUrl);
			}
			else
			{
				Flash["error"] = "Login fail!";
				RenderView("login");
			}
		}

		public void Logout()
		{
			Context.Response.RemoveCookie("APP_00101");
		}

	}
}