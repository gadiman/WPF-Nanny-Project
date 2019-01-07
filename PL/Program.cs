using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE;


namespace PL
{
    class Program
    {
        public static BL.IBL bl = FactorySingletonBL.GetBL();

        static void Main(string[] args)
        {

            Console.WriteLine("details of datasource:\n");

            print_lists();

            #region testing logic function


            //print list of nannys by request of mothers, foreach mother
            foreach (var item in bl.Get_M_list())
            {
                var n9 = bl.Search_Nanny_For_Mother(item);
                if (n9 != null)
                    Console.WriteLine("found nannys by request: \n");
                    Console.WriteLine("for: "+ item.First_Name + "\n");
                foreach (var it in n9)
                {
                    Console.WriteLine(it+"\n");
                }
                
            }


            
/*
            //print list of nanny that closes to mothers, foreach mother
            foreach (var item in bl.Get_M_list())
            {
                var n2 = bl.Find_Closes_Nannys(item,);
                if(n2 != null)
                Console.WriteLine("\nThe nannys are closes to mother-" +item.First_Name+"  :\n");
                foreach (var it in n2)
                {
                    Console.WriteLine(it + "\n");
                }

            }
*/
            //print the children who do not have a nanny
            var l = bl.Childs_Without_Nanny();
            if (l.Count() != 0)
            {
                Console.WriteLine("\nThe children who do not have a nanny:\n ");
                foreach (var item in l)
                {
                    Console.WriteLine(item);
                }
            }


            // Print nannys that works by vacations of Industry, Trade and Labor
            Console.WriteLine("\nThe nannys that works by vacations of Industry, Trade and Labor:\n ");
            var r = bl.Nannys_ITL();
            foreach (var item in r)
            {
                Console.WriteLine(item);
            }

            //print all contracts that are returned from the function
            Console.WriteLine("\nprint all contracts that are returned from the function (signed): \n");
            Console.WriteLine("\nnumber of contracts :");
            Console.WriteLine(bl.Num_Bool_Contacts_list(who => who.Contarct_Signed) + " : \n");
            var n = bl.Bool_Contacts_list(who => who.Contarct_Signed);
            foreach (var item in n)
            {
                Console.WriteLine(item);
            }

            //print all the groping of childs by Age (6 months)
            Console.WriteLine("\nprint all the groping of Nannys by Age of childs (2 months)\n");
            var list = bl.Grop_Nannys_By_Age(true,true);
            foreach (var it in list)
            {
                if (it != null)
                {
                    Console.WriteLine(it.Key + "  :\n");
                    foreach (var item in it)
                    {
                        Console.WriteLine(item);
                    }
                }
            }


            //print all the groping of Nannys by distance (5km)
            Console.WriteLine("\nprint all the groping of Nannys by distance (5 km)");
            var list1 = bl.Grop_Contract_By_Dictans_Nannys(true);
            foreach (var it in list1)
            {
                if (it != null)
                {
                    Console.WriteLine(it.Key + "  :\n");
                    foreach (var item in it)
                    {
                        Console.WriteLine(item);
                    }
                }
            }
            #endregion

            int option = -1;
            string choice;
            do
            {
                Console.WriteLine("\n");
                Console.WriteLine("Enter: Update - 1 / Remove - 2 / Print All - 3 / Exit - 0 ");
                choice = Console.ReadLine();
                option = Int32.Parse(choice);
                switch (option)
                {
                    case 0:

                        break;
                    case 1:
                        Update();
                        break;
                    case 2:
                        Remove();
                        break;
                    case 3:
                        print_lists();
                        break;
                    default:
                        Console.WriteLine("worng key\n");
                        break;
                }
                Console.WriteLine();
            }
            while (option != 0);


            void Update()
            {
                try
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("Enter 0 to update nanny \n" +
                                      "Enter 1 to update mother \n " +
                                      "Enter 2 to update child\n" +
                                        "Enter 3 to update contract \n");


                    int tepy = int.Parse(Console.ReadLine());

                    Console.WriteLine("enter ID  or code for contract\n");
                    int id = int.Parse(Console.ReadLine());

                    switch (tepy)
                    {
                        case 0:
                            var found = bl.Get_N_list().Find(who => who.ID == id);
                            if (found == null)
                            {
                                Console.WriteLine("worng id\n");
                                break;
                            }
                            Console.WriteLine("eEnter the name of the proprty, you want to change of this list:\n");
                            foreach (var item in found.GetType().GetProperties())
                            {
                                Console.WriteLine(item + "\n");
                            }
                            string prop = Console.ReadLine();
                            foreach (var item in found.GetType().GetProperties())
                            {
                                if (item.ToString() == prop)
                                {
                                    Console.WriteLine("enter new value");
                                    item.SetValue(found, Console.ReadLine(), null);
                                }
                            }
                            bl.Update_Ndetails(found);
                            Console.WriteLine("\n\n List after the Change:\n");
                            print_lists();
                            break;
                        case 1:
                            var found1 = bl.Get_M_list().Find(who => who.ID == id);
                            if (found1 == null)
                            {
                                Console.WriteLine("worng id\n");
                                break;
                            }
                            Console.WriteLine("Enter the name of the proprty, you want to change of this list:\n");
                            foreach (var item in found1.GetType().GetProperties())
                            {
                                Console.WriteLine(item + "\n");
                            }
                            string prop1 = Console.ReadLine();
                            foreach (var item in found1.GetType().GetProperties())
                            {
                                if (item.ToString() == prop1)
                                {
                                    Console.WriteLine("enter new value");
                                    item.SetValue(found1, Console.ReadLine(), null);
                                    break;
                                }
                            }
                            bl.Update_Mdetails(found1);
                            Console.WriteLine("\n\n List after the Change:\n");
                            print_lists();
                            break;
                        case 2:
                            var found2 = bl.Get_Child_List().Find(who => who.ID == id);
                            if (found2 == null)
                            {
                                Console.WriteLine("worng id\n");
                                break;
                            }
                            Console.WriteLine("eEnter the name of the proprty, you want to change of this list:\n");
                            foreach (var item in found2.GetType().GetProperties())
                            {
                                Console.WriteLine(item + "\n");
                            }
                            string prop2 = Console.ReadLine();
                            foreach (var item in found2.GetType().GetProperties())
                            {
                                if (item.ToString() == prop2)
                                {
                                    Console.WriteLine("enter new value");
                                    item.SetValue(found2, Console.ReadLine(), null);
                                }

                            }
                            bl.Update_Cdetails(found2);
                            Console.WriteLine("\n\n List after the Change:\n");
                            print_lists();
                            break;
                        case 3:
                            var found3 = bl.Get_Contract_List().Find(who => who.Code_Of_Contract == id);
                            if (found3 == null)
                            {
                                Console.WriteLine("worng id\n");
                                break;
                            }
                            Console.WriteLine("eEnter the name of the proprty, you want to change of this list:\n");
                            foreach (var item in found3.GetType().GetProperties())
                            {
                                Console.WriteLine(item + "\n");
                            }
                            string prop3 = Console.ReadLine();
                            foreach (var item in found3.GetType().GetProperties())
                            {
                                if (item.ToString() == prop3)
                                {
                                    Console.WriteLine("enter new value");
                                    item.SetValue(found3, Console.ReadLine(), null);
                                }
                            }
                            bl.Update_Contract(found3);
                            Console.WriteLine("\n\n List after the Change:\n");
                            print_lists();
                            break;
                        default:
                            Console.WriteLine("wrong id\n");
                            break;
                    }

                }
                catch (Exception problem)
                {

                    Console.WriteLine(problem.Message);
                }
            }



