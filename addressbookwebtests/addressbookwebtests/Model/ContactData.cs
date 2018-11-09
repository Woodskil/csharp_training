using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        public string Lastname { get; set; }
        public string Firstname { get; set; }

        public string Middlename { get; set; }
        public string Nickname { get; set; }
        //private string title = "";
        //private string company;
        //private string address;
        //private string home;
        //private string mobile;
        //private string work;
        //private string fax;
        //private string email;
        //private string email2;
        //private string email3;
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
