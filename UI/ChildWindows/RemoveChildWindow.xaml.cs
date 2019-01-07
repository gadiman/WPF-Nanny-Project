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
    /// Interaction logic for RemoveChildWindow.xaml
    /// </summary>
    public partial class RemoveChildWindow : Window
    {
        IBL bl;
        Child child;
        public RemoveChildWindow()
        {
            bl = FactorySingletonBL.GetBL();
            child = new Child();
            DataContext = child;
            InitializeComponent();
            datalist.ItemsSource = bl.Get_Child_List();
            comboBox.ItemsSource = bl.Get_Child_List();
            comboBox.DisplayMemberPath = "ID";
            comboBox.SelectedValuePath = "ID";
        }

        private void RemoveChild(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Remove_chaild(child.ID);
                label.Content = "";
                MessageBox.Show("Deletion Child succeeded");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            child = new BE.Child(comboBox.SelectedItem as BE.Child);
            label.Content = child.ToString();
            RemoveButton.IsEnabled = true;
        }

        private void datalist_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (datalist.SelectedItem != null)
            {
                child = new BE.Child(datalist.SelectedItem as BE.Child);
                label.Content = child.ToString();
                comboBox.DataContext = child;
                RemoveButton.IsEnabled = true;
            }
        }
    }
}
