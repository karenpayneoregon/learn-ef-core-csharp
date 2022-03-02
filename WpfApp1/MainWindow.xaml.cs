using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataTable dt = new ();
            Datagrid1.ItemsSource = dt.AsDataView();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var table = Datagrid1.ToDataTable();
        }
    }

    public static class DataGridExtensions
    {
        public static DataTable ToDataTable(this DataGrid grid) 
            => DataViewAsDataTable((DataView)grid.ItemsSource);

        private static DataTable DataViewAsDataTable(DataView dataView)
        {
            DataTable table = dataView.Table.Clone();

            foreach (DataRowView drv in dataView)
            {
                table.ImportRow(drv.Row);
            }

            return table;
        }
    }
}
