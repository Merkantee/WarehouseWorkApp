using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using WarehouseWorkApp.model;

namespace WarehouseWorkApp.AdminWindows
{
    /// <summary>
    /// Логика взаимодействия для StaffsWindow.xaml
    /// </summary>
    public partial class StaffsWindow : Window
    {
        public int idRole, idUser, idPasslog, saveid;
        public StaffsWindow()
        {
            InitializeComponent();
            SQLClass.OpenConnection();
            Roles.ItemsSource = SQLClass.SelectRoles("SELECT role_id, role_name FROM public.roles;");
            Roles.DisplayMemberPath = "Name";
            Roles.SelectedValue = "Id";
            ListUsers.ItemsSource = SQLClass.SelectUsersALL("SELECT * FROM public.users;");
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SQLClass.CloseConnection();
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            MainAdminWindow mainAdminWindow = new MainAdminWindow();
            mainAdminWindow.NameAdmin.Text = StaffsLogin.Text;
            mainAdminWindow.id = saveid;
            mainAdminWindow.Show();
            Close();
        }

        private void Button_Add_Staff(object sender, RoutedEventArgs e)
        {
            if (nameUser.Text == "" || surnameUser.Text == "" || midelUser.Text == "" || loginUser.Text == "" || passwordUser.Text == "" || Roles.Text == "" || idRole == 0 || SeriaPasport.Text == "" || NumberPhone.Text == "" || NumberPasport.Text == "" || SNILS.Text == "" || AdressLive.Text == "")
            {
                MessageBox.Show("Есть пустые поля!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (loginUser.Text.Length < 5)
            {
                MessageBox.Show("Минимальная длина логина 5 символов!", "Логин короткий!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (loginUser.Text.Contains(")") || loginUser.Text.Contains("("))
            {
                MessageBox.Show("Логин не должен содержать скобки!", "Логин содержит скобки!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Regex.IsMatch(loginUser.Text, @"[!#$?@№%^:;&*+<=>,`~|{}]"))
            {
                MessageBox.Show("В логине нельзя использовать такие спец символы! !#$?@№%^:;&*+<=>,`~|{}", "Наличие не тех спец символов!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (passwordUser.Text.Length < 8)
            {
                MessageBox.Show("Минимальная длина пароля 8 символов!", "Логин короткий!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(passwordUser.Text, @"\d"))
            {
                MessageBox.Show("В пароле нету ни одной цифры!", "Отсутствие цифры!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(passwordUser.Text, @"[-!#$?_/.]"))
            {
                MessageBox.Show("В пароле нету ни одного спец символа! !#$?./-_", "Отсутствие спец символа!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Regex.IsMatch(passwordUser.Text, @"[@№%^:;&*+<=>,`~|{}]"))
            {
                MessageBox.Show("В пароле нельзя использовать такие спец символы! @№%^:;&*+<=>,`~|{}", "Наличие не тех спец символов!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(passwordUser.Text, @"[a-zA-Z]"))
            {
                MessageBox.Show("В пароле нету латинских букв!", "Отсутствие латинских букв!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Regex.IsMatch(passwordUser.Text, @"[а-яА-Я]"))
            {
                MessageBox.Show("В пароле не должна быть кирилица!", "Наличие кирилицы!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (passwordUser.Text.Contains(")") || passwordUser.Text.Contains("("))
            {
                MessageBox.Show("Пароль не должен содержать скобки!", "Пароль содержит скобки!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SQLClass.Insert("INSERT INTO public.users(name, surname, midelname, SeriaPasport, NumberPasport, SNILS, Numberphone, AddresLive) VALUES ('"+nameUser.Text+"', '"+surnameUser.Text+"', '"+midelUser.Text+"', "+SeriaPasport.Text+", "+NumberPasport.Text+", '"+SNILS.Text+"','"+NumberPhone.Text+"' , '"+AdressLive.Text+"');");
            SQLClass.Insert("INSERT INTO public.passlog(login, password, roles_fk, users_fk) VALUES (pgp_sym_encrypt('" + loginUser.Text + "','AES_KEY'),pgp_sym_encrypt('" + passwordUser.Text + "','AES_KEY')," + idRole + ", (SELECT id FROM users ORDER BY id DESC LIMIT 1));");
            nameUser.Text = null;
            surnameUser.Text = null;
            midelUser.Text = null;
            loginUser.Text = null;
            passwordUser.Text = null;
            Roles.Text = null;
            SeriaPasport.Text = null;
            NumberPhone.Text = null;
            NumberPasport.Text = null;
            AdressLive.Text = null;
            SNILS.Text = null;
            idRole = 0;
            SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + saveid + "),'" + DateTime.Now + "'," + "'" + "Администратор добавил сотрудника" + "')");
            ListUsers.ItemsSource = SQLClass.SelectUsersALL("SELECT * FROM public.users;");
        }
        private void Button_Update_Staff(object sender, EventArgs e)
        {
            if (nameUser.Text == "" || surnameUser.Text == "" || midelUser.Text == "" || loginUser.Text == "" || passwordUser.Text == "" || Roles.Text == "" || idRole == 0 || SeriaPasport.Text == "" || NumberPhone.Text == "" || NumberPasport.Text == "" || SNILS.Text == "" || AdressLive.Text == "")
            {
                MessageBox.Show("Есть пустые поля!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (loginUser.Text.Length < 5)
            {
                MessageBox.Show("Минимальная длина логина 5 символов!", "Логин короткий!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (loginUser.Text.Contains(")") || loginUser.Text.Contains("("))
            {
                MessageBox.Show("Логин не должен содержать скобки!", "Логин содержит скобки!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Regex.IsMatch(loginUser.Text, @"[!#$?@№%^:;&*+<=>,`~|{}]"))
            {
                MessageBox.Show("В логине нельзя использовать такие спец символы! !#$?@№%^:;&*+<=>,`~|{}", "Наличие не тех спец символов!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (passwordUser.Text.Length < 8)
            {
                MessageBox.Show("Минимальная длина пароля 8 символов!", "Логин короткий!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(passwordUser.Text, @"\d"))
            {
                MessageBox.Show("В пароле нету ни одной цифры!", "Отсутствие цифры!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(passwordUser.Text, @"[-!#$?_/.]"))
            {
                MessageBox.Show("В пароле нету ни одного спец символа! !#$?./-_", "Отсутствие спец символа!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Regex.IsMatch(passwordUser.Text, @"[@№%^:;&*+<=>,`~|{}]"))
            {
                MessageBox.Show("В пароле нельзя использовать такие спец символы! @№%^:;&*+<=>,`~|{}", "Наличие не тех спец символов!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(passwordUser.Text, @"[a-zA-Z]"))
            {
                MessageBox.Show("В пароле нету латинских букв!", "Отсутствие латинских букв!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Regex.IsMatch(passwordUser.Text, @"[а-яА-Я]"))
            {
                MessageBox.Show("В пароле не должна быть кирилица!", "Наличие кирилицы!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (passwordUser.Text.Contains(")") || passwordUser.Text.Contains("("))
            {
                MessageBox.Show("Пароль не должен содержать скобки!", "Пароль содержит скобки!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SQLClass.Insert("UPDATE public.users SET name='"+nameUser.Text+"', surname='"+surnameUser.Text+"', midelname='"+midelUser.Text+"', seriapasport="+SeriaPasport.Text+", numberpasport="+NumberPasport.Text+", snils='"+SNILS.Text+"', numberphone='"+NumberPhone.Text+"', addreslive='"+AdressLive.Text+"' WHERE id="+idUser+";");
            SQLClass.Insert("UPDATE public.passlog SET login=pgp_sym_encrypt('" + loginUser.Text + "','AES_KEY'), password=pgp_sym_encrypt('" + passwordUser.Text + "','AES_KEY'), roles_fk='" + idRole + "' WHERE id=" + idPasslog + ";");
            nameUser.Text = null;
            surnameUser.Text = null;
            midelUser.Text = null;
            loginUser.Text = null;
            passwordUser.Text = null;
            Roles.Text = null;
            SeriaPasport.Text = null;
            NumberPhone.Text = null;
            NumberPasport.Text = null;
            AdressLive.Text = null;
            SNILS.Text = null;
            idRole = 0;
            SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + saveid + "),'" + DateTime.Now + "'," + "'" + "Администратор изменил сотрудника" + "')");
            ListUsers.ItemsSource = SQLClass.SelectUsersALL("SELECT * FROM public.users;");
        }
        private void Button_Delete_Staff(object sender, EventArgs e)
        {
            if (nameUser.Text == "" || surnameUser.Text == "" || midelUser.Text == "" || loginUser.Text == "" || passwordUser.Text == "" || Roles.Text == "" || idRole == 0 || SeriaPasport.Text == "" || NumberPhone.Text == "" || NumberPasport.Text == "" || SNILS.Text == "" || AdressLive.Text == "")
            {
                MessageBox.Show("Есть пустые поля!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                SQLClass.Insert("DELETE FROM public.passlog WHERE id=" + idPasslog + ";");
                SQLClass.Insert("DELETE FROM public.users WHERE id=" + idUser + ";");
                nameUser.Text = null;
                surnameUser.Text = null;
                midelUser.Text = null;
                loginUser.Text = null;
                passwordUser.Text = null;
                Roles.Text = null;
                SeriaPasport.Text = null;
                NumberPhone.Text = null;
                NumberPasport.Text = null;
                AdressLive.Text = null;
                SNILS.Text = null;
                idRole = 0;
                SQLClass.Insert("INSERT INTO public.user_logs( user_id, action_time, action_description ) VALUES ((SELECT id FROM passlog WHERE id=" + saveid + "),'" + DateTime.Now + "'," + "'" + "Администратор удалил сотрудника" + "')");
            }
            ListUsers.ItemsSource = SQLClass.SelectUsersALL("SELECT * FROM public.users;");
        }
        private void Roles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Roles roles = Roles.SelectedItem as Roles;
            if (roles != null)
            {
                idRole = roles.Id;
            }
        }

        private void ListUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Roles> roles = SQLClass.SelectRoles("SELECT role_id, role_name FROM public.roles;");
            List<PassLog> passLogs = SQLClass.SelectPassLogs("SELECT id, pgp_sym_decrypt(login::bytea, 'AES_KEY') as login, pgp_sym_decrypt(password::bytea, 'AES_KEY') as password, roles_fk, users_fk FROM public.passlog");
            Users users = ListUsers.SelectedItem as Users;
            if (users != null)
            {
                nameUser.Text = users.Name;
                surnameUser.Text = users.Surname;
                midelUser.Text = users.Midelname;
                SeriaPasport.Text = users.SeriaPasport.ToString();
                NumberPasport.Text = users.NumberPasport.ToString();
                SNILS.Text = users.SNILS;
                NumberPhone.Text = users.numberPhone;
                AdressLive.Text = users.AddresLive;

                foreach (var passLog in passLogs)
                {
                    if (users.Id == passLog.Users_fk)
                    {
                        loginUser.Text = passLog.Login;
                        passwordUser.Text = passLog.Password;
                        idPasslog = passLog.Id;
                        idUser = passLog.Users_fk;
                        foreach (var role in roles)
                        {
                            if (role.Id == passLog.Roles_fk)
                            {
                                Roles.Text = role.Name;
                                idRole = role.Id;
                            }
                        }
                    }
                }
            }
        }
    }
}
