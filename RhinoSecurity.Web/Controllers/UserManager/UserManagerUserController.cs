namespace RhinoSecurity.Web.Controllers.UserManager
{
	using Castle.MonoRail.ActiveRecordSupport;
	using Castle.MonoRail.Framework;
	using Models;
	using Rhino.Commons;

	[ControllerDetails("user", Area = "security")]
	public class SecurityUserController: AbstractBaseController
	{
		public void home()
		{
			
		}
		[AccessibleThrough(Verb.Post)]
		public void list()
		{
			PropertyBag["users"] = SecurityRepository.FindAllUser();
			RenderView("search");
		}

		[AccessibleThrough(Verb.Post)]
		public void add()
		{
			if(IsAjaxRequest) CancelLayout();

			PropertyBag["u"] = new User();
			RenderView("addoredit");
		}
		
		[AccessibleThrough(Verb.Post)]
		public void edit([ARFetch("id")] User user)
		{
			if (IsAjaxRequest) CancelLayout();

			PropertyBag["u"] = user;
			RenderView("addoredit");
		}
		
		[AccessibleThrough(Verb.Post)]
		public void CreateOrUpdate([ARDataBind("u", Validate = true)] User user)
		{
			if (IsAjaxRequest) CancelLayout();
			Repository<User>.Save(user); 
		}
		
		[AccessibleThrough(Verb.Post)]
		public void Search(string q)
		{
			PropertyBag["users"] = SecurityRepository.FindAllUser(q);
		}

	}
}