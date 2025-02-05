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
    /// Логика взаимодействия для LookProducts.xaml
    /// </summary>
    public partial class LookProducts : Window
    {
        public int idWork;
        public LookProducts()
        {
            InitializeComponent();
            SQLClass.OpenConnection();
            ProductView.ItemsSource = SQLClass.SelectProducts("SELECT product_id, product_name, description, quantity, price, category_id FROM public.products;");
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SQLClass.CloseConnection();
        }
        private void Exit(object sender, EventArgs e)
        {
            MainWarehouseWorkerWindow mainWarehouseWorkerWindow = new MainWarehouseWorkerWindow();
            mainWarehouseWorkerWindow.WorkerLogin.Text = LogiLogin.Text;
            mainWarehouseWorkerWindow.idWork = idWork;
            mainWarehouseWorkerWindow.Show();
            Close();
        }
    }
}
