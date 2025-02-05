using System;
using System.Collections.Generic;
using System.Windows;
using WarehouseWorkApp.model;

namespace WarehouseWorkApp.ManagerWindows
{
    /// <summary>
    /// Логика взаимодействия для storage_roomsWindow.xaml
    /// </summary>
    public partial class storage_roomsWindow : Window
    {
        public int saveid;
        public int roomid;
        public storage_roomsWindow()
        {
            InitializeComponent();
            SQLClass.OpenConnection();
            ViewRoomS.ItemsSource = SQLClass.SelectStorageRooms("SELECT * FROM public.storage_rooms;");
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
            List<Storage_rooms> storage_Rooms = SQLClass.SelectStorageRooms("SELECT * FROM public.storage_rooms;");
            if (storage_Rooms != null)
            {
                foreach (var item in storage_Rooms)
                {
                    if (item.Room_name == Number_Rooms.Text)
                    {
                        MessageBox.Show("Вы не можете добавить данное помещение, так как оно уже существует!", "Дублируемое значение!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }
            if (Number_Rooms.Text == "")
            {
                MessageBox.Show("Поле номер помещения не должно быть пустым!", "Пустое поле!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                SQLClass.Insert("INSERT INTO public.storage_rooms(room_name) VALUES ('" + Number_Rooms.Text + "');");
                ViewRoomS.ItemsSource = SQLClass.SelectStorageRooms("SELECT * FROM public.storage_rooms;");
                Number_Rooms.Text = null;
                roomid = 0;
                SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + saveid + "),'" + DateTime.Now + "'," + "'" + "Менеджер добавил номер помещения" + "')");
            }
        }
        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            List<Storage_rooms> storage_Rooms = SQLClass.SelectStorageRooms("SELECT * FROM public.storage_rooms;");
            if (storage_Rooms != null)
            {
                foreach (var item in storage_Rooms)
                {
                    if (item.Room_name == Number_Rooms.Text)
                    {
                        MessageBox.Show("Вы не можете изменить данное помещение, так как оно уже существует!", "Дублируемое значение!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }
            if (Number_Rooms.Text == "")
            {
                MessageBox.Show("Поле номер помещения не должно быть пустым!", "Пустое поле!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                SQLClass.Insert("UPDATE public.storage_rooms SET room_name='"+Number_Rooms.Text+ "' WHERE room_id=" + roomid+";");
                ViewRoomS.ItemsSource = SQLClass.SelectStorageRooms("SELECT * FROM public.storage_rooms;");
                Number_Rooms.Text = null;
                roomid = 0;
                SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + saveid + "),'" + DateTime.Now + "'," + "'" + "Менеджер изменил номер помещения" + "')");
            }
        }
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (Number_Rooms.Text == "")
            {
                MessageBox.Show("Поле номер помещения не должно быть пустым!", "Пустое поле!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                SQLClass.Insert("DELETE FROM public.storage_rooms WHERE room_id="+roomid+";");
                ViewRoomS.ItemsSource = SQLClass.SelectStorageRooms("SELECT * FROM public.storage_rooms;");
                Number_Rooms.Text = null;
                roomid = 0;
                SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + saveid + "),'" + DateTime.Now + "'," + "'" + "Менеджер удалил номер помещения" + "')");
            }
        }

        private void ViewRoomS_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Storage_rooms storage_Rooms = ViewRoomS.SelectedItem as Storage_rooms;
            if (storage_Rooms != null)
            {
                Number_Rooms.Text = storage_Rooms.Room_name;
                roomid = storage_Rooms.Room_id;
            }
        }
    }
}
