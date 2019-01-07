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
    /// Interaction logic for AddChildWindow.xaml
    /// </summary>
    public partial class AddChildWindow : Window
    {
        BL.IBL bl;
        BE.Child child;

        public AddChildWindow()
        {
            InitializeComponent();

            
            child = new BE.Child() { Date= DateTime.Now};
            this.DataContext = child;
            bl = BL.FactorySingletonBL.GetBL();
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.sComboBox.ItemsSource = Enum.GetValues(typeof(BE.With_Special_Needs));
            this.id_MomComboBox.ItemsSource = bl.Get_M_list();
            this.id_MomComboBox.DisplayMemberPath = "ID";
            this.id_MomComboBox.SelectedValuePath = "ID";
            child.Special_Needs = "    ";//for triger
        }

        private void AddChildClick(object sender, RoutedEventArgs e)
        {
           
            string tmp ="";
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

                if ((DateTime)dateDatePicker.SelectedDate >= (DateTime.Today))
                {
                    tmp += "Invalid Date!";
                    count_ec++;
                }

                bool check = iDTextBox.Text.Any(who => !char.IsLetter(who));
                if ((iDTextBox.Text != null) && (iDTextBox.Text!= "0"))
                {
                    if (!check)
                    {
                        iDTextBox.BorderBrush = Brushes.Red;
                        tmp += "Invalid ID Number! \n";
                        iDTextBox.Text = "";
                        count_ec++;  
                    }
                }

                if (count_ec != 0)
                    throw new Exception(tmp);

                else
                {
                  
                    bl.Add_Chaild(child);
                    MessageBox.Show("Add child successful");
                    child = new BE.Child();
                    this.DataContext = child;
                    Close();
                }
            }
            catch (Exception ex)
            {
               
                MessageBox.Show(ex.Message);
                first_NameTextBox.BorderBrush = Brushes.Black;
                iDTextBox.BorderBrush = Brushes.Black;
            }
        }

        private void Selection_Changed_IDmom_ComboBox(object sender, SelectionChangedEventArgs e)
        {
            if (id_MomComboBox.SelectedItem != null)
            {
                BE.Mother mom = new BE.Mother(id_MomComboBox.SelectedItem as BE.Mother);

                child.Id_Mom = mom.ID;
            }
        }

        private void sComboBox_Selcetion_changed(object sender, SelectionChangedEventArgs e)
        {
            if (sComboBox.SelectedItem != null)
            {
                BE.With_Special_Needs tmp = new BE.With_Special_Needs();
                tmp = BE.With_Special_Needs.NOT_NEED;
                if ((BE.With_Special_Needs)sComboBox.SelectedItem == tmp)
                    child.Special_Needs = "    ";
            }
        }
    }
}

