using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using BE;
using DAL;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using GoogleMapsApi.Entities.PlaceAutocomplete.Request;

namespace BL
{

    class MyBL : IBL
    {
        Idal dal = FactoryDal.GetDal();
        private static string API_KEY = "AIzaSyC4jBZmIQI7g8Qzzd8P-t9X0lLyjn73KZ8";

        public MyBL()
        {
            //--------check add functions and fill the lists
            //fill each list at 3 instances.

            //init_nanny();
            //init_mother();
            //init_child();
            // init_contract();

        }

      
        

        public delegate bool Delegate_Contract_List(Contract contract);

        #region Call to DS->Dal_imp.cs Add functions

        /// <summary>
        /// add child
        /// </summary>
        /// <param name="child"></param>
        public void Add_Chaild(Child child)
        {
            dal.Add_Chaild(child);
        }
        /// <summary>
        /// add contract
        /// </summary>
        /// <param name="contract"></param>
        public void Add_Contract(Contract contract)
        {
            //who nanny have a place for another childs?
            var found_Nanny = Get_N_list().Find(Who => Who.ID == contract.Nanny_ID);
            if (found_Nanny != null)
                if (found_Nanny.Number_Of_Childs >= found_Nanny.Max_Childs)
                    throw new Exception("Ther is not place to another child");

            //Who child is less than 3 months old?
            DateTime f = new DateTime();
            IEnumerable<DateTime> c = GET_Date_Child(contract);
            IEnumerator enumerator = c.GetEnumerator();
            while (enumerator.MoveNext())
            {
                f = (DateTime)enumerator.Current;
                break;
            }


            //check if the child is less then 3 months
            var today = DateTime.Now;
            var today_date = (today.Year * 100 + today.Month) * 100 + today.Day;
            var birth_date = (f.Year * 100 + f.Date.Month) * 100 + f.Date.Day;
            if (!(((today_date - birth_date) / 100) % 100 >= 03 ))
                if(f.Year == today.Year)
                throw new Exception(" It is not possible to sign an contract for a child who is less than 3 months old ");

            //salary of nanny
            if (found_Nanny != null)
            {
                if (found_Nanny.Salary_Per_Hour)
                    contract.Pay_for_Month = salary(contract, found_Nanny);
                else
                    contract.Pay_for_Month = salary(contract, found_Nanny);
            }
            else
            {
                throw new Exception("worng id of nanny");
            }
            //count the number of each nanny
            found_Nanny.Number_Of_Childs++;
            dal.Update_Ndetails(found_Nanny);
            dal.Add_Contract(contract);
        }
        /// <summary>
        /// add mother
        /// </summary>
        /// <param name="mother"></param>
        public void Add_Mom(Mother mother)
        {
            dal.Add_Mom(mother);
        }
        /// <summary>
        /// add nanny
        /// </summary>
        /// <param name="nanny"></param>
        public void Add_Nanny(Nanny nanny)
        {
            //check if nanny too young
            var today = DateTime.Now;
            var today_date = (today.Year * 100 + today.Month) * 100 + today.Day;
            var birth_date = (nanny.Date.Year * 100 + nanny.Date.Month) * 100 + nanny.Date.Day;
            if (!((today_date - birth_date) / 10000 > 18))
                throw new Exception(" The nanny is too young ( < 18) ");
            dal.Add_Nanny(nanny);
        }
        #endregion

        #region  Call to DS->Dal_imp.cs GET functions
        public List<Child> Get_Child_List()
        {
            List<Child> child_list = dal.Get_Child_List();
            return child_list;
        }

        public List<Contract> Get_Contract_List()
        {
            List<Contract> contract_list = dal.Get_Contract_List();
            return contract_list;
        }

        public List<Mother> Get_M_list()
        {
            List<Mother> mother_list = dal.Get_M_list();
            return mother_list;
        }

        public List<Nanny> Get_N_list()
        {
            List<Nanny> nanny_list = dal.Get_N_list();
            return nanny_list;
        }

