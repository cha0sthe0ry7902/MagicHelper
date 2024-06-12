using MagicHelper.Classes;
using MagicHelper.Database;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MagicHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
            BtnRegister.IsEnabled = false;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TxbFN.Text))
                errors.AppendLine("Укажите ваше имя!");
            if (string.IsNullOrWhiteSpace(TxbLN.Text))
                errors.AppendLine("Укажите вашу фамилию!");
            if (string.IsNullOrWhiteSpace(TxbLogin.Text))
                errors.AppendLine("Введите логин!");
            if (string.IsNullOrWhiteSpace(TxbPassword.Text) || string.IsNullOrWhiteSpace(PsbPassword.Password))
                errors.AppendLine("Введите пароль!");
            if (TxbPassword.Text != PsbPassword.Password)
                errors.AppendLine("Пароли не совпадают!");

            if (errors.Length > 0)
            {
                MessageBox.Show("Ошибка создания профиля!\n\n" + errors.ToString(), "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (AppConnect.modelOdb.Users.Count(x => x.UserLogin == TxbLogin.Text) > 0)
            {
                MessageBox.Show("Такой логин уже есть! \nВведите другой логин.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else try
                {
                    User UserObj = new User()
                    {
                        UserLogin = TxbLogin.Text,
                        UserFN = TxbFN.Text,
                        UserLN = TxbLN.Text,
                        UserPassword = TxbPassword.Text,
                        RoleId = 4
                    };
                    AppConnect.modelOdb.Users.Add(UserObj);
                    AppConnect.modelOdb.SaveChanges();
                    MessageBox.Show("Регистрация завершена!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                    var window = new MainWindow();
                    window.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message + "! Обратитесь к системному администратору", "Ошибка!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
        }

        private void PsbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PsbPassword.Password == TxbPassword.Text)
            {
                BtnRegister.IsEnabled = true;
            }
            else
            {
                BtnRegister.IsEnabled = false;
            }
        }
    }
}
