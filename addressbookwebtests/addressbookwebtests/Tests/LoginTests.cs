using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidCredentials()
        {   
            applicationManager.PubLoginHelper.Logout();

            AccountData account = new AccountData("admin", "secret");
            applicationManager.PubLoginHelper.Login(account);
                
            Assert.IsTrue(applicationManager.PubLoginHelper.IsLoggedIn(account));
        }

        [Test]
        public void LoginWithInvalidCredentials()
        {
            applicationManager.PubLoginHelper.Logout();
            applicationManager.PubLoginHelper.Wait(200);

            AccountData account = new AccountData("admin", "1234");
            applicationManager.PubLoginHelper.Login(account);

            Assert.IsFalse(applicationManager.PubLoginHelper.IsLoggedIn(account));
        }
    }
}