        #endregion

        #region  Call to DS->Dal_imp.cs Remove functions
        /// <summary>
        /// remove nanny
        /// </summary>
        /// <param name="ID"></param>
        public void Remomve_Nanny(int ID)
        {
            var contract_with_nanny = dal.Get_Contract_List().Find(who => who.Nanny_ID == ID);
            if (contract_with_nanny != null)
                throw new Exception("can not remove this nanny , remove the contract first");
            dal.Remomve_Nanny(ID);
        }
        /// <summary>
        /// remove child
        /// </summary>
        /// <param name="ID"></param>
        public void Remove_chaild(int ID)
        {
            var contract_with_child = dal.Get_Contract_List().Find(who => who.Child_ID == ID);
            if (contract_with_child != null)
                throw new Exception("can not remove this child , remove the contract first");
            dal.Remove_chaild(ID);
        }

        public void Remove_Contract(uint code)
        {
            dal.Remove_Contract(code);
        }
        /// <summary>
        /// remove mother
        /// </summary>
        /// <param name="ID"></param>
        public void Remove_Mom(int ID)
        {
            //check if her child under contract
            var found_mom = dal.Get_M_list().Find(who => who.ID == ID);
            var found_child = dal.Get_Child_List().Find(who => who.Id_Mom == ID);
            if (found_child != null)
            {
                var contract_with_child = dal.Get_Contract_List().Find(who => who.Child_ID == found_child.ID);
                if (contract_with_child != null)
                    throw new Exception("can not remove this mother ./nfirst, remove the contract with her child ");
            }
            try
            {
                dal.Remove_Mom(ID);
                MessageBox.Show("removing also the childs of this mother");
                IEnumerable<Child> Common_Childs = from item in dal.Get_Child_List()
                                                   where item.Id_Mom == ID
                                                   select item;
                foreach (var item in Common_Childs)
                {

                    dal.Remove_chaild(item.ID);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion

        #region  Call to DS->Dal_imp.cs Update functions

        public void Update_Cdetails(Child tmp)
        {
            dal.Update_Cdetails(tmp);
        }

        public void Update_Contract(Contract tmp)
        {
            var found_Nanny = Get_N_list().Find(Who => Who.ID == tmp.Nanny_ID);
            if (found_Nanny.Salary_Per_Hour)
                tmp.Pay_For_Hour = salary(tmp, found_Nanny);
            else
                tmp.Pay_for_Month = salary(tmp, found_Nanny);
            dal.Update_Contract(tmp);
        }

        public void Update_Mdetails(Mother tmp)
        {
            dal.Update_Mdetails(tmp);
        }

        public void Update_Ndetails(Nanny tmp)
        {
            dal.Update_Ndetails(tmp);
        }
        #endregion

        public void Coded_Contracts() {/*do nothing-do at in Dal_imp */}

        #region BL functions
        /// <summary>
        /// return list of birthdays of the childrens
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public IEnumerable<DateTime> GET_Date_Child(Contract contract)
        {
            var array = from item in dal.Get_Child_List()
                        where item.ID == contract.Child_ID
                        select item.Date;
            return array;
        }
        /// <summary>
        /// return list of Common_Childs of the same mom
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public IEnumerable<Child> GET_Common_Childs(Contract contract)
        {
            var FoundChild = Get_Child_List().Find(Who => Who.ID == contract.Child_ID);
            var FoundMom = Get_M_list().Find(Who => Who.ID == FoundChild.Id_Mom);
            IEnumerable<Child> Common_Childs = from item in dal.Get_Child_List()
                                               where item.Id_Mom == FoundMom.ID
                                               select item;
            return Common_Childs;
        }
        /// <summary>
        /// get the salary of evry nanny
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="nanny"></param>
        /// <returns></returns>
        public double salary(Contract contract, Nanny nanny)
        {
            double salary;
            double count;
            var FoundChild = Get_Child_List().Find(Who => Who.ID == contract.Child_ID);
            var FoundMom = Get_M_list().Find(Who => Who.ID == FoundChild.Id_Mom);

            if (nanny.Salary_Per_Hour == true) //Case: Salary Per Hour 
            {
                count = 0;
                for (int i = 0; i < 6; i++)
                    for (int j = 0; j < 2; j++)
                        count += FoundMom.Needed_Hours[i][j];

                salary = count * nanny.Pay_For_Hour;

                for (int i = 0; i < GET_Common_Childs(contract).Count<Child>(); i++)
                    salary *= 0.98;
            }
            else  // Case: Salary Per Month
            {
                salary = nanny.Pay_For_Mount;
                for (int i = 0; i < GET_Common_Childs(contract).Count<Child>(); i++)
                    salary *= 0.98;
            }

            return salary;
        }

        /// <summary>
        /// print list of nannys by request of mothers, foreach mother
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public int CalculateDistance(string source, string dest)
        {
            var drivingDirectionRequest = new DirectionsRequest
            {
                TravelMode = TravelMode.Walking,
                Origin = source,
                Destination = dest,
            };
            DirectionsResponse drivingDirections = GoogleMaps.Directions.Query(drivingDirectionRequest);
            Route route = drivingDirections.Routes.First();
            Leg leg = route.Legs.First();
            return leg.Distance.Value;
        }

        public IEnumerable<Nanny> Search_Nanny_For_Mother(Mother mom, Child child = null)
        {
            var foundnanny = from item in dal.Get_N_list()
                             where Equals_times(mom, item)
                             select item;



            if (foundnanny.Count() == 0)
                foundnanny = Find_Top_Nannys(mom);

            if (child != null)
            {
                foundnanny = Cheack_Age_Of_Child(child, foundnanny);
            }

            return foundnanny;
        }
        /// <summary>
        /// check if age of child suitable to nanny
        /// </summary>
        /// <param name="child"></param>
        /// <param name="foundnanny"></param>
        /// <returns></returns>
        private IEnumerable<Nanny> Cheack_Age_Of_Child(Child child, IEnumerable<Nanny> foundnanny)
        {
            var nannys = from item in foundnanny
                         where is_age_is_suitable(child, item)
                         select item;

            return nannys;
        }

        private bool is_age_is_suitable(Child child, Nanny item)
        {
            var f = new DateTime();
            f = child.Date;

            //chedck if min age<=child<=max age
            var today = DateTime.Now;
            var today_date = (today.Year * 100 + today.Month) * 100 + today.Day;
            var birth_date = (f.Year * 100 + f.Date.Month) * 100 + f.Date.Day;

            if ((((today_date - birth_date) / 100) % 100 > item.Max_Age))
                return false;
            if ((((today_date - birth_date) / 100) % 100 < item.Min_Age))
                return false;

            return true;


        }



        /// <summary>
        /// function called by Search_Nanny_For_Mother function
        //return list of nannys that Which are suitable for mothers
        /// </summary>
        /// <param name="mom"></param>
        /// <param name="nanny"></param>
        /// <returns></returns>
        public bool Equals_times(Mother mom, Nanny nanny) //found nannay for mother by requirements
        {
            if (mom == null || nanny == null)
                return false;

            //check if the days of nanny and moder are equal
            for (int i = 0; i < 6; i++)
                if (mom.Need_Nanny_Today[i] != nanny.Does_Nanny_Work[i])
                    if (nanny.Does_Nanny_Work[i] == false)
                        return false;


            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (j == 0)
                    {
                        if (mom.Needed_Hours[i][j] < nanny.Work_Hours[i][j]) // start timework
                        {
                            return false;
                        }
                    }
                    else if (j == 1)//ending timework
                    {
                        if (mom.Needed_Hours[i][j] > nanny.Work_Hours[i][j])
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        ///founction that returns list of nannys at size 5 ,who better for mom  
        //in case that no match at all
        /// </summary>
        /// <param name="mom"></param>
        /// <returns></returns>
        public IEnumerable<Nanny> Find_Top_Nannys(Mother mom)
        {


            int[,] hours = new int[Get_N_list().Count<Nanny>(), 2];
            int count = 0;//number of hours thet the nanny can work whit a child
            int row = 0;
            var nannies = Get_N_list();
            foreach (var item in nannies)
            {
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        //mom need and nnay can not
                        if (mom.Need_Nanny_Today[i] != item.Does_Nanny_Work[i] && item.Does_Nanny_Work[i] == false)
                        {
                            count += mom.Needed_Hours[i][1] - mom.Needed_Hours[i][0];
                            break;
                        }

                        if (j == 0)
                        {
                            if (mom.Needed_Hours[i][j] < item.Work_Hours[i][j])
                                count += item.Work_Hours[i][j] - mom.Needed_Hours[i][j];
                        }
                        else
                        {
                            if (mom.Needed_Hours[i][j] > item.Work_Hours[i][j])
                                count += mom.Needed_Hours[i][j] - item.Work_Hours[i][j];
                        }
                    }
                }


                hours[row, 0] = count;//count= number of hours ,no equle.
                hours[row, 1] = item.ID;
                row++;
                count = 0;

            }
            List<Nanny> top_nanny = new List<Nanny>();
            for (int n = 0; n < row && n < 5; n++)
            {
                int min = hours[0, 0];
                int index = 0;

                for (int i = 0; i < row; i++)
                {
                    if (hours[i, 0] < min)
                    {
                        min = hours[i, 1];
                        index = i;
                    }
                }
                top_nanny.Add(Get_N_list().Find(who => who.ID == min));
                hours[index, 0] = 10000;
            }
            return top_nanny;
        }

        ///founction that returns list of nannys at size 5 ,who better for mom  
        //in case that no match at all
        public IEnumerable<Nanny> Find_Closes_Nannys(Mother mom, Child child)
        {
            int distance = 0;
            List<Nanny> close_nanny = new List<Nanny>();
            var nannys = Search_Nanny_For_Mother(mom, child);
            if (mom.Search_Area != null)
            {
                distance = CalculateDistance(mom.Address, mom.Search_Area);
                foreach (var item in Get_N_list())
                {
                    if (distance <= CalculateDistance(mom.Address, item.Address))
                        close_nanny.Add(item);
                }

                return close_nanny;
            }
            else
                return nannys;
        }


        /// <summary>
        /// founction that returns list of children who do not have a nanny  
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Child> Childs_Without_Nanny()
        {
            return from item in dal.Get_Child_List()
                   where null == dal.Get_Contract_List().Find(who => who.Child_ID == item.ID)
                   select item;



        }

        /// <summary>
        /// Find nannys that works by vacations of Industry, Trade and Labor
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Nanny> Nannys_ITL()
        {
            List<Nanny> ITL_Nanny = new List<Nanny>();
            foreach (var item in Get_N_list())
            {
                if (!(item.State_Educations))
                    ITL_Nanny.Add(item);
            }
            return ITL_Nanny;
        }


        /// <summary>
        /// return list of all the contracts that are returned from the function
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public IEnumerable<Contract> Bool_Contacts_list(Func<Contract, bool> func = null)
        {
            IEnumerable<Contract> tmp = from item in Get_Contract_List()
                                        where func(item)
                                        select item;
            return tmp;
        }


        /// <summary>
        /// return the num of contracts that are returned from the function
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public int Num_Bool_Contacts_list(Func<Contract, bool> func = null)
        {
            var v = Bool_Contacts_list(func);
            return v.Count<Contract>();
        }
        /// <summary>
        /// grouping the nannies by age of children
        /// </summary>
        /// <param name="order"></param>
        /// <param name="by_max_age"></param>
        /// <returns></returns>
        public IEnumerable<IGrouping<int, Nanny>> Grop_Nannys_By_Age(bool order = false, bool by_max_age = false)
        {
            IEnumerable<IGrouping<int, Nanny>> groups;

            if (by_max_age)
            {
                groups = from item in dal.Get_N_list()
                         group item by Groups_Nanny(item.Max_Age);
            }
            else
            {
                groups = from item in dal.Get_N_list()
                         group item by Groups_Nanny(item.Min_Age);
            }

            if (order)
            {
                groups.OrderBy(s => s.Key);
            }
            return groups;

        }
        /// <summary>
        /// return Key to groping nannys each 6 months
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        private int Groups_Nanny(int age)
        {
            int i = 1;
            while ((age -= 6) > 0)
            {
                i++;
            }
            return i;

        }





        /// <summary>
        /// return groups of contracts group by distance (Each group 5 km) 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public IEnumerable<IGrouping<int, Contract>> Grop_Contract_By_Dictans_Nannys(bool order = false)
        {
            var groups = from item in dal.Get_Contract_List()
                         group item by (Grops_Contract(item));
            if (order)
            {
                groups.OrderBy(s => s.Key);
            }
            return groups;


        }


        /// <summary>
        /// //functions return Key for grop contacts(distance)(0,5,10,15...)
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        private int Grops_Contract(Contract contract)
        {
            var found_child = Get_Child_List().Find(who => who.ID == contract.Child_ID);
            var fuond_mom = Get_M_list().Find(who => who.ID == found_child.Id_Mom);
            var found_nanny = Get_N_list().Find(who => who.ID == contract.Nanny_ID);
            int distance = CalculateDistance(fuond_mom.Address, found_nanny.Address);
            distance /= 1000;//make at as km
            distance /= 2;
            return distance *= 2;
        }


        //private bool Exsit_Contract(Contract contarct)
        //{
        //    var found_chiild = dal.Get_Contract_List().Find(who => who.Child_ID == contarct.Child_ID);

            
        //}


        /// <summary>
        /// return child by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Child ReturnChildById(int id)
        {
            var found_child = Get_Child_List().Find(who => who.ID == id);
            return found_child;

        }

        //functions for combobox at Show contract
        public bool Signed_Contracts(Contract contract)
        {

            return contract.Contarct_Signed == true;
        }

        public bool Force_Contract(Contract contract)
        {
            return contract.Date_Of_Ending >= DateTime.Now;
        }

        public bool Is_Was_Meeting(Contract contract)
        {
            return contract.Was_Meeting == true;
        }




        #endregion

        #region check details
        /// <summary>
        /// Check_Nanny_Details
        /// </summary>
        /// <param name="nanny"></param>
        private void Check_Nanny_Details(Nanny nanny)
        {
            //check ID number
            string id_str = nanny.ID.ToString();
            if (!(CheckID(id_str) == true))
                throw new Exception(" ERROR! ID is Illegal ");

            //check birth_date
            var today = DateTime.Now;
            var today_date = (today.Year * 100 + today.Month) * 100 + today.Day;
            var birth_date = (nanny.Date.Year * 100 + nanny.Date.Month) * 100 + nanny.Date.Day;
            if (!((today_date - birth_date) / 10000 > 18))
                throw new Exception(" The nanny is too young ( < 18) ");

            //check number phone
            if (!(IsValid(nanny.Phone_Number) == true))
                throw new Exception(" ERROR! ID is Illegal ");
        }
        /// <summary>
        /// Check_Mother_Details
        /// </summary>
        /// <param name="mom"></param>
        private void Check_Mother_Details(Mother mom)
        {
            //check ID number
            string id_str = mom.ID.ToString();
            if (!(CheckID(id_str) == true))
                throw new Exception(" ERROR! ID is Illegal ");

            //check number phone
            if (!(IsValid(mom.Phone_Number) == true))
                throw new Exception(" ERROR! ID is Illegal ");
        }
        /// <summary>
        /// Check_Child_Details
        /// </summary>
        /// <param name="child"></param>
        private void Check_Child_Details(Child child)
        {
            //check ID number
            string idm_str = child.Id_Mom.ToString();
            if (!(CheckID(idm_str) == true))
                throw new Exception(" ERROR! ID is Illegal ");


            //check birth_date
            var today = DateTime.Now;
            var today_date = (today.Year * 100 + today.Month) * 100 + today.Day;
            var birth_date = (child.Date.Year * 100 + child.Date.Month) * 100 + child.Date.Day;
            if (!((today_date - birth_date) / 10000 > 18))
                throw new Exception(" The nanny is too young ( < 18) ");
        }
        /// <summary>
        ///  Check_Contract_Details
        /// </summary>
        /// <param name="contract"></param>
        private void Check_Contract_Details(Contract contract)
        {

            //check ID number
            string idc_str = contract.Child_ID.ToString();
            if (!(CheckID(idc_str) == true))
                throw new Exception(" ERROR! ID is Illegal ");
            string idm_str = contract.Nanny_ID.ToString();
            if (!(CheckID(idm_str) == true))
                throw new Exception(" ERROR! ID is Illegal ");

            var found_Nanny = Get_N_list().Find(Who => Who.ID == contract.Nanny_ID);

            //who child is less than 3 months old?
            DateTime f = new DateTime();
            IEnumerable<DateTime> c = GET_Date_Child(contract);
            IEnumerator enumerator = c.GetEnumerator();
            while (enumerator.MoveNext())
            {
                f = (DateTime)enumerator.Current;
                break;
            }

            var today = DateTime.Now;
            var today_date = (today.Year * 100 + today.Month) * 100 + today.Day;
            var birth_date = (f.Year * 100 + f.Date.Month) * 100 + f.Date.Day;
            if (!(((today_date - birth_date) / 100) % 100 >= 03))
                throw new Exception(" It is not possible to sign an employment contract for a child who is less than 3 months old ");
        }

        // Function to check ID number 
        public static bool CheckID(string IDNum)
        {
            // Validate correct input
            if (!Regex.IsMatch(IDNum, @"^\d{5,9}$"))
                return false;

            // The number is too short - add leading 0000
            IDNum = IDNum.PadLeft(9 - IDNum.Length, '0');

            // CHECK THE ID NUMBER 
            int mone = 0;
            for (int i = 0; i < 9; i++)
            {
                var incNum = Convert.ToInt32(IDNum[i].ToString());
                incNum *= (i % 2) + 1;
                if (incNum > 9)
                    incNum -= 9;
                mone += incNum;
            }
            if (mone % 10 == 0)
                return true;
            else
                return false;
        }


        // Function to check Phone number 
        public static bool IsValid(string s)
        {
            if (null == s)
                return false;
            var validPrefixes = new[] {
                  "02",
                  "03",
                  "04",
                  "08",
                  "09",
                  "071",
                  "072",
                  "073",
                  "074",
                  "076",
                  "077",
                  "078",
                  "079",
                  "050",
                  "051",
                  "052",
                  "053",
                  "054",
                  "055",
                  "058",
                  };
            int prefixLength = 0;
            var foundMatchingPrefix = false;
            foreach (var validPrefix in validPrefixes)
                if (s.StartsWith(validPrefix))
                {
                    prefixLength = validPrefix.Length;
                    foundMatchingPrefix = true;
                    break;
                }

            if (!foundMatchingPrefix)
                return false;

            if (s.Length == prefixLength)
                return false;

            int nextDigitIndex;
            if (s[prefixLength] == ' ')
            {
                nextDigitIndex = prefixLength + 1;
                // remaining length should be between 6 and 7
                if (!((6 == nextDigitIndex) || (7 == nextDigitIndex)))
                    return false;
            }
            else
            {
                nextDigitIndex = prefixLength;
                // remaining length should be between 6 and 7
                if (!((6 == nextDigitIndex) || (7 == nextDigitIndex)))
                    return false;
            }

            // check the rest of the string to be digits
            for (int i = nextDigitIndex; i < s.Length; i++)
                if (!(s[i] >= '0' && s[i] <= '9'))
                    return false;

            return true;
        }

        #endregion

        #region inits (for checking)


        public void init_nanny()
        {
            #region fill nanny

            this.Add_Nanny(new Nanny
            {
                ID = 22213432,
                First_Name = "Tamar",
                Last_Name = "Levi",
                Date = new DateTime(1994, 04, 14),
                Phone_Number = "0548838088",
                Address = "Gaza 23,Jerusalem, Israel",
                Is_Elevator = true,
                Floor = 2,
                experience = 4,
                Max_Childs = 5,
                Min_Age = 6,
                Max_Age = 12,
                Salary_Per_Hour = true,
                Pay_For_Hour = 30,
                Pay_For_Mount = 0,
                Does_Nanny_Work = new bool[6] { true, true, true, true, true, true },
                Work_Hours = new int[6][]{new int[2] { 8,14 },
         new int[2] { 8,14 },
         new int[2] { 8,14 },
         new int[2] { 8,14 },
          new int[2]{ 8,14 },

                            new int[2]   { 8,12 } },
                State_Educations = false,
                Recommendations = null,
            });

            this.Add_Nanny(new Nanny
            {
                ID = 204150759,
                First_Name = "Yael",
                Last_Name = "Ben-shimon",
                Date = new DateTime(1993, 09, 21),
                Phone_Number = "052563945",
                Address = "Yafo,Jerusalem, Israel",
                Is_Elevator = false,
                Floor = 2,
                experience = 2,
                Max_Childs = 4,
                Min_Age = 3,
                Max_Age = 6,
                Salary_Per_Hour = false,
                Pay_For_Hour = 25,
                Pay_For_Mount = 2000,
                Does_Nanny_Work = new bool[6] { true, true, true, true, true, true },
                Work_Hours = new int[6][]{new int[2]{ 6,14 },
                                        new int[2]{ 6,14 },
                                        new int[2]{ 6,14 },
                                        new int[2]{ 6,14 },
                                       new int[2] { 6,14 },
                                       new int[2] { 7,12 } },
                State_Educations = true,
                Recommendations = "Exelent",
            });

            this.Add_Nanny(new Nanny
            {
                ID = 203342134,
                First_Name = "ma'ayan",
                Last_Name = "Abergel",
                Date = new DateTime(1991, 12, 12),
                Phone_Number = "0505839201",
                Address = "Ben Yehuda 23,Jerusalem, Israel",
                Is_Elevator = true,
                Floor = 5,
                experience = 3,
                Max_Childs = 3,
                Min_Age = 9,
                Max_Age = 8,
                Salary_Per_Hour = false,
                Pay_For_Hour = 0,
                Pay_For_Mount = 3000,
                Does_Nanny_Work = new bool[6] { true, true, true, true, false, true },
                Work_Hours = new int[6][]{new int[2] { 7,14 },
          new int[2]{ 7,14 },
         new int[2] { 7,14 },
         new int[2] { 7,14 },
         new int[2] { 7,14 },
         new int[2] { 7,14 }
           },
                State_Educations = false,
                Recommendations = null,
            });
            #endregion

        }
        public void init_mother()
        {
            #region fill mom

            this.Add_Mom(new Mother
            {
                ID = 203867676,
                First_Name = "Tikva",
                Last_Name = "Cohen",
                Phone_Number = "0503923636",
                Address = "Gaza 10,Jerusalem,Israel",
                Search_Area = "Ben Yehuda,Jerusalem,Israel",
                Need_Nanny_Today = new bool[] { true, true, true, true, true, false },
                Needed_Hours = new int[6][]{new int[2] { 7,13 },
                                      new int[2]  { 7,13 },
                                      new int[2]  { 7,13 },
                                       new int[2] { 7,13 },
                                       new int[2] { 7,13 },
                                       new int[2] { 7,12 } },



                Commeents = "please be on time"
            });

            this.Add_Mom(new Mother
            {
                ID = 204441248,
                First_Name = "Shosana",
                Last_Name = "Levi",
                Phone_Number = "0503323212",
                Address = "Ben Yehuda,Jerusalem,Israel",
                Search_Area = "King George 30, Jerusalem,Israel",
                Need_Nanny_Today = new bool[] { false, true, true, true, true, true },
                Needed_Hours = new int[6][]{new int[2] { 8,14 },
                                        new int[2]{ 8,14 },
                                       new int[2] { 8,14 },
                                       new int[2] { 8,14 },
                                       new int[2] { 8,14 },
                                        new int[2]{ 8,12 } },



                Commeents = "please be on time"
            });

            this.Add_Mom(new Mother
            {
                ID = 301654372,
                First_Name = "Lea",
                Last_Name = "Hayun",
                Phone_Number = "0527765212",
                Address = "Ma'ale HaShalom,Jerusalem,Israel",
                Search_Area = "King George,Jerusalem,Israel",
                Need_Nanny_Today = new bool[] { true, true, true, true, true, true },
                Needed_Hours = new int[6][]{new int[2] { 7,14 },
                                       new int[2] { 7,14 },
                                       new int[2] { 7,14 },
                                       new int[2] { 7,14 },
                                      new int[2]  { 8,14 },
                                      new int[2]  { 8,12 } },

                Commeents = "please be on time"
            });
            #endregion


        }

        public void init_child()
        {
            #region fill child

            this.Add_Chaild(new Child
            {
                ID = 0,
                Id_Mom = 203867676,
                First_Name = "yakov",
                gender = Gender.MALE,
                Date = new DateTime(2017, 05, 09),
                s = With_Special_Needs.NOT_NEED,
                Special_Needs = null,
            });

            this.Add_Chaild(new Child
            {
                ID = 1,
                Id_Mom = 204441248,
                First_Name = "Netanel",
                gender = Gender.MALE,
                Date = new DateTime(2016, 07, 13),
                s = With_Special_Needs.NEED,
                Special_Needs = "Blind",
            });

            this.Add_Chaild(new Child
            {
                ID = 2,
                Id_Mom = 203867676,
                First_Name = "Tomer",
                gender = Gender.MALE,
                Date = new DateTime(2017, 05, 09),
                s = With_Special_Needs.NOT_NEED,
                Special_Needs = null,
            });

            this.Add_Chaild(new Child
            {
                ID = 3,
                Id_Mom = 203867676,
                First_Name = "eliezer",
                gender = Gender.MALE,
                Date = new DateTime(2014, 05, 09),
                s = With_Special_Needs.NOT_NEED,
                Special_Needs = null,
            });
            #endregion

        }
        public void init_contract()
        {

            this.Add_Contract(new Contract
            {
                Code_Of_Contract = 0,
                Nanny_ID = 203342134,
                Child_ID = 0,
                Was_Meeting = true,
                Contarct_Signed = true,
                Pay_For_Hour = 25,
                Pay_for_Month = 2000,
                Is_PayPer_Hour = true,
                Date_Of_Begining = new DateTime(2017, 12, 19),
                Date_Of_Ending = new DateTime(2018, 05, 19),
            });

            this.Add_Contract(new Contract
            {
                Code_Of_Contract = 1,
                Nanny_ID = 204150759,
                Child_ID = 1,
                Was_Meeting = false,
                Contarct_Signed = true,
                Pay_For_Hour = 30,
                Pay_for_Month = 3000,
                Is_PayPer_Hour = false,
                Date_Of_Begining = new DateTime(2017, 12, 19),
                Date_Of_Ending = new DateTime(2018, 06, 19),
            });

            this.Add_Contract(new Contract
            {
                Code_Of_Contract = 2,
                Nanny_ID = 22213432,
                Child_ID = 2,
                Was_Meeting = true,
                Contarct_Signed = false,
                Pay_For_Hour = 35,
                Pay_for_Month = 3500,
                Is_PayPer_Hour = true,
                Date_Of_Begining = new DateTime(2017, 12, 19),
                Date_Of_Ending = new DateTime(2019, 12, 19),
            });

            #endregion

        }


    }
}
