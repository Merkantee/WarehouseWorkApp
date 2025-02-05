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
    /// Логика взаимодействия для MainWarehouseWorkerWindow.xaml
    /// </summary>
    public partial class MainWarehouseWorkerWindow : Window
    {
        public int idWork;
        public MainWarehouseWorkerWindow()
        {
            InitializeComponent();
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            SQLClass.Insert("UPDATE public.user_sessions SET logout_time='" + DateTime.Now + "' WHERE EXISTS (SELECT 1 FROM passlog WHERE pgp_sym_decrypt(login::bytea, 'AES_KEY') = '" + WorkerLogin.Text.Replace("Работник склада ", "") + "') AND logout_time IS NULL;");
            Auth auth = new Auth();
            auth.Show();
            Close();
        }
        private void Go_to_ViewProducts(object sender, RoutedEventArgs e)
        {
            LookProducts lookProducts = new LookProducts();
            lookProducts.LogiLogin.Text = WorkerLogin.Text;
            lookProducts.idWork = idWork;
            lookProducts.Show();
            Close();
        }
        private void Go_to_ViewStoregRooms(object sender, RoutedEventArgs e)
        {
            LookRooms lookRooms = new LookRooms();
            lookRooms.LogiLogin.Text = WorkerLogin.Text;
            lookRooms.idWork = idWork;
            lookRooms.Show();
            Close();
        }
        private void Go_to_ViewProducts_Suplliers(object sender, RoutedEventArgs e)
        {
            LookProduct_Suplliers lookProduct_Suplliers = new LookProduct_Suplliers();
            lookProduct_Suplliers.LogiLogin.Text = WorkerLogin.Text;
            lookProduct_Suplliers.idWork = idWork;
            lookProduct_Suplliers.Show();
            Close();
        }
        private void Go_to_Inventory_result(object sender, RoutedEventArgs e)
        {
            Inventory_resultWorkerWindow inventory_ResultWorkerWindow = new Inventory_resultWorkerWindow();
            inventory_ResultWorkerWindow.WorkLogin.Text = WorkerLogin.Text;
            inventory_ResultWorkerWindow.idWork = idWork;
            inventory_ResultWorkerWindow.Show();
            Close();
        }
        private void Go_to_Product_location(object sender, RoutedEventArgs e)
        {
            Product_locationWindow product_LocationWindow = new Product_locationWindow();
            product_LocationWindow.WorkLogin.Text=WorkerLogin.Text;
            product_LocationWindow.idWork=idWork;
            product_LocationWindow.Show();
            Close();
        }
    }
}
