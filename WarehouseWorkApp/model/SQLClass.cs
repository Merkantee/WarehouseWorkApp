using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using WarehouseWorkApp.model;

namespace WarehouseWorkApp
{
    internal class SQLClass
    {
        public static NpgsqlConnection conn;

        public static void OpenConnection()
        {
            conn = new NpgsqlConnection
            {
                ConnectionString =
                    "Host=localhost;Username=postgres;Password=1212;Database=StorageMarket;ApplicationName=MyApp;SearchPath=public;TimeZone=UTC;Options='-c datestyle=DMY'"

            };
            conn.Open();
        }
        public static void CloseConnection()
        {
            conn.Close();
        }
        public static void Insert(String Text)
        {
            OpenConnection();
            NpgsqlCommand command = new NpgsqlCommand(Text, conn);
            command.ExecuteNonQuery();
            command.Dispose();
        }
        public static string AuthUser(String Text, String Login, String Password)
        {
            OpenConnection();
            NpgsqlCommand command = new NpgsqlCommand(Text, conn);
            command.Connection = conn;
            command.ExecuteNonQuery();
            command.Dispose();
            var dataAdapter = new NpgsqlDataAdapter(Text, conn);
            var table = new DataTable();
            dataAdapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["login"].ToString() == Login &&
                    table.Rows[i]["password"].ToString() == Password)
                {
                    switch (table.Rows[i]["roles_fk"])
                    {
                        case 1:
                            Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES (" + table.Rows[i]["id"] + ",'" + DateTime.Now + "'," + "'" + "Администратор авторизовался" + "')");
                            Insert("INSERT INTO public.user_sessions( user_id, login_time) VALUES (" + table.Rows[i]["id"] + ",'" + DateTime.Now + "')");
                            

                            AdminWindows.MainAdminWindow mainAdminWindow = new AdminWindows.MainAdminWindow();
                            mainAdminWindow.NameAdmin.Text = "Администратор " + Login;
                            mainAdminWindow.id = (int)table.Rows[i]["id"];
                            mainAdminWindow.Show();
                            return null;
                        case 2:
                            Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES (" + table.Rows[i]["id"] + ",'" + DateTime.Now + "'," + "'" + "Менеджер авторизовался" + "')");
                            Insert("INSERT INTO public.user_sessions( user_id, login_time) VALUES (" + table.Rows[i]["id"] + ",'" + DateTime.Now + "')");
                           
                            ManagerWindows.MainWarehouseManagerWindow mainWarehouseManagerWindow = new ManagerWindows.MainWarehouseManagerWindow();
                            mainWarehouseManagerWindow.NameManager.Text = "Менеджер " + Login;
                            mainWarehouseManagerWindow.Mainid = (int)table.Rows[i]["id"];
                            mainWarehouseManagerWindow.Show();
                            return null;
                        case 3:
                            Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES (" + table.Rows[i]["id"] + ",'" + DateTime.Now + "'," + "'" + "Работник склада авторизовался" + "')");
                            Insert("INSERT INTO public.user_sessions( user_id, login_time) VALUES (" + table.Rows[i]["id"] + ",'" + DateTime.Now + "')");
                            

                            WorkerWindows.MainWarehouseWorkerWindow mainWarehouseWorkerWindow = new WorkerWindows.MainWarehouseWorkerWindow();
                            mainWarehouseWorkerWindow.WorkerLogin.Text = "Работник склада " + Login;
                            mainWarehouseWorkerWindow.idWork = (int)table.Rows[i]["id"];
                            mainWarehouseWorkerWindow.Show();
                            return null;
                    }
                }
            }
            return MessageBox.Show("Неверный логин или пароль!\nПопробуйте снова.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error).ToString();
        }

