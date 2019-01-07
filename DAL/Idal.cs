using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface Idal
    {
        void Add_Nanny(BE.Nanny nanny);
        void Remomve_Nanny(int ID);
        void Update_Ndetails(BE.Nanny tmp);
        List<BE.Nanny> Get_N_list();

        void Add_Mom(BE.Mother mother);
        void Remove_Mom(int ID);
        void Update_Mdetails(BE.Mother tmp);
        List<BE.Mother> Get_M_list();

        void Add_Chaild(BE.Child child);
        void Remove_chaild(int ID);
        void Update_Cdetails(BE.Child tmp);
        List<BE.Child> Get_Child_List();

        void Add_Contract(BE.Contract contract);
        void Remove_Contract(uint code);
        void Update_Contract(BE.Contract tmp);
        void Coded_Contracts(BE.Contract contract);
        List<BE.Contract> Get_Contract_List();
  

    }
}
