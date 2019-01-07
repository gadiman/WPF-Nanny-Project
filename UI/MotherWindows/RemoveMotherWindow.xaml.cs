using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for RemoveMotherWindow.xaml
    /// </summary>
    public partial class RemoveMotherWindow : Window
    {


        IBL bl;
        Mother mother;
        public RemoveMotherWindow()
        {
            bl = FactorySingletonBL.GetBL();
            mother = new Mother();
            DataContext = mother;
            InitializeComponent();
            comboBox.ItemsSource = bl.Get_M_list();
            comboBox.DisplayMemberPath = "ID";
            comboBox.SelectedValuePath = "ID";
            datalist.ItemsSource = bl.Get_M_list();
        }

        private void RemoveMother(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Remove_Mom(mother.ID);
                MessageBox.Show("Deletion Mother succeeded");
                label.Content = "";
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mother = new BE.Mother(comboBox.SelectedItem as BE.Mother);
            label.Content = mother.ToString();
            RemoveButton.IsEnabled = true;
        }

        private void datalist_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (datalist.SelectedItem != null)
            {
                mother = new BE.Mother(datalist.SelectedItem as BE.Mother);
                label.Content = mother.ToString();
                comboBox.DataContext = mother;
                RemoveButton.IsEnabled = true;
            }
        }
    }
}
