using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for UpdateContractWindow.xaml
    /// </summary>
    public partial class UpdateContractWindow : Window
    {
        BL.IBL bl;
        BE.Contract contract;

        public UpdateContractWindow()
        {
            InitializeComponent();
            bl = BL.FactorySingletonBL.GetBL();
            contract = new BE.Contract();
            DataContext = contract;
            comboBox.DisplayMemberPath = "Code_Of_Contract";
            comboBox.SelectedValuePath = "Code_Of_Contract";
            datalist.ItemsSource = bl.Get_Contract_List();
            comboBox.ItemsSource = bl.Get_Contract_List();
        }

        private void UpdateContractClick(object sender, RoutedEventArgs e)
        {
        
            try
            {

                if ((DateTime)date_Of_BeginingDatePicker.SelectedDate >= (DateTime)date_Of_EndingDatePicker.SelectedDate)
                    throw new Exception("Invalid Date!");

                else
                {

                    bl.Update_Contract(contract);
                    MessageBox.Show("Update Contract successful");
                    contract = new BE.Contract();
                    this.DataContext = contract;
                    Close();

                }
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message);
            }
        }

        private void datalist_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (datalist.SelectedItem != null)
            {
                contract = new BE.Contract(datalist.SelectedItem as BE.Contract);
                DataContext = contract;
                UpdateButton.IsEnabled = true;
            }
        }
        private void comboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {

                if (comboBox.SelectedItem != null)
                {
                    contract = new BE.Contract(comboBox.SelectedItem as BE.Contract);
                    DataContext = contract;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void is_pay(object sender, RoutedEventArgs e)
        {
            if (is_PayPer_HourCheckBox.IsChecked == true)
            {
                pay_for_MonthTextBox.IsEnabled = false;
                pay_For_HourTextBox.IsEnabled = true;
            }
            else if (is_PayPer_HourCheckBox.IsChecked == false)
            {
                pay_For_HourTextBox.IsEnabled = false;
                pay_for_MonthTextBox.IsEnabled = true;
            }
        }
    }
}
