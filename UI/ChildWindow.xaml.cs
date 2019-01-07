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
    /// Interaction logic for ChildWindow.xaml
    /// </summary>
    public partial class ChildWindow : Window
    {
        public ChildWindow()
        {
      
            InitializeComponent();
        }

        private void AddChild(object sender, RoutedEventArgs e)
        {
            new AddChildWindow().ShowDialog();
        }

        private void UpdateChild(object sender, RoutedEventArgs e)
        {
            new UpdateChildWindow().ShowDialog();
        }

        private void RemoveChild(object sender, RoutedEventArgs e)
        {
            new RemoveChildWindow().ShowDialog();
        }

        private void ShowChild(object sender, RoutedEventArgs e)
        {
            new ShowChildWindow().ShowDialog();
        }
    }
}
