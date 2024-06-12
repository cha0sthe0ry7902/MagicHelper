using MagicHelper.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Interaction logic for JoinEvent.xaml
    /// </summary>
    public partial class JoinEvent : Page
    {
        string EnteredCode;
        int CurrentUserId;
        public JoinEvent(int UserId)
        {
            InitializeComponent();
            CurrentUserId = UserId;
        }

        private void BtnSendRequest_Click(object sender, RoutedEventArgs e)
        {
            EnteredCode = TxbEnterCode.Text;
            var SameCodeEvent = AppConnect.modelOdb.Events.FirstOrDefault(x => x.EventCode == EnteredCode);
            if (SameCodeEvent != null)
            {
                string StringCurrentUserId = Convert.ToString(CurrentUserId);

                //var ParticipatedEvent = AppConnect.modelOdb.Events.Where(x => x.UsersIdList == StringCurrentUserId);
                //if (ParticipatedEvent.Contains(SameCodeEvent))
                //{
                //    var ResultAlreadyJoined = MessageBox.Show("Вы уже участвуете в турнире " + SameCodeEvent.EventName + ". Желаете перейти на страницу турнира?",
                //        "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                //    if (ResultAlreadyJoined == MessageBoxResult.Yes)
                //    {
                //        AppFrame.MainFrame.Navigate(new UserEvent(CurrentUserId, SameCodeEvent.EventId));
                //    }
                //    else return;
                //}

                if (SameCodeEvent.UsersIdList.Contains("\b" + StringCurrentUserId + "\b"))
                {
                    var ResultAlreadyJoined = MessageBox.Show("Вы уже участвуете в турнире " + SameCodeEvent.EventName + ". Желаете перейти на страницу турнира?",
                        "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (ResultAlreadyJoined == MessageBoxResult.Yes)
                    {
                        AppFrame.MainFrame.Navigate(new UserEvent(CurrentUserId, SameCodeEvent.EventId));
                    }
                    else return;
                }

                var ResultJoin = MessageBox.Show("Вы успешно присоединились к турниру " + SameCodeEvent.EventName + ", желаете перейти на страницу турнира?",
                    "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (ResultJoin == MessageBoxResult.Yes)
                {
                    AppFrame.MainFrame.Navigate(new UserEvent(CurrentUserId, SameCodeEvent.EventId));
                }
            }
            else MessageBox.Show("Не обнаружено турниров. Перепроверьте правильность введёного кода!", "Внимание!",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
