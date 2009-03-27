namespace RhinoSecurity.Web.Controllers
{
	using Castle.MonoRail.Framework;

	[Layout("default")]
	[Resource("res", "Basic.Web.Resources.Strings", AssemblyName = "Basic.Web")]
	public abstract class AbstractBaseController: SmartDispatcherController
	{

		/// <summary>
		/// Gets a value indicating whether this instance is an ajax request.
		/// </summary>
		public bool IsAjaxRequest
		{
			get { return Request.Headers["X-Requested-With"] == "XMLHttpRequest"; }
		}

	}
}