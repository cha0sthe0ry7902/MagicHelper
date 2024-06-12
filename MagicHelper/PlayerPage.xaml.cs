using MagicHelper.Classes;
using MagicHelper.Database;
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
    /// Interaction logic for PlayerPage.xaml
    /// </summary>
    public partial class PlayerPage : Page
    {
        int CurrentRoleId;
        int CurrentUserId;
        MagicHelperDBEntities entities = new MagicHelperDBEntities();
        List<Event> UserInEventsList = new List<Event>();
        public PlayerPage(int RoleId, int UserId)
        {
            InitializeComponent();
            CurrentRoleId = RoleId;
            CurrentUserId = UserId;
            if (RoleId == 1)
            {
                CreateEvent.IsEnabled = true;
            }
            else CreateEvent.IsEnabled = false;

            string StringCurrentUserId;
            StringCurrentUserId = Convert.ToString(CurrentUserId);

            //Regex EventFilter = new Regex(@"\b" + StringCurrentUserId + @"\b");

            foreach (var ParticipatedEvent in AppConnect.modelOdb.Events.Where(x => x.UsersIdList == StringCurrentUserId))
            {
                DgMyEvents.Items.Add(ParticipatedEvent);
            }

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void JoinEvent_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new JoinEvent(CurrentUserId));
        }

        private void CreateEvent_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new CreateEvent(CurrentUserId));
        }

        private void About_MagicHelper_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AboutEvent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CopyCodeEvent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LeftEvent_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
