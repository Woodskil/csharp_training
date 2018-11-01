using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            int GroupNumber = 1;

            while (!applicationManager.PubGroupHelper.ThereIsGroup(GroupNumber))
            {
                applicationManager.PubGroupHelper.Create(new GroupData("test_group"));
            }

            GroupData newData = new GroupData("mewgroup");
            newData.Header = "newheader";
            newData.Footer = "newfooter";

            applicationManager.PubGroupHelper.Modify(GroupNumber, newData);
        }
    }
}
