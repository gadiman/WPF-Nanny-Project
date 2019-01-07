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
    /// Interaction logic for ShowChildWindow.xaml
    /// </summary>
    /// 
    
    public partial class ShowChildWindow : Window
    {
        ListCollectionView collection;
        BL.IBL bl;
        BE.Child child;
        public ShowChildWindow()
        {
            InitializeComponent();
            bl = BL.FactorySingletonBL.GetBL();
            child = new BE.Child();
           
        }

      

        private void selection_change_dataGrid(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                child = new BE.Child(dataGrid.SelectedItem as BE.Child);
                DataContext = child;

            }
        }

        private void show_chids_without_nannys_click(object sender, RoutedEventArgs e)
        {
            Visibility Hidden = Visibility.Visible;
            dataGrid.Visibility = Hidden;
          dataGrid.ItemsSource = bl.Childs_Without_Nanny();
            DataContext = null;
        }

        private void show_all_childs(object sender, RoutedEventArgs e)
        {
                 Visibility Hidden = Visibility.Visible;
            dataGrid.Visibility = Hidden;
           dataGrid.ItemsSource = bl.Get_Child_List();
        }

        private void groping_childs_click(object sender, RoutedEventArgs e)
        {
            Visibility Hidden = Visibility.Hidden;
            dataGrid.Visibility = Hidden;
            collection = new ListCollectionView(bl.Get_Child_List());
            collection.GroupDescriptions.Add(new PropertyGroupDescription("Id_Mom"));
            dgData.ItemsSource = collection;
        }

        private void dgData_selection_cehanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem != null)
            {
                child = new BE.Child(dgData.SelectedItem as BE.Child);
                DataContext = child;

            }
        }
    }
}
