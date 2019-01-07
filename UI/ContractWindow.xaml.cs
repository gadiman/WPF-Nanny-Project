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
    /// Interaction logic for ContractWindow.xaml
    /// </summary>
    public partial class ContractWindow : Window
    {
        public ContractWindow()
        {
        
            InitializeComponent();
        }

        private void AddContract(object sender, RoutedEventArgs e)
        {
            new AddContractWindow().ShowDialog();
        }

        private void UpdateContract(object sender, RoutedEventArgs e)
        {
            new UpdateContractWindow().ShowDialog();
        }

        private void RemoveContract(object sender, RoutedEventArgs e)
        {
            new RemoveContractWindow().ShowDialog();
        }

        private void ShowContract(object sender, RoutedEventArgs e)
        {
            new ShowContractWindow().ShowDialog();
        }
    }
}
