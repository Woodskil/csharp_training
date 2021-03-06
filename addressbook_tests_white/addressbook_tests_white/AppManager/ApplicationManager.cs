﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;

namespace addressbook_tests_white
{
    public class ApplicationManager
    {
        private GroupHelper groupHelper;
        private ContactHelper contactHelper;

        public GroupHelper Groups { get { return groupHelper; } }
        public ContactHelper Contacts { get { return contactHelper; } }

        public Window MainWindow { get; private set; }

        private static string BaseProgramExePath = @"C:\Tools\AddressBook\AddressBook.exe";
        public static string WINTITLE = "Free Address Book";

        public ApplicationManager()
        {
            Application app = Application.Launch(BaseProgramExePath);
            MainWindow = app.GetWindow(WINTITLE);

            groupHelper = new GroupHelper(this);
            contactHelper = new ContactHelper(this);
        }

        public void Stop()
        {
            MainWindow.Get<Button>("uxExitAddressButton").Click();
        }
    }
}
