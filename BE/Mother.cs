using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BE
{
    public class Mother
    {
        public int ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Phone_Number { get; set; }
        public string Address { get; set; }
        public string Search_Area { get; set; }
        public bool[] Need_Nanny_Today { get; set; } = new bool[6];
        public int[][] Needed_Hours { get; set; } = new int[6][];
        public string Commeents { get; set; }



        public Mother()
        {
            Needed_Hours[0] =new int[2];
            Needed_Hours[1] = new int[2];
            Needed_Hours[2] = new int[2];
            Needed_Hours[3] = new int[2];
            Needed_Hours[4] = new int[2];
            Needed_Hours[5] = new int[2];
        }

        public Mother(Mother x)
        {
            if (x != null)
            {
                ID = x.ID;
                First_Name = x.First_Name;
                Last_Name = x.Last_Name;
                Phone_Number = x.Phone_Number;
                Address = x.Address;
                Search_Area = x.Search_Area;
                Need_Nanny_Today = x.Need_Nanny_Today;
                Needed_Hours = x.Needed_Hours;
                Commeents = x.Commeents;
            }
        }

        public override string ToString()
        {

            string C_out ="";
            C_out += "\n" + "Firs name: " + First_Name;
            C_out += "\n" + "Last name:: " + Last_Name;
            C_out += "\n" + "Id: " + ID;
            C_out += "\n" + "Phone Number" + Phone_Number;
            C_out += "\n" + "Address" +Address;
            C_out += "\n" + "Search area nanny" + Search_Area;
            C_out += "\n" + "Wanted days" + ": [ ";
            foreach (var it in this.Need_Nanny_Today)
            {
                C_out += it.ToString() + ",";
            }
            C_out += ']';

            C_out += "\n" + "Wanted Hours: ";
            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        C_out += "\n" + "Sunday: ";
                        break;
                    case 1:
                        C_out += "\n" + "Monday: ";
                        break;
                    case 2:
                        C_out += "\n" + "Tuesday: ";
                        break;
                    case 3:
                        C_out += "\n" + "Wednesday: ";
                        break;
                    case 4:
                        C_out += "\n" + "Tuesday: ";
                        break;
                    case 5:
                        C_out += "\n" + "Friday: ";
                        break;
                    default:
                        break;
                }
                
                    C_out += "start:" + this.Needed_Hours[i][0]+ " --->  ";
                    C_out +="Finish:" + this.Needed_Hours[i][1];
            }

            C_out += "\n" + "Commeents:  " + Commeents;

            return C_out;
        }

    }
}

