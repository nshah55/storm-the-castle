using Castle.MonoRail.TestSupport;
using MbUnit.Framework;
using NetTwitt.Controllers;
using NetTwitt.Helpers;
using NetTwitt.Security;

namespace NetTwittTests.Controllers
{
	[TestFixture]
	public class LoginControllerTests : BaseControllerTest
	{
		private LoginController m_loginController;

		[SetUp]
		public void SetUp()
		{
			m_loginController = new LoginController();
			PrepareController(m_loginController);
		}

		[Test]
		public void CheckCanLogOut()
		{
			LoggedInUser user = new LoggedInUser("Test", null, 1);
			AuthenticationHelper.AddSessionToContext(Context, user);
			Assert.AreEqual(Context.CurrentUser, user);
			m_loginController.Logout();
			Assert.AreEqual(Context.CurrentUser, null);
			
		}

	}
}
