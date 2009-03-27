namespace RhinoSecurity.Web.Controllers.UserManager
{
	using Castle.MonoRail.Framework;
	using Rhino.Commons;
	using Rhino.Security.Interfaces;
	using Rhino.Security.Model;

	[ControllerDetails("operations", Area = "security")]
	public class SecurityOperationController: AbstractBaseController
	{
		protected IAuthorizationRepository authorizationRepository;

		public IAuthorizationRepository AuthorizationRepository
		{
			get { return authorizationRepository; }
			set { authorizationRepository = value; }
		}

		public void Home()
		{
			PropertyBag["oprations"] = Repository<Operation>.FindAll();
		}

		public void Create(string name)
		{
			AuthorizationRepository.CreateOperation(name);
			UnitOfWork.CurrentSession.Flush();
		}
	}
}