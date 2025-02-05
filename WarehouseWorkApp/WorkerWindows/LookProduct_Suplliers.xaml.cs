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

namespace WarehouseWorkApp.WorkerWindows
{
    /// <summary>
    /// Логика взаимодействия для LookProduct_Suplliers.xaml
    /// </summary>
    public partial class LookProduct_Suplliers : Window
    {
        public int idWork;
        public LookProduct_Suplliers()
        {
            InitializeComponent();
            SQLClass.OpenConnection();
            Product_SuplliersView.ItemsSource = SQLClass.SelectProductSupplier("SELECT supply_id, product_id, supplier_id, supply_quantity, supply_date FROM public.product_supplies;");
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SQLClass.CloseConnection();
        }
        private void Exit (object sender, EventArgs e)
        {
            MainWarehouseWorkerWindow mainWarehouseWorkerWindow = new MainWarehouseWorkerWindow();
            mainWarehouseWorkerWindow.WorkerLogin.Text = LogiLogin.Text;
            mainWarehouseWorkerWindow.idWork = idWork;
            mainWarehouseWorkerWindow.Show();
            Close();
        }

    }
}
