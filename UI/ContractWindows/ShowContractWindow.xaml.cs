using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for ShowContractWindow.xaml
    /// </summary>
    public partial class ShowContractWindow : Window
    {
        Func<BE.Contract, bool> condition;
        string choice;
        List<string> function;
        BL.IBL bl;
        BE.Contract contract;
        ListCollectionView collection;




        public ShowContractWindow()
        {

            bl = BL.FactorySingletonBL.GetBL();
            InitializeComponent();
            contract = new BE.Contract();
            DataContext = contract;
            dataGrid.ItemsSource = bl.Get_Contract_List();
            textBox.Text = bl.Get_Contract_List().Count().ToString();
            
            function = new List<string>();
            function.Add("show all");
            function.Add("If in force");
            function.Add("If signed");
            function.Add("If was meeting");
            comboBox.ItemsSource = function;

        }

     

        private void Selected_Cells_Changed_dataGrid(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {

                contract = new BE.Contract(dataGrid.SelectedItem as BE.Contract);
                DataContext = contract;
            }

        }

        private void groping_button_click(object sender, RoutedEventArgs e)
        {


            new Thread(() =>
            {
                collection = new ListCollectionView(bl.Get_Contract_List());
                var groping_list = bl.Grop_Contract_By_Dictans_Nannys();

                foreach (var it in groping_list)
                {
                    if (it != null)
                    {
                        foreach (var item in it)
                        {
                            string tmp = (it.Key ).ToString();
                            string tmp2 = (it.Key + 2).ToString();
                            string tmp3 = tmp + "-" + tmp2 + " Km";
                            item.gropcode = tmp3.ToString();
                        }
                    }
                }

                collection.GroupDescriptions.Add(new PropertyGroupDescription("gropcode"));
                

                Dispatcher.Invoke(new Action(() =>
                {
                    dgData.ItemsSource = collection;
                }));
            }).Start();
        }

        private void ComboBox_selected_functions(object sender, SelectionChangedEventArgs e)
        {
            if(comboBox.SelectedItem != null)
            {
                try
                {
                    choice = ((string)(comboBox.SelectedItem));
                    switch (choice)
                    {
                        case "If was meeting":
                            {
                                condition = new Func<BE.Contract, bool>(bl.Is_Was_Meeting);
                                dataGrid.ItemsSource = bl.Bool_Contacts_list(condition);
                                textBox.Text = bl.Num_Bool_Contacts_list(condition).ToString();
                                break;
                            }
                        case "If signed":
                            {
                                condition = new Func<BE.Contract, bool>(bl.Signed_Contracts );
                                dataGrid.ItemsSource = bl.Bool_Contacts_list(condition);
                                textBox.Text = bl.Num_Bool_Contacts_list(condition).ToString();
                                break;
                            }
                        case "If in force":
                            {
                                condition = new Func<BE.Contract, bool>(bl.Force_Contract);
                                dataGrid.ItemsSource = bl.Bool_Contacts_list(condition);
                                textBox.Text = bl.Num_Bool_Contacts_list(condition).ToString();
                                break;
                            }
                        case "show all":
                            {
                                dataGrid.ItemsSource = bl.Get_Contract_List();
                                textBox.Text = bl.Get_Contract_List().Count().ToString();
                                break;
                            }
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }





            }
        }
    }
      
}
