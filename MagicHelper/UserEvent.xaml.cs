using MagicHelper.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for UserEvent.xaml
    /// </summary>
    public partial class UserEvent : Page
    {
        int CurrentUserId;
        int CurrentEventId;
        
        public UserEvent(int UserId, int EventId)
        {
            InitializeComponent();
            CurrentUserId = UserId;
            CurrentEventId = EventId;

            var UsersInEvent = AppConnect.modelOdb.Events.First(x => x.EventId == CurrentEventId);
            string UsersToFilter = UsersInEvent.UsersIdList;
            string FilterToFindUsers = @"\d+"; //шаблон для поиска юзеров
            MatchCollection matches = Regex.Matches(UsersToFilter, FilterToFindUsers);

            foreach (Match match in matches)
            {
                int ExactUserId = int.Parse(match.Value);
                var ExactUserInEvent = AppConnect.modelOdb.Users.First(x => x.UserId == ExactUserId);
                DgEventPlayers.Items.Add(ExactUserInEvent);
            }


            var CurrentEventObj = AppConnect.modelOdb.Events.FirstOrDefault(x => x.EventId == CurrentEventId);
            if (CurrentUserId != CurrentEventObj.EventOwnerId)
            {
                BtnEditEvent.IsEnabled = false; 
                BtnDeleteEvent.IsEnabled = false;
                BtnBeginEvent.IsEnabled = false;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEditEvent_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnBeginEvent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void AboutPlayer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void KickFromEvent_Click(object sender, RoutedEventArgs e)
        {
            var CurrentEventObj = AppConnect.modelOdb.Events.FirstOrDefault(x => x.EventId == CurrentEventId);
            if (CurrentUserId != CurrentEventObj.EventOwnerId)
            {
                MessageBox.Show("Вы - не организатор. Вы не можете исключать игроков из турнира", "Внимание!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void default_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
