namespace MRSample.Controllers
{
	using Castle.MonoRail.Framework;

	public class HomeController: SmartDispatcherController
	{
		public void Index()
		{
			PropertyBag["user"] = "Sokun";
		}
	}
}
