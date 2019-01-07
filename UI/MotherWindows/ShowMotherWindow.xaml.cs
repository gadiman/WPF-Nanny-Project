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
    /// Interaction logic for ShowMotherWindow.xaml
    /// </summary>
    public partial class ShowMotherWindow : Window
    {
        BL.IBL bl;
        BE.Mother mother;
        public ShowMotherWindow()
        {
            bl=BL.FactorySingletonBL.GetBL();
            InitializeComponent();
            dataGrid.ItemsSource = bl.Get_M_list();
            mother = new BE.Mother();
            DataContext = mother;
        }

        private void dataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (dataGrid.SelectedItem != null)
            {
                mother = new BE.Mother(dataGrid.SelectedItem as BE.Mother);
                DataContext = mother;

            }
        }
    }
}
