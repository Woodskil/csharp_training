using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactSearchTests : ContactTestBase
    {
        [Test]
        public void ContactSearchTest()
        {
            applicationManager.PubContactHelper.GetNumberOfSearchResult();
        }
    }
}
