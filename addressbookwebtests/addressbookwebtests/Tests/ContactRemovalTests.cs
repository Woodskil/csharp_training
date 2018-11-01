using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            if (!applicationManager.PubContactHelper.ThereIsContact())
            {
                applicationManager.PubContactHelper.Create(new ContactData("test_first_name", "test_middlename"));
            }

            applicationManager.PubContactHelper.Remove();
        }
    }
}
