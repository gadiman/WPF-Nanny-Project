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
    /// Interaction logic for UpdateChildWindow.xaml
    /// </summary>
    public partial class UpdateChildWindow : Window
    {
        BL.IBL bl;
        BE.Child child;

        public UpdateChildWindow()
        {
            InitializeComponent();
            bl = BL.FactorySingletonBL.GetBL();
            child = new BE.Child();
            DataContext = child;
            comboBox.DisplayMemberPath = "ID";
            comboBox.SelectedValuePath = "ID";
            datalist.ItemsSource = bl.Get_Child_List();
            comboBox.ItemsSource = bl.Get_Child_List();
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.sComboBox.ItemsSource = Enum.GetValues(typeof(BE.With_Special_Needs));
            this.id_MomComboBox.ItemsSource = bl.Get_M_list();
            this.id_MomComboBox.DisplayMemberPath = "ID";
            this.id_MomComboBox.SelectedValuePath = "ID";
        }

        private void datalist_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (datalist.SelectedItem != null)
            {
                child = new BE.Child(datalist.SelectedItem as BE.Child);
                DataContext = child;
                AddButton.IsEnabled = true;

            }
        }
        private void comboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (comboBox.SelectedItem != null)
            {
                child = new BE.Child(comboBox.SelectedItem as BE.Child);
                DataContext = child;

            }
        }

        private void UpdateChildClick(object sender, RoutedEventArgs e)
        {
        
            string tmp = "";
            int count_ec = 0;
            try
            {
                bool check1 = first_NameTextBox.Text.Any(who => char.IsLetter(who));
                if (first_NameTextBox.Text != null)
                {
                    if (!check1)
                    {
                        first_NameTextBox.BorderBrush = Brushes.Red;
                       tmp+= "Invalid First name! \n";
                        first_NameTextBox.Text = "";
                        count_ec++;
                    }
                }
                if (count_ec != 0)
                    throw new Exception(tmp);
                else
                {
                    
                    bl.Update_Cdetails(child);
                    child = new BE.Child();
                    MessageBox.Show("Update child successful");
                    this.DataContext = child;
                    Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }   
}

