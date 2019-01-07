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
    /// Interaction logic for RemoveNannyWindow.xaml
    /// </summary>
    public partial class RemoveNannyWindow : Window
    {
        IBL bl;
        Nanny nanny;
        public RemoveNannyWindow()
        {
            bl = FactorySingletonBL.GetBL();
            nanny = new Nanny();
            DataContext = nanny;
            InitializeComponent();
            comboBox.ItemsSource = bl.Get_N_list();
            datalist.ItemsSource = bl.Get_N_list();
            comboBox.DisplayMemberPath = "ID";
            comboBox.SelectedValuePath = "ID";
        }

        private void RemoveNanny(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Remomve_Nanny(nanny.ID);
                MessageBox.Show("Deletion Nanny succeeded");
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
            nanny = new BE.Nanny(comboBox.SelectedItem as BE.Nanny);
            label.Content = nanny.ToString();
            RemoveButton.IsEnabled = true;

        }

        private void datalist_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (datalist.SelectedItem != null)
            {

                nanny = new BE.Nanny(datalist.SelectedItem as BE.Nanny);
                label.Content = nanny.ToString();
                comboBox.DataContext = nanny;
                RemoveButton.IsEnabled = true;
            }
        }
    }
}
