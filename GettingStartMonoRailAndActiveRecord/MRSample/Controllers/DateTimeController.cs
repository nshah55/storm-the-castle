namespace MRSample.Controllers
{
	using System;
	using Castle.MonoRail.Framework;

	/// <summary>
	/// Demonstrate Html.DateTime() usage
	/// </summary>
	public class DateTimeController: SmartDispatcherController
	{
		public void Index()
		{
			PropertyBag["monthNamesInKhmer"] = "មករា, កុម្ភះ, មីនា, មេសា, ឧសភា, មិថុនា, កក្កដា, សីហា, កញ្ញា, តុលា, វិច្ឆិកា, ធ្នូ";
		}

		public void Send(DateTime dt1, DateTime dt2)
		{
			PropertyBag["date1"] = dt1;
			PropertyBag["date2"] = dt2;
		}
	}
}
