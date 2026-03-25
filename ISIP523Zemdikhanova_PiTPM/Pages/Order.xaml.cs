using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        private Films film;
        private Room room;
        private Session session;
        private Seat seat;
        private RoomCategory roomCategory;

        public Order(Films film, Room room, Session session, Seat seat, RoomCategory roomCategory)
        {
            InitializeComponent();

            this.film = film;
            this.room = room;
            this.session = session;
            this.seat = seat;
            this.roomCategory = roomCategory;

            OrderInfo();
        }

        private void OrderInfo()
        {
            FilmText.Text = $"Фильм: {film.FilmName}";
            RoomText.Text = $"Номер зала: {room.RoomID}";
            DateText.Text = $"Дата сеанса: {session.SessionDate:dd.MM.yyyy}";
            TimeText.Text = $"Время сеанса: {session.SessionTime}";
            SeatText.Text = $"Выбранное место: {seat.Number}";
            PriceText.Text = $"Цена: {roomCategory.Price}";
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Покупка успешно подтверждена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new MainPage());

            var ticket = new Ticket
            {
                UserID = Core.CurrentUser.UserID,
                SessionID = Core.Sessions.SessionID,
                SitID = seat.SeatID
            };
            Core.Context.Ticket.Add(ticket);
            Core.Context.SaveChanges();


        }
    }
}
