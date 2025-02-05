using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для SessionsWindow.xaml
    /// </summary>
    public partial class SessionsWindow : Window
    {
        public int saveid;
        public SessionsWindow()
        {
            InitializeComponent();
            SQLClass.OpenConnection();
            SessionView.ItemsSource = SQLClass.SelectSession("SELECT session_id, user_id, login_time, logout_time FROM public.user_sessions;");
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SQLClass.CloseConnection();
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            MainAdminWindow mainAdminWindow = new MainAdminWindow();
            mainAdminWindow.id = saveid;
            mainAdminWindow.NameAdmin.Text = SessionLogin.Text;
            mainAdminWindow.Show();
            Close();
        }
        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем диалог для сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                Title = "Сохранить файл"
            };

            // Если пользователь выбрал файл и нажал "Сохранить"
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Записываем текст из TextBox в файл
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        writer.WriteLine("Session_id; User_id; Login_time; Logout_time ");
                        foreach (var item in SessionView.ItemsSource)
                        {
                            var i = item as model.Session;
                            if (i != null)
                            {
                                writer.WriteLine($"{i.Session_id}; {i.User_id}; {i.Login_time}; {i.Logout_time} ");
                            }
                        }
                    }
                    MessageBox.Show("Данные успешно экспортированы!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении файла: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
