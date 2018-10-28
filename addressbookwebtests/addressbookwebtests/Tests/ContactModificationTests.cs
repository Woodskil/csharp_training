using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newcontact = new ContactData("NonHomer", "NonSimpson");

            applicationManager.PubContactHelper.Modify(newcontact);
        }
    }
}
