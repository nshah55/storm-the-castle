namespace Basic.Web.Filters
{
	using Castle.MonoRail.Framework;

	public class AuthenticationFilterAttribute: FilterAttribute
	{
		private string[] allowRoles;
		private bool requiredAuth;

		public AuthenticationFilterAttribute(params string[] allowRoles) : 
			base(ExecuteWhen.BeforeAction, typeof(AuthenticationFilter))
		{
			this.allowRoles = allowRoles;
		}

		public string[] AllowRoles
		{
			get { return allowRoles; }
		}

		public bool RequiredAuth
		{
			get { return requiredAuth; }
			set { requiredAuth = value; }
		}

	}
}
