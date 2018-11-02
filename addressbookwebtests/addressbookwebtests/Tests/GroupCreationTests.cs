using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            GroupData group = new GroupData("a");
            group.Header = "d";
            group.Footer = "f";

            List<GroupData> oldGroups = applicationManager.PubGroupHelper.GetGroupList();

            applicationManager.PubGroupHelper.Create(group);
            applicationManager.PubLoginHelper.Wait(100);

            List<GroupData> newGroups = applicationManager.PubGroupHelper.GetGroupList();

            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";

            List<GroupData> oldGroups = applicationManager.PubGroupHelper.GetGroupList();

            applicationManager.PubGroupHelper.Create(group);
            applicationManager.PubGroupHelper.Wait(100);

            List<GroupData> newGroups = applicationManager.PubGroupHelper.GetGroupList();

            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
