using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
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

            List<GroupData> oldGroups = applicationManager.PubGroupHelper.GetGroupList();

            GroupData newData = new GroupData("mewgroup");
            newData.Header = "newheader";
            newData.Footer = "newfooter";

            applicationManager.PubGroupHelper.Modify(GroupNumber, newData);
            applicationManager.PubGroupHelper.Wait(100);

            List<GroupData> newGroups = applicationManager.PubGroupHelper.GetGroupList();

            oldGroups[GroupNumber - 1] = newData;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
