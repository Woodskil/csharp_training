using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : TestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("mewgroup");
            newData.Header = "newheader";
            newData.Footer = "newfooter";

            applicationManager.PubGroupHelper.Modify(1, newData);
        }
    }
}
