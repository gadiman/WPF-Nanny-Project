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
    /// Interaction logic for NannyWindow.xaml
    /// </summary>
    public partial class NannyWindow : Window
    {
        public NannyWindow()
        {
         
            InitializeComponent();
        }

        private void AddNanny(object sender, RoutedEventArgs e)
        {
            new AddNannyWindow().Show();
        }

        private void RemoveNanny(object sender, RoutedEventArgs e)
        {
            new RemoveNannyWindow().Show();
        }

        private void UpdateNanny(object sender, RoutedEventArgs e)
        {
            new UpdateNannyWindow().Show();
          
        }

        private void ShowNanny(object sender, RoutedEventArgs e)
        {
            new ShowNannyWindow().Show();
        }
    }
}