        //Выводы
        public static List<Logi> SelectLogi(String Text) //Вывод логов
        {
            NpgsqlCommand command = new NpgsqlCommand(Text, conn);
            command.Connection = conn;
            command.ExecuteNonQuery();
            command.Dispose();
            var dataAdapter = new NpgsqlDataAdapter(Text, conn);
            var table = new DataTable();
            dataAdapter.Fill(table);
            List<Logi> log = new List<Logi>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                log.Add(new Logi((int)table.Rows[i]["log_id"], (int)table.Rows[i]["user_id"], (DateTime)table.Rows[i]["action_time"], table.Rows[i]["action_description"].ToString()));
            }
            return log;
        }
        public static List<Session> SelectSession(String Text)//Вывод сессий
        {
            NpgsqlCommand command = new NpgsqlCommand(Text, conn);
            command.Connection = conn;
            command.ExecuteNonQuery();
            command.Dispose();
            var dataAdapter = new NpgsqlDataAdapter(Text, conn);
            var table = new DataTable();
            dataAdapter.Fill(table);
            List<Session> sessions = new List<Session>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["logout_time"] == DBNull.Value)
                {
                    sessions.Add(new Session((int)table.Rows[i]["session_id"], (int)table.Rows[i]["user_id"], (DateTime)table.Rows[i]["login_time"]));
                }
                else
                {
                    sessions.Add(new Session((int)table.Rows[i]["session_id"], (int)table.Rows[i]["user_id"], (DateTime)table.Rows[i]["login_time"], (DateTime)(table.Rows[i]["logout_time"])));
                }
            }
            return sessions;
        }
        public static List<Suppliers> SelectSuppliers(String Text)
        {
            NpgsqlCommand command = new NpgsqlCommand(Text, conn);
            command.Connection = conn;
            command.ExecuteNonQuery();
            command.Dispose();
            var dataAdapter = new NpgsqlDataAdapter(Text, conn);
            var table = new DataTable();
            dataAdapter.Fill(table);
            List<Suppliers> suppliers = new List<Suppliers>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                suppliers.Add(new Suppliers((int)table.Rows[i]["supplier_id"], (string)table.Rows[i]["supplier_name"], (string)table.Rows[i]["contact_info"]));
            }
            return suppliers;
        }
        public static List<Roles> SelectRoles(String Text)
        {
            OpenConnection();
            NpgsqlCommand command = new NpgsqlCommand(Text, conn);
            command.Connection = conn;
            command.ExecuteNonQuery();
            command.Dispose();
            var dataAdapter = new NpgsqlDataAdapter(Text, conn);
            var table = new DataTable();
            dataAdapter.Fill(table);
            List<Roles> roles = new List<Roles>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                roles.Add(new Roles((int)table.Rows[i]["role_id"], (string)table.Rows[i]["role_name"]));
            }
            return roles;
        }
        public static List<PassLog> SelectPassLogs(String Text)
        {
            OpenConnection();
            NpgsqlCommand command = new NpgsqlCommand(Text, conn);
            command.Connection = conn;
            command.ExecuteNonQuery();
            command.Dispose();
            var dataAdapter = new NpgsqlDataAdapter(Text, conn);
            var table = new DataTable();
            dataAdapter.Fill(table);
            List<PassLog> passLogs = new List<PassLog>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                passLogs.Add(new PassLog((int)table.Rows[i]["id"], (string)table.Rows[i]["login"], (string)table.Rows[i]["password"], (int)table.Rows[i]["roles_fk"], (int)table.Rows[i]["users_fk"]));
            }
            return passLogs;
        }
        public static List<Users> SelectUsers(String Text)
        {
            OpenConnection();
            NpgsqlCommand command = new NpgsqlCommand(Text, conn);
            command.Connection = conn;
            command.ExecuteNonQuery();
            command.Dispose();
            var dataAdapter = new NpgsqlDataAdapter(Text, conn);
            var table = new DataTable();
            dataAdapter.Fill(table);
            List<Users> users = new List<Users>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                users.Add(new Users((int)table.Rows[i]["id"], (string)table.Rows[i]["name"], (string)table.Rows[i]["surname"], (string)table.Rows[i]["midelname"]));
            }
            return users;
        }
        public static List<Users> SelectUsersALL(String Text)
        {
            OpenConnection();
            NpgsqlCommand command = new NpgsqlCommand(Text, conn);
            command.Connection = conn;
            command.ExecuteNonQuery();
            command.Dispose();
            var dataAdapter = new NpgsqlDataAdapter(Text, conn);
            var table = new DataTable();
            dataAdapter.Fill(table);
            List<Users> users = new List<Users>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                users.Add(new Users((int)table.Rows[i]["id"], (string)table.Rows[i]["name"], (string)table.Rows[i]["surname"], (string)table.Rows[i]["midelname"], (int)table.Rows[i]["SeriaPasport"], (int)table.Rows[i]["NumberPasport"], (string)table.Rows[i]["SNILS"], (string)table.Rows[i]["Numberphone"], (string)table.Rows[i]["AddresLive"]));
            }
            return users;
        }
        public static List<Inventory_results> SelectInventoryResults(String Text)
        {
            OpenConnection();
            NpgsqlCommand command = new NpgsqlCommand(Text, conn);
            command.Connection = conn;
            command.ExecuteNonQuery();
            command.Dispose();
            var dataAdapter = new NpgsqlDataAdapter(Text, conn);
            var table = new DataTable();
            dataAdapter.Fill(table);
            List<Inventory_results> inventory_Results = new List<Inventory_results>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                inventory_Results.Add(new Inventory_results((int)table.Rows[i]["result_id"], (int)table.Rows[i]["user_id"], (int)table.Rows[i]["product_id"], (int)table.Rows[i]["actual_quantity"], (DateTime)table.Rows[i]["check_time"]));
            }
            return inventory_Results;
        }
        public static List<Product_categories> SelectProductCategories(String Text)
        {
            OpenConnection();
            NpgsqlCommand command = new NpgsqlCommand(Text, conn);
            command.Connection = conn;
            command.ExecuteNonQuery();
            command.Dispose();
            var dataAdapter = new NpgsqlDataAdapter(Text, conn);
            var table = new DataTable();
            dataAdapter.Fill(table);
            List<Product_categories> product_Categories = new List<Product_categories>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                product_Categories.Add(new Product_categories((int)table.Rows[i]["category_id"], (string)table.Rows[i]["category_name"]));
            }
            return product_Categories;
        }
        public static List<Storage_rooms> SelectStorageRooms(String Text)
        {
            OpenConnection();
            NpgsqlCommand command = new NpgsqlCommand(Text, conn);
            command.Connection = conn;
            command.ExecuteNonQuery();
            command.Dispose();
            var dataAdapter = new NpgsqlDataAdapter(Text, conn);
            var table = new DataTable();
            dataAdapter.Fill(table);
            List<Storage_rooms> storage_rooms = new List<Storage_rooms>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                storage_rooms.Add(new Storage_rooms((int)table.Rows[i]["room_id"], (string)table.Rows[i]["room_name"], (string)table.Rows[i]["Adress_room"]));
            }
            return storage_rooms;
        }
        public static List<Products> SelectProducts(String Text)
        {
            OpenConnection();
            NpgsqlCommand command = new NpgsqlCommand(Text, conn);
            command.Connection = conn;
            command.ExecuteNonQuery();
            command.Dispose();
            var dataAdapter = new NpgsqlDataAdapter(Text, conn);
            var table = new DataTable();
            dataAdapter.Fill(table);
            List<Products> products = new List<Products>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                products.Add(new Products((int)table.Rows[i]["product_id"], (string)table.Rows[i]["product_name"], (string)table.Rows[i]["description"], (int)table.Rows[i]["quantity"], (decimal)table.Rows[i]["price"], (int)table.Rows[i]["category_id"]));
            }
            return products;
        }
        public static List<Product_Supplier> SelectProductSupplier(String Text)
        {
            OpenConnection();
            NpgsqlCommand command = new NpgsqlCommand(Text, conn);
            command.Connection = conn;
            command.ExecuteNonQuery();
            command.Dispose();
            var dataAdapter = new NpgsqlDataAdapter(Text, conn);
            var table = new DataTable();
            dataAdapter.Fill(table);
            List<Product_Supplier> product_Suppliers = new List<Product_Supplier>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                product_Suppliers.Add(new Product_Supplier((int)table.Rows[i]["supply_id"], (int)table.Rows[i]["product_id"], (int)table.Rows[i]["supplier_id"], (int)table.Rows[i]["supply_quantity"], (DateTime)table.Rows[i]["supply_date"]));
            }
            return product_Suppliers;
        }
        public static List<Product_location> SelectProductLocation(String Text)
        {
            NpgsqlCommand command = new NpgsqlCommand(Text, conn);
            command.Connection = conn;
            command.ExecuteNonQuery();
            command.Dispose();
            var dataAdapter = new NpgsqlDataAdapter(Text, conn);
            var table = new DataTable();
            dataAdapter.Fill(table);
            List<Product_location> product_Locations = new List<Product_location>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                product_Locations.Add(new Product_location((int)table.Rows[i]["location_id"], (int)table.Rows[i]["product_id"], (int)table.Rows[i]["room_id"], (int)table.Rows[i]["quantity"]));
            }
            return product_Locations;
        }
    }
}
