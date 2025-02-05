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
using WarehouseWorkApp.AdminWindows;
using WarehouseWorkApp.model;

namespace WarehouseWorkApp.ManagerWindows
{
    /// <summary>
    /// Логика взаимодействия для product_suplpiersWindow.xaml
    /// </summary>
    public partial class product_suplpiersWindow : Window
    {
        public int saveid;
        public int FK_Product=-1;
        public int FK_Suppliers=-1;
        public int ID_Spply=-1;
        public product_suplpiersWindow()
        {
            InitializeComponent();
            SQLClass.OpenConnection();
            Products.ItemsSource = SQLClass.SelectProducts("SELECT product_id, product_name, description, quantity, price, category_id FROM public.products;");
            Products.DisplayMemberPath = "Product_name";
            Products.SelectedValue = "Product_id";
            Suppliers.ItemsSource = SQLClass.SelectSuppliers("SELECT supplier_id, supplier_name, contact_info FROM public.suppliers;");
            Suppliers.DisplayMemberPath = "SupplierName";
            Suppliers.SelectedValue = "SupplierID";
            ListSupply.ItemsSource = SQLClass.SelectProductSupplier("SELECT supply_id, product_id, supplier_id, supply_quantity, supply_date FROM public.product_supplies;");
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SQLClass.CloseConnection();
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWarehouseManagerWindow mainWarehouseManagerWindow = new MainWarehouseManagerWindow();
            mainWarehouseManagerWindow.Mainid = saveid;
            mainWarehouseManagerWindow.NameManager.Text = StaffsLogin.Text;
            mainWarehouseManagerWindow.Show();
            Close();
        }
        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (Products.Text == "" || Counts.Text == "" || Suppliers.Text == "")
            {
                MessageBox.Show("Есть пустые поля!");
                return;
            }
            else
            {
                SQLClass.Insert("INSERT INTO public.product_supplies(product_id, supplier_id, supply_quantity, supply_date) VALUES ("+FK_Product+", "+FK_Suppliers+", "+Counts.Text+", '"+DateTime.Now+"');");
                Counts.Text = null;
                Products.Text=null;
                Suppliers.Text = null;
                FK_Product = -1;
                FK_Suppliers = -1;
                SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + saveid + "),'" + DateTime.Now + "'," + "'" + "Менеджер добавил поставку" + "')");
                ListSupply.ItemsSource = SQLClass.SelectProducts("SELECT product_id, product_name, description, quantity, price, category_id FROM public.products;");
            }
        }
        /*private void Button_Click_Update(object sender, RoutedEventArgs e) нет кнопки для изменения в макете word файле
        {

        }*/
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (Products.Text == "" || Counts.Text == "" || Suppliers.Text == "" || ID_Spply == -1)
            {
                MessageBox.Show("Есть пустые поля!");
                return;
            }
            else
            {
                SQLClass.Insert("DELETE FROM public.product_supplies WHERE supply_id=" + ID_Spply + ";");
                Counts.Text = null;
                Products.Text = null;
                Suppliers.Text = null;
                FK_Product = -1;
                FK_Suppliers = -1;
                SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + saveid + "),'" + DateTime.Now + "'," + "'" + "Менеджер удалил поставку" + "')");
                ListSupply.ItemsSource = SQLClass.SelectProducts("SELECT product_id, product_name, description, quantity, price, category_id FROM public.products;");
            }
        }

        private void ListSupply_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Products> products = SQLClass.SelectProducts("SELECT product_id, product_name, description, quantity, price, category_id FROM public.products;");
            List<Suppliers> suppliers = SQLClass.SelectSuppliers("SELECT supplier_id, supplier_name, contact_info FROM public.suppliers;");
            Product_Supplier product_Supplier = ListSupply.SelectedItem as Product_Supplier;
            if (product_Supplier != null)
            {
                ID_Spply = product_Supplier.Supply_id;
                Counts.Text = product_Supplier.Supply_quantity.ToString();
                foreach (var item in suppliers)
                {
                    if (product_Supplier.Supplier_id == item.SupplierID)
                    {
                        FK_Suppliers = item.SupplierID;
                        Suppliers.Text = item.SupplierName;
                    }
                }
                foreach (var item in products)
                {
                    if (product_Supplier.Product_id == item.Product_id)
                    {
                        FK_Product = item.Product_id;
                        Products.Text = item.Product_name;
                    }
                }
            }
        }

        private void Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Products products = Products.SelectedItem as Products;
            if (products != null)
            {
                FK_Product = products.Product_id;
            }
        }

        private void Suppliers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model.Suppliers suppliers = Suppliers.SelectedItem as Suppliers;
            if (suppliers != null)
            {
                FK_Suppliers = suppliers.SupplierID;
            }
        }
    }
}
