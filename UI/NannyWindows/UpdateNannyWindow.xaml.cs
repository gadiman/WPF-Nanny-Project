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
    /// Interaction logic for UpdateNannyWindow.xaml
    /// </summary>
    public partial class UpdateNannyWindow : Window
    {
        BL.IBL bl;
        BE.Nanny nanny;

        public UpdateNannyWindow()
        {
            InitializeComponent();
            bl = BL.FactorySingletonBL.GetBL();
            nanny = new BE.Nanny() ;
            DataContext = nanny;
            comboBox.DisplayMemberPath = "ID";
            comboBox.SelectedValuePath = "ID";
            datalist.ItemsSource = bl.Get_N_list();
            comboBox.ItemsSource = bl.Get_N_list();
            ItemSourceForComboBox();
        }

        private void datalist_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (datalist.SelectedItem != null)
            {
                nanny = new BE.Nanny(datalist.SelectedItem as BE.Nanny);
                DataContext = nanny;
                AddButton.IsEnabled = true;

            }
        }
        private void comboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (comboBox.SelectedItem != null)
            {
                nanny = new BE.Nanny(comboBox.SelectedItem as BE.Nanny);
                DataContext = nanny;

            }
        }

        private void UpdateNannyClick(object sender, RoutedEventArgs e)
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
                        tmp += "Invalid First name! \n";
                        first_NameTextBox.Text = "";
                        count_ec++;
                    }
                }

                bool check2 = first_NameTextBox.Text.Any(who => char.IsLetter(who));
                if (last_NameTextBox.Text != null)
                {
                    if (!check2)
                    {
                        last_NameTextBox.BorderBrush = Brushes.Red;
                        tmp += "Invalid Last name! \n";
                        last_NameTextBox.Text = "";
                        count_ec++;
                    }
                }
                if ((iDTextBox.Text != null) && (iDTextBox.Text != "0"))
                {
                    if (typeof(int) != (Convert.ToInt32(iDTextBox.Text).GetType()) )
                    {
                        iDTextBox.BorderBrush = Brushes.Red;
                        tmp += "Invalid ID Number! \n";
                        iDTextBox.Text = "";
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
                if (phone_NumberTextBox.Text != null)
                {
                    if (phone_NumberTextBox.ToString().Length < 9)
                    {
                        phone_NumberTextBox.BorderBrush = Brushes.Red;
                        tmp += "Invalid Phone Number! \n";
                        phone_NumberTextBox.Text = "";
                        count_ec++;
                    }
                }
                if (floorTextBox.Text != null)
                {
                    if ((typeof(int) != (Convert.ToInt32(floorTextBox.Text).GetType())))
                    {
                        floorTextBox.BorderBrush = Brushes.Red;
                        tmp += "Invalid Value For Floor! \n";
                        floorTextBox.Text = "";
                        count_ec++;
                    }
                }
                if (max_ChildsTextBox.Text != null)
                {
                    if ((typeof(int) != (Convert.ToInt32(max_ChildsTextBox.Text).GetType())))
                    {
                        max_ChildsTextBox.BorderBrush = Brushes.Red;
                        tmp += "Invalid Value For Maximum Of Child! (integer) \n";
                        max_ChildsTextBox.Text = "";
                        count_ec++;
                    }
                }
                if (min_AgeTextBox.Text != null)
                {
                    if ((typeof(int) != (Convert.ToInt32(min_AgeTextBox.Text).GetType())) || ((Convert.ToInt32(min_AgeTextBox.Text)) > (Convert.ToInt32(max_AgeTextBox.Text))))
                    {
                        min_AgeTextBox.BorderBrush = Brushes.Red;
                        tmp += "Invalid Value For Minimum Age Of Child! (integer) \n";
                        min_AgeTextBox.Text = "";
                        count_ec++;
                    }
                }
                if (max_AgeTextBox.Text != null)
                {
                    if ((typeof(int) != (Convert.ToInt32(max_AgeTextBox.Text).GetType())))
                    {
                        max_AgeTextBox.BorderBrush = Brushes.Red;
                        tmp += "Invalid Value For Maximum Age Of Child! (integer) \n";
                        max_AgeTextBox.Text = "";
                        count_ec++;
                    }
                }
                if (experienceTextBox.Text != null)
                {
                    if ((typeof(int) != (Convert.ToInt32(experienceTextBox.Text).GetType())))
                    {
                        experienceTextBox.BorderBrush = Brushes.Red;
                        tmp += "Invalid Value For Experience Of Nanny! (integer) \n";
                        experienceTextBox.Text = "";
                        count_ec++;
                    }
                }
                if ((pay_For_HourTextBox.Text != null) && (pay_For_HourTextBox.Text != "0"))
                {
                    if (typeof(double) != ((double)(Convert.ToInt32(pay_For_HourTextBox.Text))).GetType())
                    {
                        pay_For_HourTextBox.BorderBrush = Brushes.Red;
                        tmp += "Invalid Pay! \n";
                        pay_For_HourTextBox.Text = "";
                        count_ec++;
                    }
                }
                if ((pay_For_MountTextBox.Text != null) && (pay_For_MountTextBox.Text != "0"))
                {
                    if (typeof(double) != ((double)(Convert.ToInt32(pay_For_MountTextBox.Text))).GetType())
                    {
                        pay_For_MountTextBox.BorderBrush = Brushes.Red;
                        tmp += "Invalid Pay! \n";
                        pay_For_MountTextBox.Text = "";
                        count_ec++;
                    }
                }

                //check if selected hours
                bool flag = false;
                foreach (var item in nanny.Work_Hours)
                {
                    if (item[0] != 0 && item[1] != 0)
                    {
                        flag = true;

                    }
                }
                if (!flag)
                {
                    tmp += "No hours selected \n";
                    count_ec++;
                }
                //check if hours(begin <end) ok
                flag = true;
                foreach (var item in nanny.Work_Hours)
                {
                    if (item[0] > item[1])
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
                    bl.Update_Ndetails(nanny);
                    MessageBox.Show("Update Nanny successful");
                    
                    nanny = new BE.Nanny();
                    this.DataContext = nanny;
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