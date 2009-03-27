namespace RhinoSecurity.Web.Controllers
{
	using System;
	using System.IO;
	using Castle.ActiveRecord;
	using Castle.MonoRail.Framework;
	using Models;
	using Rhino.Commons;
	using Rhino.Security.Interfaces;
	using Rhino.Security.Model;

	[Layout("blank"), Rescue("generalerror")]
	public class SetupController: SmartDispatcherController
	{

		public void Index()
		{
		}

		public void Install(bool confirm)
		{
			if (Request.IsLocal && confirm)
			{
				try
				{
					ActiveRecordStarter.CreateSchema();

					string baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "static" + Path.DirectorySeparatorChar + "sql");
					//ActiveRecordStarter.CreateSchemaFromFile(Path.Combine(baseDirectory, "Sitemap.sql"));

					//todo: define default user, group and permission
					IAuthorizationRepository authorizationRepository = IoC.Resolve<IAuthorizationRepository>();
					UsersGroup group = authorizationRepository.CreateUsersGroup("Administrators");

					User defaultUser = new User("Administrator", "admin@localhost", "admin");
					SecurityRepository.CreateUser(defaultUser);

					authorizationRepository.AssociateUserWith(defaultUser, "Administrators");
					Operation operation = authorizationRepository.CreateOperation("/usermanager/home/index");

					IPermissionsBuilderService permissionBuilderService = IoC.Resolve<IPermissionsBuilderService>();
					permissionBuilderService
						.Allow(operation)
						.For(group)
						.OnEverything()
						.DefaultLevel()
						.Save();

					UnitOfWork.Current.TransactionalFlush();

					RenderView("index");
				}
				catch (Exception ex)
				{
					throw new Exception("Schema geneneration fail !", ex);
				}
			}
			else
			{
				return;
			}
		}

		public void Uninstall(bool confirm)
		{
			Flash["msg"] = "";
			if (Request.IsLocal && confirm)
			{
				try
				{
					ActiveRecordStarter.DropSchema();
					string baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "static" + Path.DirectorySeparatorChar + "sql");
					
					//ActiveRecordStarter.CreateSchemaFromFile(Path.Combine(baseDirectory, "drop_view.sql"));

					Flash["msg"] = "Completed";

					RenderView("index");
				}
				catch (Exception ex)
				{
					throw new Exception("Drop fail !", ex);
				}
			}
			else
			{
				return;
			}
		}
	}
}