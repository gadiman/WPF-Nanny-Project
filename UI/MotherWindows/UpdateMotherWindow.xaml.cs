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
    /// Interaction logic for UpdateMotherWindow.xaml
    /// </summary>
    public partial class UpdateMotherWindow : Window
    {
        BL.IBL bl;
        BE.Mother mother;

        public UpdateMotherWindow()
        {
            InitializeComponent();
            bl = BL.FactorySingletonBL.GetBL();
            mother = new BE.Mother();
            DataContext = mother;
            comboBox.DisplayMemberPath = "ID";
            comboBox.SelectedValuePath = "ID";
            datalist.ItemsSource = bl.Get_M_list();
            comboBox.ItemsSource= bl.Get_M_list();
            ItemSourceForComboBox();
        }

        private void datalist_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (datalist.SelectedItem != null)
            {
                mother = new BE.Mother(datalist.SelectedItem as BE.Mother);
                DataContext = mother;
                AddButton.IsEnabled = true;
            }
        }
        private void comboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (comboBox.SelectedItem != null)
            {
                mother = new BE.Mother(comboBox.SelectedItem as BE.Mother);
                DataContext = mother;

            }
        }

        private void UpdateMotherClick(object sender, RoutedEventArgs e)
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
                       tmp+="Invalid First name! \n";
                        first_NameTextBox.Text = "";
                        count_ec++;
                    }
                }
                bool check2 = first_NameTextBox.Text.Any(who => char.IsLetter(who));
                if (first_NameTextBox.Text !=null)
                {
                    if (!check2)
                    {
                        last_NameTextBox.BorderBrush = Brushes.Red;
                        tmp += "Invalid Last name! \n";
                        last_NameTextBox.Text = "";
                        count_ec++;
                    }
                }
                if (addressTextBox.Text != null)
                {
                    if (typeof(string) != (addressTextBox.Text.GetType()))
                    {
                        addressTextBox.BorderBrush = Brushes.Red;
                        tmp += "Invalid Address! \n";
                        addressTextBox.Text = "";
                        count_ec++;
                    }
                }
                if (search_AreaTextBox.Text != null)
                {
                    if (typeof(string) != (search_AreaTextBox.Text.GetType()))
                    {
                        search_AreaTextBox.BorderBrush = Brushes.Red;
                        tmp += "Invalid Address! \n";
                        search_AreaTextBox.Text = "";
                        count_ec++;
                    }
                }
                bool check3 = first_NameTextBox.Text.Any(who => char.IsLetter(who));
                if (phone_NumberTextBox.Text != null)
                {
                    if (!check3 ||phone_NumberTextBox.ToString().Length < 9)
                    {
                        phone_NumberTextBox.BorderBrush = Brushes.Red;
                        tmp += "Invalid Phone Number! \n";
                        phone_NumberTextBox.Text = "";
                        count_ec++;
                    }
                }
                //check if selected hours
                bool flag = false;
                foreach (var item in mother.Needed_Hours)
                {
                    if (item[0] != 0 && item[1] != 0)
                        flag = true;
                }
                if (!flag)
                {
                    tmp += "No hours selected \n";
                    count_ec++;
                }


                //check if hours(begin <end) ok
                flag = true;
                foreach (var item in mother.Needed_Hours)
                {
                    if (item[0] >item[1])
                        flag = false;
                }
                if (!flag)
                {
                    tmp += "Start time is less than end time\n";
                    count_ec++;
                }



                if (count_ec != 0)
                    throw new Exception(tmp);
                else
                {
                   
                    bl.Update_Mdetails(mother);
                    MessageBox.Show("Update Mother successful");
                    mother = new BE.Mother();
                    this.DataContext = mother;
                    Close();
                }
            }
            catch (Exception ex)
            {
               
                MessageBox.Show(ex.Message);
            }
        }


        private void ItemSourceForComboBox()
        {

            List<int> hours = new List<int>();
            for (int i = 7; i <= 20; i++)
            {

                hours.Add(i);

            }

            comboBox_sunday1.ItemsSource = hours;
            comboBox_sunday2.ItemsSource = hours;
            comboBox_monday1.ItemsSource = hours;
            comboBox_monday2.ItemsSource = hours;
            comboBox_tuesday1.ItemsSource = hours;
            comboBox_tuesday2.ItemsSource = hours;
            comboBox_wednesday1.ItemsSource = hours;
            comboBox_wednesday2.ItemsSource = hours;
            comboBox_thursday1.ItemsSource = hours;
            comboBox_thursday2.ItemsSource = hours;
            comboBox_friday1.ItemsSource = hours;
            comboBox_friday2.ItemsSource = hours;

        }
    }
}
