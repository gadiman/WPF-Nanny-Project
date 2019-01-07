using System;
using System.Collections;
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
    /// Interaction logic for ShowNannyWindow.xaml
    /// </summary>
    public partial class ShowNannyWindow : Window
    {
        BL.IBL bl;
        BE.Nanny nanny;
        bool flag;
        ListCollectionView collection;

        public ShowNannyWindow()
        {
            bl = BL.FactorySingletonBL.GetBL();
            InitializeComponent();
            dataGrid.ItemsSource = bl.Get_N_list();
            nanny = new BE.Nanny();
            DataContext = nanny;
            collection = new ListCollectionView(bl.Get_N_list());

        }

        private void dataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (dataGrid.SelectedItem != null)
            {
                nanny = new BE.Nanny(dataGrid.SelectedItem as BE.Nanny);
                DataContext = nanny;

            }
        }

      
      


        private void groping_button_click(object sender, RoutedEventArgs e)
        {
          
            var list1 = bl.Grop_Nannys_By_Age(false, true);
            foreach (var it in list1)
            {
                if (it != null)
                {
                    foreach (var item in it)
                    {
                        string tmp = (it.Key * 6).ToString();
                        string tmp2 = ((it.Key * 6) - 6).ToString();
                        string tmp3 = tmp2 + "-" + tmp + " Months";
                        item.keygrop = tmp3.ToString();
                    }
                }
            }
            collection = new ListCollectionView(bl.Get_N_list());
            collection.GroupDescriptions.Add(new PropertyGroupDescription("keygrop"));
            dgData.ItemsSource = collection;
            button3.IsEnabled = true;
            flag = true;
        }

        private void show_all_nanny_click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = bl.Get_N_list();
        }

        private void show_nannys_who_take_ITL_vacations_click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = bl.Nannys_ITL();
        }

        private void groping_button_click1(object sender, RoutedEventArgs e)
        {
            
           
            
            var list1= bl.Grop_Nannys_By_Age();
            foreach (var it in list1)
            {
                if (it != null)
                {
                    foreach (var item in it)
                    {
                        string tmp = (it.Key * 6).ToString();
                        string tmp2 = ((it.Key * 6) - 6).ToString();
                        string tmp3 = tmp2 + "-" + tmp + " Months";
                        item.keygrop = tmp3.ToString();
                    }
                }
            }
            collection = new ListCollectionView(bl.Get_N_list());
            collection.GroupDescriptions.Add(new PropertyGroupDescription("keygrop"));
            dgData.ItemsSource = collection;
            button3.IsEnabled = true;
            flag = false;

        }

        private void sort_groping_click(object sender, RoutedEventArgs e)
        {
            if(flag)
            {
              
                var list1 = bl.Grop_Nannys_By_Age(true, true);
                foreach (var it in list1)
                {
                    if (it != null)
                    {
                        foreach (var item in it)
                        {
                            string tmp = (it.Key * 6).ToString();
                            string tmp2 = ((it.Key * 6) - 6).ToString();
                            string tmp3 = tmp2 + "-" + tmp + " Months";
                            item.keygrop = tmp3.ToString();
                        }
                    }
                }
                collection = new ListCollectionView(bl.Get_N_list());
                collection.GroupDescriptions.Add(new PropertyGroupDescription("keygrop"));
                dgData.ItemsSource = collection;
                
            }
            else
            {
                collection = new ListCollectionView(bl.Get_N_list());
                var list2 = bl.Grop_Nannys_By_Age(true);
                foreach (var it in list2)
                {
                    if (it != null)
                    {
                        foreach (var item in it)
                        {
                            string tmp = (it.Key * 6).ToString();
                            string tmp2 = ((it.Key * 6) - 6).ToString();
                            string tmp3 = tmp2 + "-" + tmp + " Months";
                            item.keygrop = tmp3.ToString();
                        }
                    }
                }

                collection.GroupDescriptions.Add(new PropertyGroupDescription("keygrop"));
                dgData.ItemsSource = collection;
            }
        }
    }
}