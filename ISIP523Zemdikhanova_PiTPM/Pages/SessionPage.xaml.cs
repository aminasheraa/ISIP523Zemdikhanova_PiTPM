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
using static System.Collections.Specialized.BitVector32;

namespace ISIP523Zemdikhanova_PiTPM.Pages
{
    /// <summary>
    /// Логика взаимодействия для SessionPage.xaml
    /// </summary>
    public partial class SessionPage : Page
    {
        private Session _currentSession;
        private Seat selectedSeat;
        private Films selectedFilm;
        private Room selectedRoom;
        private RoomCategory selectedRoomCategory;

        public SessionPage(Films film, Session session)
        {
            InitializeComponent();

            selectedFilm = film;
            _currentSession = session;
            selectedRoom = session.Room;
            selectedRoomCategory = session.Room.RoomCategory;

            LoadSeats();
        }


        private void LoadSeats(bool hideTaken = false)
        {
            var seatSessions = Core.Context.SeatSession.Where(ss => ss.SessionID == _currentSession.SessionID);

            if (HideTakenCheckBox.IsChecked == true)
            {
                seatSessions = seatSessions.Where(ss => (bool)!ss.IsTaken);
            }

            SeatListBox.ItemsSource = seatSessions.ToList();

        }

        private void HideTakenCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            LoadSeats(true);
        }
        private void HideTakenCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            LoadSeats(false);

        }

        private void SeatListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SeatListBox.SelectedItem is SeatSession seatSession)
            {
                if (seatSession.IsTaken == true)
                {
                    MessageBox.Show("Это место уже занято", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                selectedSeat = seatSession.Seat;

                MessageBox.Show($"Вы выбрали место {selectedSeat.Number}");
            }
        }


        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Order(selectedFilm, selectedRoom, _currentSession, selectedSeat, selectedRoomCategory));

        }


    }
}
