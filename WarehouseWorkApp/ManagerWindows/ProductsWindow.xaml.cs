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
    /// Логика взаимодействия для ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        public int saveid;
        public int Category_fk;
        public int idProduct=-1;
        public ProductsWindow()
        {
            InitializeComponent();
            SQLClass.OpenConnection();
            Category_products.ItemsSource = SQLClass.SelectProductCategories("SELECT category_id, category_name FROM public.product_categories;");
            Category_products.DisplayMemberPath = "Category_name";
            Category_products.SelectedValue = "Category_id";
            // Создаем список для хранения объединенных данных
            List<Product_Plus_Category> product_Plus_s = new List<Product_Plus_Category>();

            // Получаем списки товаров и категорий
            List<Products> products = SQLClass.SelectProducts("SELECT product_id, product_name, description, quantity, price, category_id FROM public.products;");
            List<Product_categories> product_Categories = SQLClass.SelectProductCategories("SELECT category_id, category_name FROM public.product_categories;");

            // Объединяем товары и категории
            foreach (var product in products)
            {
                foreach (var product_Categorie in product_Categories)
                {
                    if (product.Product_category == product_Categorie.Category_id)
                    {
                        // Создаем новый объект Product_Plus_Category и добавляем его в список
                        Product_Plus_Category product_Plus_Category = new Product_Plus_Category(product_Categorie.Category_name, product.Product_name, product.Product_description, product.Quantity, product.Product_price);
                        product_Plus_s.Add(product_Plus_Category);
                        break; // Выходим из внутреннего цикла, если нашли соответствующую категорию
                    }
                }
            }

            // Устанавливаем объединенный список как источник данных для ListProducts
            ListProducts.ItemsSource = product_Plus_s;
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
            if(nameProduct.Text == "" || descriptionProduct.Text == "" || Category_products.Text == "" || Counts.Text == "" || Price.Text == "")
            {
                MessageBox.Show("Есть пустые поля!");
                return;
            }
            else
            {
                SQLClass.Insert("INSERT INTO public.products(product_name, description, quantity, price, category_id) VALUES ('"+nameProduct.Text+"','"+ descriptionProduct.Text +"',"+ Counts.Text+","+Price.Text+", "+Category_fk+");");
                nameProduct.Text = null;
                descriptionProduct.Text = null;
                Category_products.Text = null;
                Price.Text = null;
                Counts.Text = null;
                Category_fk = -1;
                SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + saveid + "),'" + DateTime.Now + "'," + "'" + "Менеджер добавил продукт" + "')");
                ListProducts.ItemsSource = SQLClass.SelectProducts("SELECT product_id, product_name, description, quantity, price, category_id FROM public.products;");
            }
        }
        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            if (nameProduct.Text == "" || descriptionProduct.Text == "" || Category_products.Text == "" || Counts.Text == "" || Price.Text == "" || idProduct == -1)
            {
                MessageBox.Show("Есть пустые поля!");
                return;
            }
            else
            {
                SQLClass.Insert("UPDATE public.products SET product_name='"+nameProduct.Text+"', description='"+descriptionProduct.Text+"', quantity="+Counts.Text+", price="+Price.Text.Replace(",",".")+", category_id="+Category_fk+" WHERE product_id="+ idProduct + ";");
                nameProduct.Text = null;
                descriptionProduct.Text = null;
                Category_products.Text = null;
                Price.Text = null;
                Counts.Text = null;
                Category_fk = -1;
                idProduct = -1;
                SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + saveid + "),'" + DateTime.Now + "'," + "'" + "Менеджер изменил продукт" + "')");
                ListProducts.ItemsSource = SQLClass.SelectProducts("SELECT product_id, product_name, description, quantity, price, category_id FROM public.products;");
            }
        }
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (nameProduct.Text == "" || descriptionProduct.Text == "" || Category_products.Text == "" || Counts.Text == "" || Price.Text == "" || idProduct == -1)
            {
                MessageBox.Show("Есть пустые поля!");
                return;
            }
            else
            {
                SQLClass.Insert("DELETE FROM public.products WHERE product_id="+ idProduct + ";");
                nameProduct.Text = null;
                descriptionProduct.Text = null;
                Category_products.Text = null;
                Price.Text = null;
                Counts.Text = null;
                Category_fk = -1;
                idProduct = -1;
                SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + saveid + "),'" + DateTime.Now + "'," + "'" + "Менеджер удалил продукт" + "')");
                ListProducts.ItemsSource = SQLClass.SelectProducts("SELECT product_id, product_name, description, quantity, price, category_id FROM public.products;");
            }
        }

        private void Category_products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product_categories product_Categories = Category_products.SelectedItem as Product_categories;
            if (product_Categories != null)
            {
                Category_fk = product_Categories.Category_id;
            }
        }

        private void ListProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Product_categories> CategotyProducts = SQLClass.SelectProductCategories("SELECT category_id, category_name FROM public.product_categories;");
            Products product = ListProducts.SelectedItem as Products;
            if (product != null)
            {
                nameProduct.Text = product.Product_name;
                descriptionProduct.Text = product.Product_description;
                Price.Text = product.Product_price.ToString();
                Counts.Text = product.Quantity.ToString();
                idProduct = product.Product_id;
                foreach (var item in CategotyProducts)
                {
                    if (product.Product_category == item.Category_id)
                    {
                        Category_products.Text = item.Category_name;
                        Category_fk = item.Category_id;
                    }
                }
            }
        }
    }
}
