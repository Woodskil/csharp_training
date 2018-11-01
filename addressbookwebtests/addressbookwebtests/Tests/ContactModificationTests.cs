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
            if (!applicationManager.PubContactHelper.ThereIsContact())
            {
                applicationManager.PubContactHelper.Create(new ContactData("test_first_name", "test_middlename"));
            }

            ContactData newcontact = new ContactData("NonHomer", "NonSimpson");

            applicationManager.PubContactHelper.Modify(newcontact);
        }
    }
}
