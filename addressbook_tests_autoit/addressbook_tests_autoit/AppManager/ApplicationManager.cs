using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoItX3Lib;

namespace addressbook_tests_autoit
{
    public class ApplicationManager
    {
        private GroupHelper groupHelper;
        public GroupHelper Groups { get { return groupHelper; } }

        private AutoItX3 aux;
        public AutoItX3 Aux { get { return aux; } }

        private static string BaseProgramExePath = @"C:\Tools\AddressBook\AddressBook.exe";
        public static string WINTITLE = "Free Address Book";

        public ApplicationManager()
        {
            aux = new AutoItX3();
            aux.Run(BaseProgramExePath);
            aux.WinWait(WINTITLE);
            aux.WinActivate(WINTITLE);
            aux.WinWaitActive(WINTITLE);

            groupHelper = new GroupHelper(this); 
        }

        public void Stop()
        {
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.1114f8110");
        }


    }
}
