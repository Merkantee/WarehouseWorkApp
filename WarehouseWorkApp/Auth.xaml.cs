using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace WarehouseWorkApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Auth : Window
    {

        public Auth()
        {
            InitializeComponent();
            SQLClass.OpenConnection();
        }
        private void txtPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password.Length == 0)
                PasswordBox.Style = Resources["PasswordStyle"] as Style;
            else
                PasswordBox.Style = null;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SQLClass.CloseConnection();
        }

        private void Button_ClickAuth(object sender, RoutedEventArgs e)
        {
            /*SQLClass.Insert("INSERT INTO users(username, password, role_id)" +
                "VALUES('" + Login.Text + "'," +
                "'" + PasswordBox.Password + "'," +
                "1)"); Добавление в БД*/
            if(SQLClass.AuthUser("SELECT id, pgp_sym_decrypt(login::bytea, 'AES_KEY') as login, pgp_sym_decrypt(password::bytea, 'AES_KEY') as password, roles_fk FROM public.passlog", Login.Text, PasswordBox.Password) == null)
            {
                Close();
            }
            else
            {
                Login.Text = null;
                PasswordBox.Password = null;
            }

        }
    }

}
