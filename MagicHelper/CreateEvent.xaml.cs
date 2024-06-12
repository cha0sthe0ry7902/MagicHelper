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
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Data.Entity.Validation;

namespace MagicHelper
{
    /// <summary>
    /// Interaction logic for CreateEvent.xaml
    /// </summary>
    public partial class CreateEvent : Page
    {
        MagicHelperDBEntities entities = new MagicHelperDBEntities();
        int OwnerId;
        public CreateEvent(int CurrentUserId)
        {
            InitializeComponent();
            OwnerId = CurrentUserId;
            foreach (var SweissType in entities.Sweisses)
            {
                CbSwissTypeEvent.Items.Add(SweissType);
            }
        }

        private void TxbCountPeole_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BtnCreateEvent_Click(object sender, RoutedEventArgs e)
        {
            var SelectedSweissType = CbSwissTypeEvent.SelectedItem as Sweiss;
            try
            {
                if (AppConnect.modelOdb.Events.Count(x => x.EventName == TxbNameEvent.Text) > 0)
                {
                    MessageBox.Show("Уже есть турнир с таким названием. Введите другое!", "Внимание!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                StringBuilder errors = new StringBuilder();

                if (string.IsNullOrWhiteSpace(TxbNameEvent.Text))
                    errors.AppendLine("Укажите название турнира!");
                if (string.IsNullOrWhiteSpace(TxbAdressEvent.Text))
                    errors.AppendLine("Укажите адрес!");
                if (string.IsNullOrWhiteSpace(CbSwissTypeEvent.Text))
                    errors.AppendLine("Выберите механизм распределения!");
                if (DPDateEvent.SelectedDate == null)
                    errors.AppendLine("Выберите дату проведения турнира!");
                if (Convert.ToInt32(TxbCountPeole.Text)>32 || Convert.ToInt32(TxbCountPeole.Text) < 4)
                    errors.AppendLine("Введите корректное количество участников - не более 32 и не менее 4!");

                if (errors.Length > 0)
                {
                    MessageBox.Show("Ошибка создания турнира!\n\n" + errors.ToString(), "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string InviteCode;
                var Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var StringChars = new char[7];
                Random random = new Random();
                do
                {
                    for (int i = 0; i < 7; i++)
                        StringChars[i] = Chars[random.Next(Chars.Length)];
                    InviteCode = new String(StringChars);
                }
                while (AppConnect.modelOdb.Events.Count(x => x.EventCode == InviteCode) > 0);

                Event EventObj = new Event();
                entities.Events.Add(EventObj);
                EventObj.EventName = TxbNameEvent.Text;
                EventObj.EventMaxGuests = Convert.ToInt32(TxbCountPeole.Text);
                EventObj.EventAdress = TxbAdressEvent.Text;
                EventObj.SweissId = SelectedSweissType.SweissId;
                EventObj.EventCode = InviteCode;
                EventObj.EventOwnerId = OwnerId;
                EventObj.UsersIdList = Convert.ToString(OwnerId);
                EventObj.EventDate = (DateTime)DPDateEvent.SelectedDate;
                entities.SaveChanges();
                MessageBox.Show("Турнир успешно создан!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.MainFrame.Navigate(new UserEvent(OwnerId, EventObj.EventId));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message + "! Обратитесь к системному администратору", "Ошибка!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
