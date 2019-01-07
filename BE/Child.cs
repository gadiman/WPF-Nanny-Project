using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BE
{
    public class Child
    {
        public int ID { get; set; }
        public int Id_Mom { get; set; }
        public string First_Name { get; set; }
        public DateTime Date { get; set; }
        public With_Special_Needs s { get; set; }
        public string Special_Needs { get; set; }
        public Gender gender { get; set; }
        

        public Child()
        {

        }

        public Child(Child x)
        {
            ID = x.ID;
            Id_Mom = x.Id_Mom;
            First_Name = x.First_Name;
            gender = x.gender;
            Date = x.Date;
            s = x.s;
            Special_Needs = x.Special_Needs;
        }

        public override string ToString()
        {
            string C_out="";
            C_out += "\n" + "First name:  " + First_Name;
            C_out += "\n" + "Gender:  " + this.gender.ToString();
            C_out += "\n" + "ID:  " + ID;
            C_out += "\n" + "Mother id:  " + Id_Mom;
            C_out += "\n" + "Date of birth:  " + this.Date.ToString();
            C_out += "\n" + "Special needs:  " + this.s.ToString();
            if(s.ToString() == "NEED")
                C_out += "\n" + "The special needs:  " + Special_Needs;

            return C_out;
        }
    }
}

