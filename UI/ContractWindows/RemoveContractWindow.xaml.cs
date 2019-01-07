using BE;
using BL;
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
    /// Interaction logic for RemoveContractWindow.xaml
    /// </summary>
    public partial class RemoveContractWindow : Window
    {
        IBL bl;
        Contract contract;
        public RemoveContractWindow()
        {
            bl = FactorySingletonBL.GetBL();
            contract = new Contract();
            DataContext = contract;
            InitializeComponent();
            comboBox.ItemsSource = bl.Get_Contract_List();
            datalist.ItemsSource = bl.Get_Contract_List();
            comboBox.DisplayMemberPath = "Code_Of_Contract";
            comboBox.SelectedValuePath = "Code_Of_Contract";
        }

        private void RemoveContract(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Remove_Contract(contract.Code_Of_Contract);
                label.Content = "";
                MessageBox.Show("Deletion Contract succeeded");
                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            contract = new BE.Contract(comboBox.SelectedItem as BE.Contract);
            label.Content = contract.ToString();
            RemoveButton.IsEnabled = true;
        }

        private void datalist_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (datalist.SelectedItem != null)
            {

                contract = new BE.Contract(datalist.SelectedItem as BE.Contract);
                label.Content = contract.ToString();
                comboBox.DataContext = contract;
                RemoveButton.IsEnabled = true;
            }
        }
    }
}
