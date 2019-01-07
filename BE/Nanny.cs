using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BE
{
    public class Nanny
    {
        public int ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime Date { get; set; }
        public string Phone_Number { get; set; }
        public string Address { get; set; }
        public bool Is_Elevator { get; set; }
        public int Floor { get; set; }
        public int experience { get; set; }
        public int Max_Childs { get; set; }
        public int Min_Age { get; set; }
        public int Max_Age { get; set; }
        public bool Salary_Per_Hour { get; set; }
        public double Pay_For_Hour { set; get; }
        public double Pay_For_Mount { set; get; }
        public bool[] Does_Nanny_Work { set; get; } = new bool[6];
        public int[][] Work_Hours { set; get; } = new int[6][];
        public bool State_Educations { get; set; }
        public string Recommendations { set; get; }
        public int Number_Of_Childs { set; get; }
        public string keygrop { set; get; }

        public Nanny()
        {
            Work_Hours[0] = new int[2];
            Work_Hours[1] = new int[2];
            Work_Hours[2] = new int[2];
            Work_Hours[3] = new int[2];
            Work_Hours[4] = new int[2];
            Work_Hours[5] = new int[2];
        }

        public Nanny(Nanny x)
        {
            if (x != null)
            {
                ID = x.ID;
                First_Name = x.First_Name;
                Last_Name = x.Last_Name;
                Date = x.Date;
                Phone_Number = x.Phone_Number;
                Address = x.Address;
                Is_Elevator = x.Is_Elevator;
                Floor = x.Floor;
                experience = x.experience;
                Max_Childs = x.Max_Childs;
                Min_Age = x.Min_Age;
                Max_Age = Max_Age;
                Salary_Per_Hour = x.Salary_Per_Hour;
                Pay_For_Hour = x.Pay_For_Hour;
                Pay_For_Mount = x.Pay_For_Mount;
                Does_Nanny_Work = x.Does_Nanny_Work;
                Work_Hours = x.Work_Hours;
                State_Educations = x.State_Educations;
                Recommendations = x.Recommendations;
            }
        }

        public override string ToString()
        {
            string C_out ="";
            C_out += "\n" + "Firs name:  " + First_Name;
            C_out += "\n" + "Last name:  " + Last_Name;
            C_out += "\n" + "Id:  " + ID;
            C_out += "\n" + "Birthday:  " + Date;
            C_out += "\n" + "Phone Number:  " + Phone_Number;
            C_out += "\n" + "Address:  " + Address;
            C_out += "\n" + "There is an elevator:  " + Is_Elevator.ToString();
            C_out += "\n" + "Floor:  " + Floor;
            C_out += "\n" + "Experience:  " + experience;
            C_out += "\n" + "Max Childs:  " + Max_Childs;
            C_out += "\n" + "Min Age:  " + Min_Age;
            C_out += "\n" + "Max Age:  " + Max_Age;
            if (Salary_Per_Hour)
                C_out += "\n" + "Hourly payment:  " + Pay_For_Hour;
            else
                C_out += "\n" + "Pay per month:  " + Pay_For_Mount;

            C_out += "\n" + "Work days" + ":  [ ";
            foreach (var it in Does_Nanny_Work)
            {
                C_out += it.ToString() + ",";
            }
            C_out += ']';

            C_out += "\n" + "Work Hours:  ";
            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        C_out += "\n" + "Sunday:  ";
                        break;
                    case 1:
                        C_out += "\n" + "Monday:  ";
                        break;
                    case 2:
                        C_out += "\n" + "Tuesday:  ";
                        break;
                    case 3:
                        C_out += "\n" + "Wednesday:  ";
                        break;
                    case 4:
                        C_out += "\n" + "Tuesday:  ";
                        break;
                    case 5:
                        C_out += "\n" + "Friday:  ";
                        break;
                }

                C_out += "start:" + Work_Hours[i][0] + " ---> ";
                C_out += "Finish:" + Work_Hours[i][1];
            }

            C_out += "\n" + "State Educations:  " + State_Educations.ToString();
            C_out += "\n" + "Recommendations:  " + Recommendations;

            return C_out;
        }

    }
}
