namespace MRSample.Controllers
{
	using Castle.MonoRail.Framework;

	public class HomeController: SmartDispatcherController
	{

		public void Index()
		{
			PropertyBag["variable1"] = "Put me in a bag so the view can poke on me.";

			Flash["variable2"] = "I came from Flash[x]";
		}

	}
}
