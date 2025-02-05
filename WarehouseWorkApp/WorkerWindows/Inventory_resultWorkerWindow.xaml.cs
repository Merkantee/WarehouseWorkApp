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
using WarehouseWorkApp.model;

namespace WarehouseWorkApp.WorkerWindows
{
    /// <summary>
    /// Логика взаимодействия для Inventory_resultWorkerWindow.xaml
    /// </summary>
    public partial class Inventory_resultWorkerWindow : Window
    {
        public int FKUser=-1;
        public int FKProducts=-1;
        public int IDResult;
        public int idWork;
        public Inventory_resultWorkerWindow()
        {
            InitializeComponent();
            Workers.ItemsSource = SQLClass.SelectPassLogs("SELECT id, pgp_sym_decrypt(login::bytea, 'AES_KEY') as login, pgp_sym_decrypt(password::bytea, 'AES_KEY') as password, roles_fk, users_fk FROM passlog where roles_fk=(SELECT role_id FROM ROLES where role_name='Работник склада');");
            Workers.DisplayMemberPath = "Login";
            Workers.SelectedValue = "Id";
            Products.ItemsSource = SQLClass.SelectProducts("SELECT product_id, product_name, description, quantity, price, category_id FROM public.products;");
            Products.DisplayMemberPath = "Product_name";
            Products.SelectedValue = "Product_id";
            ListResult.ItemsSource = SQLClass.SelectInventoryResults("SELECT result_id, user_id, product_id, actual_quantity, check_time FROM public.inventory_results;");
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SQLClass.CloseConnection();
        }
        private void Exit(object sender, EventArgs e)
        {
            MainWarehouseWorkerWindow mainWarehouseWorkerWindow = new MainWarehouseWorkerWindow();
            mainWarehouseWorkerWindow.WorkerLogin.Text = WorkLogin.Text;
            mainWarehouseWorkerWindow.idWork = idWork;
            mainWarehouseWorkerWindow.Show();
            Close();
        }
        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (Workers.Text ==""|| Products.Text == "" || CountsQ.Text == "" || Picker.Text=="")
            {
                MessageBox.Show("Есть пустые поля!", "Наличие пустых полей", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                SQLClass.Insert("INSERT INTO public.inventory_results(user_id, product_id, actual_quantity, check_time) VALUES ("+FKUser+", "+FKProducts+", "+CountsQ.Text+", '"+Picker.Text+"');");
                Workers.Text = null;
                Products.Text = null;
                Picker.Text = null;
                CountsQ.Text = null;
                SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + idWork + "),'" + DateTime.Now + "'," + "'" + "Работник склада добавил инвентаризацию" + "')");
                ListResult.ItemsSource = SQLClass.SelectInventoryResults("SELECT result_id, user_id, product_id, actual_quantity, check_time FROM public.inventory_results;");
            }
        }
        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            if (Workers.Text == "" || Products.Text == "" || CountsQ.Text == "" || Picker.Text == "" || FKProducts == -1 || FKUser == -1)
            {
                MessageBox.Show("Есть пустые поля!", "Наличие пустых полей", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                SQLClass.Insert("UPDATE public.inventory_results SET user_id="+FKUser+", product_id="+FKProducts+", actual_quantity="+CountsQ.Text+", check_time='"+Picker.Text+"' WHERE result_id="+IDResult+";");
                Workers.Text = null;
                Products.Text = null;
                Picker.Text = null;
                CountsQ.Text = null;
                SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + idWork + "),'" + DateTime.Now + "'," + "'" + "Работник склада изменил инвентаризацию" + "')");
                ListResult.ItemsSource = SQLClass.SelectInventoryResults("SELECT result_id, user_id, product_id, actual_quantity, check_time FROM public.inventory_results;");
            }
        }
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (Workers.Text == "" || Products.Text == "" || CountsQ.Text == "" || Picker.Text == "" || FKProducts == -1 || FKUser == -1)
            {
                MessageBox.Show("Есть пустые поля!", "Наличие пустых полей", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                SQLClass.Insert("DELETE FROM public.inventory_results WHERE result_id="+IDResult+";");
                Workers.Text = null;
                Products.Text = null;
                Picker.Text = null;
                CountsQ.Text = null;
                SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + idWork + "),'" + DateTime.Now + "'," + "'" + "Работник склада удалил инвентаризацию" + "')");
                ListResult.ItemsSource = SQLClass.SelectInventoryResults("SELECT result_id, user_id, product_id, actual_quantity, check_time FROM public.inventory_results;");
            }
        }

        private void Workers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PassLog passLog = Workers.SelectedItem as PassLog;
            if (passLog != null)
            {
                Workers.Text = passLog.Login;
                FKUser = passLog.Id;
            }
        }

        private void Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model.Products products = Products.SelectedItem as model.Products;
            if (products != null)
            {
                Products.Text = products.Product_name;
                FKProducts = products.Product_id;
            }
        }

        private void ListProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List <PassLog> passLogs = SQLClass.SelectPassLogs("SELECT id, pgp_sym_decrypt(login::bytea, 'AES_KEY') as login, pgp_sym_decrypt(password::bytea, 'AES_KEY') as password, roles_fk, users_fk FROM passlog where roles_fk=(SELECT role_id FROM ROLES where role_name='Работник склада');");
            List<Products> products = SQLClass.SelectProducts("SELECT product_id, product_name, description, quantity, price, category_id FROM public.products;");
            Inventory_results inventory_Results = ListResult.SelectedItem as Inventory_results;
            if (inventory_Results != null)
            {
                CountsQ.Text = inventory_Results.Actual_quantity.ToString();
                Picker.Text = inventory_Results.Check_time.ToString();
                IDResult = inventory_Results.Result_id;
                foreach (var item in products)
                {
                    if (item.Product_id == inventory_Results.Product_id)
                    {
                        Products.Text = item.Product_name;
                        FKProducts = item.Product_id;
                    }
                }
                foreach (var item in passLogs)
                {
                    if (item.Id==inventory_Results.User_id)
                    {
                        Workers.Text = item.Login;
                        FKUser = item.Id;
                    }
                }
            }
        }
    }
}
