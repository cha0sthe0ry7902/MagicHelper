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
using System.Windows.Shapes;

namespace MagicHelper
{
    /// <summary>
    /// Interaction logic for UserPanel.xaml
    /// </summary>
    public partial class UserPanel : Window
    {
        MagicHelperDBEntities entities = new MagicHelperDBEntities();
        int CurrentRoleId;
        int CurrentUserId;

        public UserPanel(string UserFN, int RoleId, int UserId)
        {
            InitializeComponent();
            CurrentRoleId = RoleId;
            CurrentUserId = UserId;
            switch (RoleId) 
            {
                case 1:
                    LblWelcome.Content = "Добро пожаловать, организатор " + UserFN + "!";
                    break;
                case 2:
                    LblWelcome.Content = "Добро пожаловать, игрок " + UserFN + "!";
                    break;
            }
            AppFrame.MainFrame = FrMainFrame;
            AppFrame.MainFrame.Navigate(new PlayerPage(CurrentRoleId, CurrentUserId));
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.GoBack();
        }

        private void BtnBackToMain_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PlayerPage(CurrentRoleId, CurrentUserId));
        }

        private void FrMainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (AppFrame.MainFrame.CanGoBack)
            {
                BtnBack.Visibility = Visibility.Visible;
            }
            else
            {
                BtnBack.Visibility = Visibility.Hidden;
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            var Result = MessageBox.Show("Вы уверены, что хотите выйти из вашего профиля?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Result == MessageBoxResult.Yes)
            {
                var window = new MainWindow();
                window.Show();
                this.Close();
            }
        }
    }
}
