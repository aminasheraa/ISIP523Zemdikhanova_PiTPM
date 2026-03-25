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

namespace ISIP523Zemdikhanova_PiTPM.Pages
{
    /// <summary>
    /// Логика взаимодействия для AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        public List<Ticket> Tickets = Core.Context.Ticket.Where(t => t.UserID == Core.CurrentUser.UserID).ToList();


        public AccountPage()
        {
            InitializeComponent();

            UsernameText.Text = Core.CurrentUser.Username;
            DateOfBirthText.Text = Core.CurrentUser.DateOfBirth.ToString();
            EmailText.Text = Core.CurrentUser.Email;
            TicketListBox.ItemsSource = Tickets;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
