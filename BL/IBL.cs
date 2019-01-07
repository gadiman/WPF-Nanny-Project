using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
//test
namespace BL
{
    public interface IBL
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
        Child ReturnChildById(int id);

        void Add_Contract(BE.Contract contract);
        void Remove_Contract(uint code);
        void Update_Contract(BE.Contract tmp);
        void Coded_Contracts();
        List<BE.Contract> Get_Contract_List();


        IEnumerable<DateTime> GET_Date_Child(Contract contract);
        IEnumerable<Child> GET_Common_Childs(Contract contract);
        IEnumerable<Nanny> Search_Nanny_For_Mother(Mother mom, Child child = null);
        IEnumerable<Nanny> Find_Top_Nannys(Mother mom);
        IEnumerable<Nanny> Find_Closes_Nannys(Mother mom, Child child);
        IEnumerable<Child> Childs_Without_Nanny();
        IEnumerable<Nanny> Nannys_ITL();
        IEnumerable<Contract> Bool_Contacts_list(Func<Contract, bool> func = null);
        int Num_Bool_Contacts_list(Func<Contract, bool> func = null);
        IEnumerable<IGrouping<int, Nanny>> Grop_Nannys_By_Age(bool order = false, bool by_max_age = false);
        IEnumerable<IGrouping<int, Contract>> Grop_Contract_By_Dictans_Nannys(bool order = false);
        void init_nanny();
        void init_mother();
        void init_child();
        void init_contract();
        
        bool Signed_Contracts(Contract contract);
        bool Force_Contract(Contract contract);
        bool Is_Was_Meeting(Contract contract);
       










    }
}