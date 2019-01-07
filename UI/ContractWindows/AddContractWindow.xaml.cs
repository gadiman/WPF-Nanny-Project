using System;
using System.Collections.Generic;
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
using BE;

namespace UI
{
    /// <summary>
    /// Interaction logic for AddContractWindow.xaml
    /// </summary>
    public partial class AddContractWindow : Window
    {
        BL.IBL bl;
        BE.Contract contract;
        IEnumerable<BE.Nanny> nannyList;

        public AddContractWindow()
        {
            InitializeComponent();
            bl = BL.FactorySingletonBL.GetBL();
            contract = new BE.Contract() { Date_Of_Begining = DateTime.Now, Date_Of_Ending = DateTime.Now  };
            DataContext = contract;

            this.child_IDComboBox.ItemsSource = bl.Get_Child_List();
            this.child_IDComboBox.DisplayMemberPath = "ID";
            this.child_IDComboBox.SelectedValuePath = "ID";
            nannyList = new List<BE.Nanny>();

          
            pay_for_MonthTextBox.Text ="  ";//for triger
        }

        private void AddContractClick(object sender, RoutedEventArgs e)
        {
         
            try
            {
                if ((DateTime)date_Of_BeginingDatePicker.SelectedDate >= (DateTime)date_Of_EndingDatePicker.SelectedDate)
                    throw new Exception("Invalid Date!");

                bl.Add_Contract(contract);
                MessageBox.Show("Add Contract successful");
                contract = new BE.Contract();
                this.DataContext = contract;
                Close();

                        
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
           
          
        }

    
    private void child_IDComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (child_IDComboBox.SelectedItem != null)
            {
                try
                {
                    var Child = new BE.Child(child_IDComboBox.SelectedItem as BE.Child);
                    var Mom = bl.Get_M_list().Find(who => who.ID == Child.Id_Mom);
                    contract.Child_ID = Child.ID;

                    //find top nannys
                    new Thread(() =>
                    {
                        var groping_list = bl.Find_Closes_Nannys(Mom, Child).ToList();

                        Dispatcher.Invoke(new Action(() =>
                        {
                            datalist.ItemsSource = groping_list;
                        }));
                    }).Start();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void datalist_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (datalist.SelectedItem != null)
            {
                BE.Nanny nanny = new BE.Nanny(datalist.SelectedItem as BE.Nanny);
                nanny_IDTextBlock.Text = nanny.ID.ToString();
                is_PayPer_HourCheckBox.IsChecked = (bool)nanny.Salary_Per_Hour;
                pay_For_HourTextBox.Text = nanny.Pay_For_Hour.ToString();
                pay_for_MonthTextBox.Text = nanny.Pay_For_Mount.ToString();

                DataContextNanny(nanny);
                AddButton.IsEnabled = true;
              
            }
        }

     

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void DataContextNanny(Nanny nanny)
        {
            contract.Nanny_ID = nanny.ID;
            contract.Is_PayPer_Hour = nanny.Salary_Per_Hour;
            
            contract.Pay_For_Hour = nanny.Pay_For_Hour;
            contract.Pay_for_Month = nanny.Pay_For_Mount;
        }
       
    }
}

