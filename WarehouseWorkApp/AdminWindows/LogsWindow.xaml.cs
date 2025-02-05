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
    /// Логика взаимодействия для LogsWindow.xaml
    /// </summary>
    public partial class LogsWindow : Window
    {
        public int saveid;
        public LogsWindow()
        {
            InitializeComponent();
            SQLClass.OpenConnection();
            LogiView.ItemsSource = SQLClass.SelectLogi("SELECT log_id, user_id, action_time, action_description FROM public.user_logs;");
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SQLClass.CloseConnection();
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            MainAdminWindow mainAdminWindow = new MainAdminWindow();
            mainAdminWindow.id = saveid;
            mainAdminWindow.NameAdmin.Text = LogiLogin.Text;
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
                        writer.WriteLine("log_id; user_id; action_time; action_description ");
                        foreach (var item in LogiView.ItemsSource)
                        {
                            var i = item as model.Logi;
                            if (i != null)
                            {
                                writer.WriteLine($"{i.Log_id}; {i.User_id}; {i.Action_time}; {i.Action_description} ");
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
