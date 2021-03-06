﻿using System;
using System.Collections.Generic;
using NUnit.Framework;


namespace addressbook_tests_white
{
    [TestFixture]
    public class GroupRemoveTests : TestBase
    {
        [Test]
        public void GroupRemoveTest()
        {
            int indexOfRemovedGroup = 1;
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            while (oldGroups.Count <= indexOfRemovedGroup)
            {
                app.Groups.AddGroup(new GroupData("NewTestGroup"));
                oldGroups = app.Groups.GetGroupList();
            }

            GroupData TobeRemoved = oldGroups[indexOfRemovedGroup];

            app.Groups.RemoveFroup(TobeRemoved);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Remove(TobeRemoved);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

        }
    }
}