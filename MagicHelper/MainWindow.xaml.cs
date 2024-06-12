using MagicHelper.Classes;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppConnect.modelOdb = new Database.MagicHelperDBEntities();
        }

        private void BtnRegistration_Click(object sender, RoutedEventArgs e)
        {
            var window = new Register();
            window.Show();
            this.Close();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var UserObj = AppConnect.modelOdb.Users.FirstOrDefault(x => x.UserLogin == TxbxLogin.Text && x.UserPassword == PsbxPassword.Password);
                if (UserObj == null)
                {
                    MessageBox.Show("Неправильные логин и/или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    switch (UserObj.RoleId)
                    {
                        case 1:
                            MessageBox.Show("Добро пожаловать, организатор " + UserObj.UserLN + " " + UserObj.UserFN + "!", "Уведомление",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            var window = new UserPanel(UserObj.UserFN, UserObj.RoleId, UserObj.UserId);
                            this.Close();
                            window.ShowDialog();
                            break;

                        case 2:
                            MessageBox.Show("Добро пожаловать, игрок " + UserObj.UserLN + " " + UserObj.UserFN + "!", "Уведомление",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            window = new UserPanel(UserObj.UserFN, UserObj.RoleId, UserObj.UserId);
                            this.Close();
                            window.ShowDialog();
                            break;

                        default:
                            MessageBox.Show("Не удалось получить ваши данные. Обратитесь к системному администратору.", "Уведомление",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось подключиться к базе данных. Ошибка: " + ex.Message + "\n\nОбратитесь к системному администратору",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
