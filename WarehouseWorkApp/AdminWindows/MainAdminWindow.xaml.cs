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

namespace WarehouseWorkApp.AdminWindows
{
    /// <summary>
    /// Логика взаимодействия для MainAdminWindow.xaml
    /// </summary>
    public partial class MainAdminWindow : Window
    {
        public int id;
        public MainAdminWindow()
        {
            InitializeComponent();
            SQLClass.OpenConnection();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SQLClass.CloseConnection();
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            SQLClass.Insert("UPDATE public.user_sessions SET logout_time='" + DateTime.Now + "' WHERE EXISTS (SELECT 1 FROM passlog WHERE pgp_sym_decrypt(login::bytea, 'AES_KEY') = '" + NameAdmin.Text.Replace("Администратор ", "") + "') AND logout_time IS NULL;");
            Auth auth = new Auth();
            auth.Show();
            Close();
        }
        private void Go_to_Staffs(object sender, RoutedEventArgs e)
        {
            StaffsWindow staffs = new StaffsWindow();
            staffs.saveid = id;
            staffs.StaffsLogin.Text = NameAdmin.Text;
            staffs.Show();
            Close();
        }
        private void Go_to_Suppliers(object sender, RoutedEventArgs e)
        {
            SuppliersWindow suppliers = new SuppliersWindow();
            suppliers.saveid = id;
            suppliers.SuppliersLogin.Text = NameAdmin.Text;
            suppliers.Show();
            Close();
        }
        private void Go_to_Logs(object sender, RoutedEventArgs e)
        {
            LogsWindow logs = new LogsWindow();
            logs.saveid = id;
            logs.LogiLogin.Text = NameAdmin.Text;
            logs.Show();
            Close();
        }
        private void Go_to_sessions(object sender, RoutedEventArgs e)
        {
            SessionsWindow sessions = new SessionsWindow();
            sessions.saveid = id;
            sessions.SessionLogin.Text = NameAdmin.Text;
            sessions.Show();
            Close();
        }
    }
}
