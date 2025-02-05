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

namespace WarehouseWorkApp.AdminWindows
{
    /// <summary>
    /// Логика взаимодействия для SuppliersWindow.xaml
    /// </summary>
    public partial class SuppliersWindow : Window
    {
        int Suppliers_id=-1;
        public int saveid;
        public SuppliersWindow()
        {
            InitializeComponent();
            SQLClass.OpenConnection();
            ListSuppliers.ItemsSource = SQLClass.SelectSuppliers("SELECT supplier_id, supplier_name, contact_info FROM public.suppliers;");
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SQLClass.CloseConnection();
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            MainAdminWindow mainAdminWindow = new MainAdminWindow();
            mainAdminWindow.NameAdmin.Text = SuppliersLogin.Text;
            mainAdminWindow.id = saveid;
            mainAdminWindow.Show();
            Close();
        }
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            SQLClass.Insert("INSERT INTO public.suppliers(supplier_name, contact_info) VALUES ('" + Name_Supliers.Text + "','" + info.Text + "');");
            ListSuppliers.ItemsSource = SQLClass.SelectSuppliers("SELECT supplier_id, supplier_name, contact_info FROM public.suppliers;");
            Name_Supliers.Text = null;
            info.Text = null;
            SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + saveid + "),'" + DateTime.Now + "'," + "'" + "Администратор добавил поставщика" + "')");
        }
        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Suppliers_id == -1)
            {
                MessageBox.Show("Выберите запись, которую хотите изменить.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                SQLClass.Insert("UPDATE public.suppliers SET supplier_name='"+ Name_Supliers.Text +"', contact_info='"+ info.Text +"' WHERE supplier_id="+ Suppliers_id +";");
                Name_Supliers.Text = null;
                info.Text = null;
                Suppliers_id = -1;
                SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + saveid + "),'" + DateTime.Now + "'," + "'" + "Администратор изменил поставщика" + "')");
            }
            ListSuppliers.ItemsSource = SQLClass.SelectSuppliers("SELECT supplier_id, supplier_name, contact_info FROM public.suppliers;");
        }
        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Suppliers_id == -1)
            {
                MessageBox.Show("Выберите запись, которую хотите удалить.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                SQLClass.Insert("DELETE FROM public.suppliers WHERE supplier_id="+Suppliers_id+";");
                Name_Supliers.Text = null;
                info.Text = null;
                Suppliers_id = -1;
                SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + saveid + "),'" + DateTime.Now + "'," + "'" + "Администратор удалил поставщика" + "')");
            }
            ListSuppliers.ItemsSource = SQLClass.SelectSuppliers("SELECT supplier_id, supplier_name, contact_info FROM public.suppliers;");

        }

        private void ListSuppliers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Suppliers suppliers = ListSuppliers.SelectedItem as Suppliers;
            if (suppliers != null)
            {
                Name_Supliers.Text = suppliers.SupplierName;
                info.Text = suppliers.Contact_info;
                Suppliers_id = suppliers.SupplierID;
            }
        }
    }
}