            void Remove()
            {
                try
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("Enter 0 to update nanny \n" +
                                      "Enter 1 to update mother \n " +
                                      "Enter 2 to update child\n" +
                                        "Enter 3 to update contract \n");


                    int tepy = int.Parse(Console.ReadLine());

                    Console.WriteLine("enter ID  or code for contract\n");
                    int id = int.Parse(Console.ReadLine());

                    switch (tepy)
                    {
                        case 0:
                            bl.Remomve_Nanny(id);
                            Console.WriteLine("\n\n List after the removing:\n");
                            print_lists();
                            break;
                        case 1:
                            bl.Remove_Mom(id);
                            Console.WriteLine("\n\n List after the removing:\n");
                            print_lists();
                            break;
                        case 2:
                            bl.Remove_chaild(id);
                            Console.WriteLine("\n\n List after the removing:\n");
                            print_lists();
                            break;

                        case 3:
                            uint code = (uint)id; // = uint.Parse(Console.ReadLine());
                            bl.Remove_Contract(code);
                            Console.WriteLine("\n\n List after the removing:\n");
                            print_lists();
                            break;
                        default:
                            Console.WriteLine("wrong id\n");
                            break;
                    }

                }
                catch (Exception problem)
                {

                    Console.WriteLine(problem.Message);
                }
            }

        








        }

       public static void print_lists()
        {
            //print the lists
            Console.WriteLine("nannys:\n");
            foreach (var item in bl.Get_N_list())
            {
                Console.WriteLine(item);
                Console.WriteLine('\n');
            }
            Console.WriteLine("\nmothers:\n");
            foreach (var item in bl.Get_M_list())
            {
                Console.WriteLine(item);
                Console.WriteLine('\n');
            }
            Console.WriteLine("\nchilds:\n");
            foreach (var item in bl.Get_Child_List())
            {
                Console.WriteLine(item);
                Console.WriteLine('\n');
            }
            Console.WriteLine("\ncontracts:\n");
            foreach (var item in bl.Get_Contract_List())
            {
                Console.WriteLine(item);
                Console.WriteLine('\n');
            }
        }






    }
}

