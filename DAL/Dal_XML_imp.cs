using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DAL
{
    public class Dal_XML_imp:Idal
    {
        const string ChildPath = @"ChildPath_.xml";
        const string NannyPath = @"NannyPath_.xml";
        const string MotherdPath = @"MotherPath_.xml";
        const string ContractPath = @"ContractPath_.xml";
        const string ConfigPath = @"configXml.xml";
        XElement ChildXml, configXml;
        private static uint coded = 10000000;

       public Dal_XML_imp()
        {
            
            OpenFiles();
            DS.DataSource.nanny_list = Xml_To_List<BE.Nanny>(NannyPath);
            DS.DataSource.mother_list = Xml_To_List<BE.Mother>(MotherdPath);
            DS.DataSource.contract_list = Xml_To_List<BE.Contract>(ContractPath);
        
        }

        private void OpenFiles()
        {
            if (!File.Exists(ConfigPath))
            {
                configXml = new XElement("Contract_Code");
                configXml.Save(ConfigPath);
                var code = new XElement("Coded", coded);
                configXml.Add(code);
                configXml.Save(ConfigPath);
            }
            else
            {
                try
                {
                    configXml = XElement.Load(ConfigPath);
                    coded = Convert.ToUInt32(configXml.Element("Coded").Value);
                }
                catch
                {
                    var code = new XElement("Coded", coded);
                    configXml.Add(code);
                    configXml.Save(ConfigPath);
                }
            }

            if (!File.Exists(ChildPath))
            {
                ChildXml = new XElement("ChildRoot");
                ChildXml.Save(ChildPath);
            }
            else
               ChildXml = XElement.Load(ChildPath);

            //if the files dont exist so initialize the files so they can be worked with XmlSerializer
            if (!File.Exists(ContractPath))
            {
                List_To_Xml(new List<BE.Contract>(), ContractPath);
            }


            if (!File.Exists(MotherdPath))
            {
                List_To_Xml(new List<BE.Mother>(), MotherdPath);
            }

            if (!File.Exists(NannyPath))
            {
                List_To_Xml(new List<BE.Nanny>(), NannyPath);
            }
            

        }
       
        #region Get Child/Mother/Nanny/Contract Lists
        public List<BE.Child> Get_Child_List()
        {
        return  (from Child in ChildXml.Elements()
                       select new BE.Child
                       {
                           ID = Convert.ToInt32(Child.Element("Id").Value),
                           Id_Mom = Convert.ToInt32(Child.Element("IdMom").Value),
                           gender = (BE.Gender)Enum.Parse(typeof(BE.Gender), Child.Element("Gender").Value),
                           s = (BE.With_Special_Needs)Enum.Parse(typeof(BE.With_Special_Needs), Child.Element("S").Value),
                           Special_Needs = Child.Element("SpecialNeeds").Value,
                           First_Name = Child.Element("FirstName").Value,
                           Date = DateTime.Parse(Child.Element("Date").Value)
                       }).ToList();

        }

        public List<BE.Contract> Get_Contract_List()
        {
            return DS.DataSource.contract_list;
        }

        public List<BE.Mother> Get_M_list()
        {
            return DS.DataSource.mother_list;
        }

        public List<BE.Nanny> Get_N_list()
        {
            return DS.DataSource.nanny_list;
        }

        private void List_To_Xml<T>(List<T> list, string path)
        {
            try
            {
                XmlSerializer serialize = new XmlSerializer(list.GetType());
                FileStream stream = new FileStream(path, FileMode.Create);
                serialize.Serialize(stream, list);
                stream.Close();
            }
            catch
            {

                throw new Exception("error when trying to receive data from database");
            }
        }

        public List<T> Xml_To_List<T>(string path)
        {
            try
            {
                List<T> list;
                XmlSerializer deSerialize = new XmlSerializer(typeof(List<T>));
                FileStream Stream = new FileStream(path, FileMode.Open);
                list = (List<T>)deSerialize.Deserialize(Stream);

                Stream.Close();

                return list;
            }
            catch
            {

                throw new Exception("error when trying to save data to database");

            }
        }
        #endregion

        #region Add Child/Mother/Nanny/Contract
        public void Add_Chaild(BE.Child child)
        {
            var found_child = (from Child in ChildXml.Elements()
                               where Convert.ToUInt32(Child.Element("Id").Value) == child.ID
                               select Child).ToList();

            if(found_child.Count != 0)
            throw new Exception("Child already exist with same id");


            
            var clone = new XElement("Child", new XElement("Id", child.ID),
                                              new XElement("IdMom", child.Id_Mom),
                                              new XElement("Gender", child.gender),
                                              new XElement("S", child.s),
                                              new XElement("SpecialNeeds", child.Special_Needs),
                                              new XElement("FirstName", child.First_Name),
                                               new XElement("Date", child.Date));
            ChildXml.Add(clone);
            ChildXml.Save(ChildPath);                            
        }

        public void Add_Contract(BE.Contract contract)
        {
           var found_child = (from Child in ChildXml.Elements()
                               where Convert.ToUInt32(Child.Element("Id").Value) == contract.Child_ID
                              select new BE.Child
                              {
                                  ID = Convert.ToInt32(Child.Element("Id").Value),
                                  Id_Mom = Convert.ToInt32(Child.Element("IdMom").Value),
                                  gender = (BE.Gender)Enum.Parse(typeof(BE.Gender), Child.Element("Gender").Value),
                                  s = (BE.With_Special_Needs)Enum.Parse(typeof(BE.With_Special_Needs), Child.Element("S").Value),
                                  Special_Needs = Child.Element("SpecialNeeds").Value,
                                  First_Name = Child.Element("FirstName").Value,
                                  Date = DateTime.Parse(Child.Element("Date").Value)
                              }).FirstOrDefault();





            var found_mom = DS.DataSource.mother_list.Find(who => who.ID == found_child.Id_Mom);
            var fount_nanny = DS.DataSource.nanny_list.Find(who => who.ID == contract.Nanny_ID);

            if (found_mom == null || fount_nanny == null)
                throw new Exception("Mother or Nanny not exsit");

            Coded_Contracts(contract);
            
            configXml.Element("Coded").Value = coded.ToString();
            configXml.Save(ConfigPath);

            DS.DataSource.contract_list.Add(contract.clone());
            List_To_Xml(DS.DataSource.contract_list, ContractPath);
        }
       
        public void Coded_Contracts(BE.Contract contract)
        {
            contract.Code_Of_Contract = coded++;
        }


        public void Add_Mom(BE.Mother mother)
        {
            if (DS.DataSource.mother_list.Exists(who => who.ID == mother.ID))
                throw new Exception("Mother already exist with same ID");

            DS.DataSource.mother_list.Add(mother.Clone_mom());
            List_To_Xml(DS.DataSource.mother_list, MotherdPath);
        }

        public void Add_Nanny(BE.Nanny nanny)
        {
            if (DS.DataSource.nanny_list.Exists(who => who.ID == nanny.ID))
                throw new Exception("Nanny already exist with same Code");

            DS.DataSource.nanny_list.Add(nanny.Clone_nanny());
            List_To_Xml(DS.DataSource.nanny_list, NannyPath);
        }

        #endregion

        #region Remove Child/Mother/Nanny/Contract
        public void Remomve_Nanny(int ID)
        {
            var found = DS.DataSource.nanny_list.Find(who => who.ID == ID);
            if (found == null)
                throw new Exception("Nanny whit this ID not exist");

            DS.DataSource.nanny_list.Remove(found);

            try
            {
                List_To_Xml(DS.DataSource.nanny_list, NannyPath);
            }
            catch
            {
                throw new Exception("Error in deleting Nanny.");
            }
        }

        public void Remove_chaild(int ID)
        {
            var found = from Child in ChildXml.Elements()
                        where Convert.ToUInt32(Child.Element("Id").Value) == ID
                        select Child;
            if (found == null)
                throw new Exception("Child whit this ID not exist");

           

            try
            {
                (from Child in ChildXml.Elements()
                 where Convert.ToUInt32(Child.Element("Id").Value) == ID
                 select Child).FirstOrDefault().Remove();
                ChildXml.Save(ChildPath);
            }
            catch
            {
                throw new Exception("Error in deleting Child");
            }
        }

        public void Remove_Contract(uint code)
        {
            var found = DS.DataSource.contract_list.Find(who => who.Code_Of_Contract == code);
            if (found == null)
                throw new Exception("Contract whit this code not exist");

            DS.DataSource.contract_list.Remove(found);
            try
            {
                List_To_Xml(DS.DataSource.contract_list, ContractPath);
            }
            catch
            {
                throw new Exception("Error in deleting contract.");
            }

        }


        public void Remove_Mom(int ID)
        {
            var found = DS.DataSource.mother_list.Find(who => who.ID == ID);
            if (found == null)
                throw new Exception("Mother whit this ID not exist");

            DS.DataSource.mother_list.Remove(found);
            try
            {
                List_To_Xml(DS.DataSource.mother_list, MotherdPath);
            }
            catch
            {
                throw new Exception("Error in deleting Mother");
            }

        }
        #endregion

        #region Update details Child/Mother/Nanny/Contract
        public void Update_Cdetails(BE.Child tmp)
        {
            XElement updateChild = (from Child in ChildXml.Elements()
                                        where Convert.ToUInt32(Child.Element("Id").Value) == tmp.ID
                                        select Child).FirstOrDefault();

            if (updateChild == null)
                throw new Exception("Child not exsit in the List");

            updateChild.Element("Id").Value = tmp.ID.ToString();
            updateChild.Element("IdMom").Value = tmp.Id_Mom.ToString();
            updateChild.Element("Gender").Value = tmp.gender.ToString();
            updateChild.Element("S").Value = tmp.s.ToString();
            updateChild.Element("SpecialNeeds").Value = tmp.Special_Needs;
            updateChild.Element("FirstName").Value = tmp.First_Name;
            updateChild.Element("Date").Value = tmp.Date.ToString();

            ChildXml.Save(ChildPath);
        }

        public void Update_Contract(BE.Contract tmp)
        {

            var found = DS.DataSource.contract_list.Find(who => who.Code_Of_Contract == tmp.Code_Of_Contract);
            if (found == null)
                throw new Exception("Contract not exsit in the List");

            DS.DataSource.contract_list.Remove(found);
            DS.DataSource.contract_list.Add(tmp.clone());
            List_To_Xml(DS.DataSource.contract_list, ContractPath);
        }

        public void Update_Mdetails(BE.Mother tmp)
        {
            var found = DS.DataSource.mother_list.Find(who => who.ID == tmp.ID);
            if (found == null)
                throw new Exception("Mother not exsit in the List");

            DS.DataSource.mother_list.Remove(found);
            DS.DataSource.mother_list.Add(tmp.clone());
            List_To_Xml(DS.DataSource.mother_list, MotherdPath);
        }

        public void Update_Ndetails(BE.Nanny tmp)
        {
            var found = DS.DataSource.nanny_list.Find(who => who.ID == tmp.ID);
            if (found == null)
                throw new Exception("Nanny not exsit in the List");

            DS.DataSource.nanny_list.Remove(found);
            DS.DataSource.nanny_list.Add(tmp.clone());
            List_To_Xml(DS.DataSource.nanny_list, NannyPath);
        }

        #endregion


    }


}
