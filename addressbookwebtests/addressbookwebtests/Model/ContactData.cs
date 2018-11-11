using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        public string Lastname { get; set; }
        public string Firstname { get; set; }

        public string Middlename { get; set; }
        public string Nickname { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }

        private string allPhones;
        private string allEmails;

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    string result = "";
                    if (Email != null && Email != "") { result += Email + "\r\n"; }
                    if (Email2 != null && Email2 != "") { result += Email2 + "\r\n"; }
                    if (Email3 != null && Email3 != "") { result += Email3; }

                    return result.Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }

        //private string title = "";
        //private string company;
        //private string fax;
        //private string homepage;
        //private string bday;
        //private string bmonth;
        //private string byear;
        //private string aday;
        //private string amonth;
        //private string ayear;
        //private GroupData new_group;
        //private string address2;
        //private string phone2;
        //private string notes;

        public ContactData(string lastname, string firstname)
        {
            Lastname = lastname;
            Firstname = firstname;

            Middlename = null;
            Nickname = null;
            Address = null;
            HomePhone = null;
            MobilePhone = null;
            WorkPhone = null;
            Email = null;
            Email2 = null;
            Email3 = null;
        }

        public ContactData()
        {
            Lastname = null;
            Firstname = null;

            Middlename = null;
            Nickname = null;
            Address = null;
            HomePhone = null;
            MobilePhone = null;
            WorkPhone = null;
            Email = null;
            Email2 = null;
            Email3 = null;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return this.Lastname == other.Lastname && this.Firstname == other.Firstname;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return 0;
            }
            return ("" + this.Lastname + " " + this.Firstname).CompareTo("" + other.Lastname + " " + other.Firstname);
        }

        public override int GetHashCode()
        {
            return ("" + this.Lastname + " " + this.Firstname).GetHashCode();
        }

        public override string ToString()
        {
            return ("contact = " + this.Lastname + " " + this.Firstname);
        }
    }
}
