using System;
using System.Collections.Generic;
using System.IO.Packaging;
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
    /// Логика взаимодействия для Product_locationWindow.xaml
    /// </summary>
    public partial class Product_locationWindow : Window
    {
        public int idWork;
        public int FKProduct=-1;
        public int FKRoom=-1;
        public int IDlocation;
        string nameroom="";
        string nameproduct="";
        
        List<Storage_rooms> storage_Rooms = SQLClass.SelectStorageRooms("SELECT * FROM public.storage_rooms;");
        List<Products> products = SQLClass.SelectProducts("SELECT * FROM public.products;");
        List<Product_location> product_Locations = SQLClass.SelectProductLocation("SELECT * FROM public.product_locations;");
        List<Product_location> Product_locations = new List<Product_location>();
        public Product_locationWindow()
        {
            InitializeComponent();
            SQLClass.OpenConnection();
            
            Rooms.ItemsSource = SQLClass.SelectStorageRooms("SELECT * FROM public.storage_rooms;");
            Rooms.DisplayMemberPath = "Room_name";
            Rooms.SelectedValue = "Room_id";
            Products.ItemsSource = SQLClass.SelectProducts("SELECT * FROM public.products;");
            Products.DisplayMemberPath = "Product_name";
            Products.SelectedValue = "Product_id";
            
            viewProductLocation();
        }
        public void viewProductLocation()
        {
            Product_locations.Clear();
            List<Storage_rooms> storage_Rooms = SQLClass.SelectStorageRooms("SELECT * FROM public.storage_rooms;");
            List<Products> products = SQLClass.SelectProducts("SELECT * FROM public.products;");
            List<Product_location> product_Locations = SQLClass.SelectProductLocation("SELECT * FROM public.product_locations;");
            foreach (var product_Location in product_Locations)
            {
                foreach (var storage_Room in storage_Rooms)
                {
                    if (product_Location.Id_room == storage_Room.Room_id)
                    {
                        nameroom = storage_Room.Room_name;
                        break;
                    }
                }
                foreach (var product in products)
                {
                    if (product_Location.Id_product == product.Product_id)
                    {
                        nameproduct = product.Product_name;
                        break;
                    }
                }
                Product_locations.Add(new Product_location(nameproduct, product_Location.Count, nameroom));
                nameproduct = "";
                nameroom = "";
                ListLocationProducts.ItemsSource = Product_locations;
            }
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
            if (CountsQ.Text ==""||Rooms.Text==""||Products.Text=="")
            {
                MessageBox.Show("Есть пустые поля!", "Наличие пустых полей", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                SQLClass.Insert("INSERT INTO public.product_locations(product_id, room_id, quantity) VALUES ("+FKProduct+", "+FKRoom+", "+CountsQ.Text+");");
                Rooms.Text = null;
                Products.Text = null;
                CountsQ.Text = null;
                FKRoom = -1;
                FKProduct = -1;
                SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + idWork + "),'" + DateTime.Now + "'," + "'" + "Работник склада добавил помещение товара" + "')");
                viewProductLocation();

            }
        }
        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            if (CountsQ.Text == "" || Rooms.Text == "" || Products.Text == "" || FKProduct==-1||FKRoom==-1)
            {
                MessageBox.Show("Есть пустые поля!", "Наличие пустых полей", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                SQLClass.Insert("UPDATE public.product_locations SET product_id="+FKProduct+", room_id="+FKRoom+", quantity="+CountsQ.Text+ " WHERE location_id="+IDlocation+";");
                Rooms.Text = null;
                Products.Text = null;
                CountsQ.Text = null;
                FKRoom = -1;
                FKProduct = -1;
                SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + idWork + "),'" + DateTime.Now + "'," + "'" + "Работник склада изменил помещение товара" + "')");
                viewProductLocation();

            }
        }
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (CountsQ.Text == "" || Rooms.Text == "" || Products.Text == "" || FKProduct == -1 || FKRoom == -1)
            {
                MessageBox.Show("Есть пустые поля!", "Наличие пустых полей", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                SQLClass.Insert("DELETE FROM public.product_locations WHERE location_id="+IDlocation+";");
                Rooms.Text = null;
                Products.Text = null;
                CountsQ.Text = null;
                FKRoom = -1;
                FKProduct = -1;
                SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + idWork + "),'" + DateTime.Now + "'," + "'" + "Работник склада удалил помещение товара" + "')");
                viewProductLocation();
            }
        }

        private void Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model.Products products = Products.SelectedItem as model.Products;
            if (products != null)
            {
                Products.Text = products.Product_name;
                FKProduct = products.Product_id;
            }
        }

        private void Rooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Storage_rooms storage_Rooms = Rooms.SelectedItem as Storage_rooms;
            if (storage_Rooms != null)
            {
                Rooms.Text = storage_Rooms.Room_name;
                FKRoom = storage_Rooms.Room_id;
            }
        }

        private void ListProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Products> products = SQLClass.SelectProducts("SELECT product_id, product_name, description, quantity, price, category_id FROM public.products;");
            List<Storage_rooms> storage_Rooms = SQLClass.SelectStorageRooms("SELECT * FROM public.storage_rooms;");
            Product_location product_Location = ListLocationProducts.SelectedItem as Product_location;
            if (product_Location != null)
            {
                CountsQ.Text = product_Location.Count.ToString();
                IDlocation = product_Location.Id_location;
                Products.Text = product_Location.ProductName;
                Rooms.Text = product_Location.RoomName;
                foreach (var item in product_Locations)
                {
                    if (item.Count.ToString() == CountsQ.Text)
                    {
                        IDlocation = item.Id_location;
                        break;
                    }
                }
                foreach (var item in products)
                {
                    if (item.Product_name == Products.Text)
                    {
                        FKProduct = item.Product_id;
                        break;
                    }
                }
                foreach (var item in storage_Rooms)
                {
                    if (item.Room_name == Rooms.Text)
                    {
                        FKRoom = item.Room_id;
                        break;
                    }
                }
            }
        }
    }
}
