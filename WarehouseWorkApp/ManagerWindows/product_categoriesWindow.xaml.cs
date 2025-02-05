using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WarehouseWorkApp.model;

namespace WarehouseWorkApp.ManagerWindows
{
    /// <summary>
    /// Логика взаимодействия для product_categoriesWindow.xaml
    /// </summary>
    public partial class product_categoriesWindow : Window
    {
        public int saveid;
        public int Category_id = 0;
        public product_categoriesWindow()
        {
            InitializeComponent();
            SQLClass.OpenConnection();
            CategoryView.ItemsSource = SQLClass.SelectProductCategories("SELECT category_id, category_name FROM public.product_categories;");
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
        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            List<Product_categories> product_Categories = SQLClass.SelectProductCategories("SELECT category_id, category_name FROM public.product_categories;");
            if (product_Categories != null)
            {
                foreach (var item in product_Categories)
                {
                    if (item.Category_name == Name_Category.Text)
                    {
                        MessageBox.Show("Вы не можете добавить данную категорию товара, так как она уже существует!", "Дублируемое значение!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }
            if (Name_Category.Text == "")
            {
                MessageBox.Show("Поле название не должно быть пустым!", "Пустое поле!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                SQLClass.Insert("INSERT INTO public.product_categories(category_name) VALUES ('" + Name_Category.Text + "');");
                CategoryView.ItemsSource = SQLClass.SelectProductCategories("SELECT category_id, category_name FROM public.product_categories;");
                Name_Category.Text = null;
                Category_id = 0;
                SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + saveid + "),'" + DateTime.Now + "'," + "'" + "Менеджер добавил категорию товара" + "')");
            }
        }
        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            List<Product_categories> product_Categories = SQLClass.SelectProductCategories("SELECT category_id, category_name FROM public.product_categories;");
            if (product_Categories != null)
            {
                foreach (var item in product_Categories)
                {
                    if (item.Category_name == Name_Category.Text)
                    {
                        MessageBox.Show("Вы не можете изменить данную категорию товара, так как она уже существует!", "Дублируемое значение!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }
            if (Name_Category.Text == "" || Category_id == 0)
            {
                MessageBox.Show("Поле название не должно быть пустым!", "Пустое поле!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                SQLClass.Insert("UPDATE public.product_categories SET category_name='" + Name_Category.Text + "' WHERE category_id= " + Category_id + ";");
                CategoryView.ItemsSource = SQLClass.SelectProductCategories("SELECT category_id, category_name FROM public.product_categories;");
                Name_Category.Text = null;
                Category_id = 0;
                SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + saveid + "),'" + DateTime.Now + "'," + "'" + "Менеджер изменил категорию товара" + "')");
            }
        }
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (Name_Category.Text == "" || Category_id == 0)
            {
                MessageBox.Show("Поле название не должно быть пустым!", "Пустое поле!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                SQLClass.Insert("DELETE FROM public.product_categories WHERE category_id= " + Category_id + ";");
                CategoryView.ItemsSource = SQLClass.SelectProductCategories("SELECT category_id, category_name FROM public.product_categories;");
                Name_Category.Text = null;
                Category_id = 0;
                SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + saveid + "),'" + DateTime.Now + "'," + "'" + "Менеджер удалил категорию товара" + "')");
            }
        }

        private void LogiView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product_categories product_Categories = CategoryView.SelectedItem as Product_categories;
            if (product_Categories != null)
            {
                Name_Category.Text = product_Categories.Category_name;
                Category_id = product_Categories.Category_id;
            }
        }
    }
}
