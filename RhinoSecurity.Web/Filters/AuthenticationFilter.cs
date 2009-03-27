namespace Basic.Web.Filters
{
	using Castle.MonoRail.Framework;
	using RhinoSecurity.Models;
	using RhinoSecurity.Web.Common;

	/// <summary>
	/// Apply this filter on the controller that need to be authentication
	///		if the user not login the filter will redirect to login page.
	/// </summary>
	public class AuthenticationFilter : Filter, IFilterAttributeAware
	{
		private AuthenticationFilterAttribute _attribute;

		protected override bool OnBeforeAction(IEngineContext context, IController controller, IControllerContext controllerContext)
		{
			User user = SecurityUtil.GetCurrentUser(context);
			context.CurrentUser = user;

			bool authorized = !_attribute.RequiredAuth;

			if(!authorized)
			{
				if(user == null || !user.IsAuthenticated)
				{
					SecurityUtil.RedirectToLoginAction(context);
				}
				else
				{
					// are u in or out?
					if(_attribute.AllowRoles != null)
					{
						foreach(string role in _attribute.AllowRoles)
						{
							if (!user.IsInRole(role))
							{
								continue;
							}
							authorized = true;
							break;
						}
					}
					else
					{
						authorized = true;
					}
				}
			}
			
			return authorized;
		}

		#region IFilterAttributeAware

		/// <summary>
		/// Sets the filter.
		/// </summary>
		/// <value>
		/// The filter.
		/// </value>
		public FilterAttribute Filter
		{
			set { _attribute = (AuthenticationFilterAttribute) value; }
		}

		#endregion

	}

}
