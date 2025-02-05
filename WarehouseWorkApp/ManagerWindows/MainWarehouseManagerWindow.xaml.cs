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
namespace WarehouseWorkApp.ManagerWindows
{
    /// <summary>
    /// Логика взаимодействия для MainWarehouseManagerWindow.xaml
    /// </summary>
    public partial class MainWarehouseManagerWindow : Window
    {
        public int Mainid;
        public MainWarehouseManagerWindow()
        {
            InitializeComponent();
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            SQLClass.Insert("UPDATE public.user_sessions SET logout_time='" + DateTime.Now + "' WHERE EXISTS (SELECT 1 FROM passlog WHERE pgp_sym_decrypt(login::bytea, 'AES_KEY') = '" + NameManager.Text.Replace("Менеджер ", "") + "') AND logout_time IS NULL;");
            Auth auth = new Auth();
            auth.Show();
            Close();
        }
        private void Go_to_suplier(object sender, RoutedEventArgs e)
        {
            product_suplpiersWindow product_SuplpiersWindow = new product_suplpiersWindow();
            product_SuplpiersWindow.StaffsLogin.Text = NameManager.Text;
            product_SuplpiersWindow.saveid = Mainid;
            product_SuplpiersWindow.Show();
            Close();
        }
        private void Go_to_product(object sender, RoutedEventArgs e)
        {
            ProductsWindow productsWindow = new ProductsWindow();
            productsWindow.StaffsLogin.Text=NameManager.Text;
            productsWindow.saveid = Mainid;
            productsWindow.Show();
            Close();
        }
        private void Go_to_category_product(object sender, RoutedEventArgs e)
        {
            product_categoriesWindow product_CategoriesWindow = new product_categoriesWindow();
            product_CategoriesWindow.LogiLogin.Text = NameManager.Text;
            product_CategoriesWindow.saveid = Mainid;
            product_CategoriesWindow.Show();
            Close();
        }
        private void Go_to_storeg_room(object sender, RoutedEventArgs e)
        {
            storage_roomsWindow storage_RoomsWindow = new storage_roomsWindow();
            storage_RoomsWindow.LogiLogin.Text=NameManager.Text;
            storage_RoomsWindow.saveid = Mainid;
            storage_RoomsWindow.Show();
            Close();
        }
        private void Go_to_Results_inventory(object sender, RoutedEventArgs e)
        {
            Inventory_resultsWindow inventory_ResultsWindow = new Inventory_resultsWindow();
            inventory_ResultsWindow.LogiLogin.Text=NameManager.Text;
            inventory_ResultsWindow.saveid = Mainid;
            inventory_ResultsWindow.Show();
            Close();
        }
    }
}
