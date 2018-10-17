using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class TestBase
    {
        protected ApplicationManager applicationManager;

        [SetUp]
        public void SetupTest()
        {
            applicationManager = new ApplicationManager();
            applicationManager.PubNavigationHelper.OpenHomePage();
            applicationManager.PubLoginHelper.Login(new AccountData("admin", "secret"));
        }

        [TearDown]
        public void TeardownTest()
        {
            applicationManager.PubLoginHelper.Logout();
            applicationManager.Stop();
        }
    }
}
