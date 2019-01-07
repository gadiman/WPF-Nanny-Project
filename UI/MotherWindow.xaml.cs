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
    /// Interaction logic for MotherWindow.xaml
    /// </summary>
    public partial class MotherWindow : Window
    {
        public MotherWindow()
        {
       
        InitializeComponent();
        }

        private void AddMother(object sender, RoutedEventArgs e)
        {
            new AddMotherWindow().ShowDialog();
        }

        private void UpdateMother(object sender, RoutedEventArgs e)
        {
            new UpdateMotherWindow().ShowDialog();
        }

        private void RemoveMother(object sender, RoutedEventArgs e)
        {
            new RemoveMotherWindow().ShowDialog();
        }

        private void ShowMother(object sender, RoutedEventArgs e)
        {
            new ShowMotherWindow().ShowDialog();
        }
    }
}
