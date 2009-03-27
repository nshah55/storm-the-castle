namespace RhinoSecurity.Web.Controllers.UserManager
{
	using System;
	using Basic.Web.Filters;
	using Castle.MonoRail.ActiveRecordSupport;
	using Castle.MonoRail.Framework;
	using Rhino.Commons;
	using Rhino.Security.Interfaces;
	using Rhino.Security.Model;

	[ControllerDetails("group", Area = "security")]
	[AuthenticationFilter("Administrators", RequiredAuth = true)]
	public class UserManagerGroupController: AbstractBaseController
	{
		protected IAuthorizationRepository authorizationRepository;

		public IAuthorizationRepository AuthorizationRepository
		{
			get { return authorizationRepository; }
			set { authorizationRepository = value; }
		}

		public void home()
		{
			
		}
		
		public void Search(string q)
		{
			
		}

		public void list()
		{
			if (IsAjaxRequest) CancelLayout();
			
			PropertyBag["groups"] = Repository<UsersGroup>.FindAll();
			RenderView("search");
		}

		public void add()
		{
		}
		
		public void edit([ARFetch("id")] UsersGroup usersGroup)
		{
			PropertyBag["g"] = usersGroup;
		}

		public void Create(string name)
		{
			try
			{
				authorizationRepository.CreateUsersGroup(name);
				UnitOfWork.Current.TransactionalFlush();				
			}
			catch(Exception ex)
			{
				PropertyBag["error"] = ex.Message;
			}
		}
	}
}