using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Contract
    {
        public uint Code_Of_Contract { get; set; }
        public int Nanny_ID { get; set; }
        public int Child_ID { get; set; }
        public bool Was_Meeting { get; set; }
        public bool Contarct_Signed { get; set; }
        public double Pay_For_Hour { get; set; }
        public double Pay_for_Month { get; set; }
        public bool Is_PayPer_Hour { get; set; }
        public DateTime Date_Of_Begining { set; get; }
        public DateTime Date_Of_Ending { set; get; }
        public string gropcode { set; get; }



        public Contract()
        {

        }

        public Contract(Contract x)
        {
            Code_Of_Contract = x.Code_Of_Contract;
            Nanny_ID = x.Nanny_ID;
            Child_ID = x.Child_ID;
            Was_Meeting = x.Was_Meeting;
            Contarct_Signed = x.Contarct_Signed;
            Pay_For_Hour = x.Pay_For_Hour;
            Pay_for_Month = x.Pay_for_Month;
            Is_PayPer_Hour = x.Is_PayPer_Hour;
            Date_Of_Begining = x.Date_Of_Begining;
            Date_Of_Ending = x.Date_Of_Ending;
        }

        public override string ToString()
        {
            string C_out = "";
            C_out += "\n" + "Serial of contract:  " + Code_Of_Contract;
            C_out += "\n" + "Nanny id:  " + Nanny_ID;
            C_out += "\n" + "Child id:  " + Child_ID;
            C_out += "\n" + "A meeting was held:  " +Was_Meeting.ToString();
            C_out += "\n" + "The contract was signed:  " + this.Contarct_Signed.ToString();
            if(Is_PayPer_Hour)
                C_out += "\n" + "Hourly payment:  " + Pay_For_Hour;
            else
                C_out += "\n" + "Pay per month:  " + Pay_For_Hour;

            C_out += "\n" + "Date of begining:  " + Date_Of_Begining.ToString();
            C_out += "\n" + "Date of ending:  " + Date_Of_Ending.ToString();
            return C_out;
        }
    }
}


