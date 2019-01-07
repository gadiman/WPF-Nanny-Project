//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks; 
//using BE;

//namespace DAL
//{
//    public class Dal_XML_imp : Idal
//    {
//        #region Add Child/Mother/Nanny/Contract

//        public void Add_Chaild(Child child)
//        {
//            if (DS.DataSource.child_list.Exists(who => who.ID == child.ID))
//                throw new Exception("Child already exist with same id");

//            DS.DataSource.child_list.Add(child.clone());
//        }

//        public void Add_Contract(Contract contract)
//        {
            

//           var found_child = DS.DataSource.child_list.Find(who => who.ID==contract.Child_ID);
//           var found_mom= DS.DataSource.mother_list.Find(who => who.ID == found_child.Id_Mom);
//            var fount_nanny = DS.DataSource.nanny_list.Find(who => who.ID == contract.Nanny_ID);

//            if (found_mom == null || fount_nanny == null)
//                throw new Exception("Mother or Nanny not exsit");

//            Coded_Contracts(contract);

//            DS.DataSource.contract_list.Add(contract.clone());
//        }

//        public void Add_Mom(Mother mother)
//        {
//            if (DS.DataSource.mother_list.Exists(who => who.ID == mother.ID))
//                throw new Exception("Mother already exist with same Code");

//            DS.DataSource.mother_list.Add(mother.Clone_mom());
//        }

//        public void Add_Nanny(Nanny nanny)
//        {
//            if (DS.DataSource.nanny_list.Exists(who => who.ID == nanny.ID))
//                throw new Exception("Nanny already exist with same Code");

//            DS.DataSource.nanny_list.Add(nanny.Clone_nanny());
//        }
//        #endregion

//        #region Remove Child/Mother/Nanny/Contract
//        public void Remomve_Nanny(int ID)
//        {
//            var found = DS.DataSource.nanny_list.Find(who => who.ID == ID);
//            if (found == null)
//                throw new Exception("Nanny whit this ID not exist");
           
//                DS.DataSource.nanny_list.Remove(found);
//        }

//        public void Remove_chaild(int ID)
//        {
//            var found = DS.DataSource.child_list.Find(who => who.ID == ID);
//            if (found == null)
//                throw new Exception("Child whit this ID not exist");
            
//                DS.DataSource.child_list.Remove(found);
//        }

//        public void Remove_Contract(uint code)
//        {
//            var found = DS.DataSource.contract_list.Find(who => who.Code_Of_Contract == code);
//            if (found == null)
//                throw new Exception("Child whit this ID not exist");
            
//                DS.DataSource.contract_list.Remove(found);
//        }


//        public void Remove_Mom(int ID)
//        {
//            var found = DS.DataSource.mother_list.Find(who => who.ID == ID);
//            if (found == null)
//                throw new Exception("Child whit this ID not exist");
            
//                DS.DataSource.mother_list.Remove(found);
//        }
//        #endregion

//        #region Update details Child/Mother/Nanny/Contract
//        public void Update_Cdetails(Child tmp)
//        {
//            var found = DS.DataSource.child_list.Find(who => who.ID == tmp.ID);
//            if (found == null)
//                throw new Exception("Child not exsit in the List");

//            DS.DataSource.child_list.Remove(found);
//            DS.DataSource.child_list.Add(tmp.clone());
//        }

//        public void Update_Contract(Contract tmp)
//        {
//            var found = DS.DataSource.contract_list.Find(who => who.Code_Of_Contract == tmp.Code_Of_Contract);
//            if (found == null)
//                throw new Exception("Contract not exsit in the List");

//            DS.DataSource.contract_list.Remove(found);
//            DS.DataSource.contract_list.Add(tmp.clone());
//        }

//        public void Update_Mdetails(Mother tmp)
//        {
//            var found = DS.DataSource.mother_list.Find(who => who.ID == tmp.ID);
//            if (found == null)
//                throw new Exception("Mother not exsit in the List");

//            DS.DataSource.mother_list.Remove(found);
//            DS.DataSource.mother_list.Add(tmp.clone());
//        }

//        public void Update_Ndetails(Nanny tmp)
//        {
//            var found = DS.DataSource.nanny_list.Find(who => who.ID == tmp.ID);
//            if (found == null)
//                throw new Exception("Nanny not exsit in the List");

//            DS.DataSource.nanny_list.Remove(found);
//            DS.DataSource.nanny_list.Add(tmp.clone());
//        }

//        #endregion

//        #region Get Child/Mother/Nanny/Contract Lists
//        public List<Child> Get_Child_List()
//        {
//            return DS.DataSource.child_list;
//        }

//        public List<Contract> Get_Contract_List()
//        {
//            return DS.DataSource.contract_list;
//        }

//        public List<Mother> Get_M_list()
//        {
//            return DS.DataSource.mother_list;
//        }

//        public List<Nanny> Get_N_list()
//        {
//            return DS.DataSource.nanny_list;
//        }
//        #endregion


//        private static uint coded = 0;
//        public void Coded_Contracts(Contract contract)
//        {
//            contract.Code_Of_Contract = coded++;
//        }

      
//    }
//}