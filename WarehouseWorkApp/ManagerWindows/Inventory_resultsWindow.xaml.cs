using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
using WarehouseWorkApp.AdminWindows;

namespace WarehouseWorkApp.ManagerWindows
{
    /// <summary>
    /// Логика взаимодействия для Inventory_resultsWindow.xaml
    /// </summary>
    public partial class Inventory_resultsWindow : Window
    {
        public int saveid;
        public Inventory_resultsWindow()
        {
            InitializeComponent();
            SQLClass.OpenConnection();
            LogiView.ItemsSource = SQLClass.SelectInventoryResults("SELECT result_id, user_id, product_id, actual_quantity, check_time FROM public.inventory_results;");
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SQLClass.CloseConnection();
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWarehouseManagerWindow mainWarehouseManagerWindow = new MainWarehouseManagerWindow();
            mainWarehouseManagerWindow.Mainid = saveid;
            mainWarehouseManagerWindow.NameManager.Text = LogiLogin.Text;
            mainWarehouseManagerWindow.Show();
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
                        writer.WriteLine("result_id; user_id; product_id; actual_quantity; check_time; ");
                        foreach (var item in LogiView.ItemsSource)
                        {
                            var i = item as model.Inventory_results;
                            if (i != null)
                            {
                                writer.WriteLine($"{i.Result_id}; {i.User_id}; {i.Product_id}; {i.Actual_quantity}; {i.Check_time} ");
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
