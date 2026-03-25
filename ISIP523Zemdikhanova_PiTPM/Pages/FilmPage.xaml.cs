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

namespace ISIP523Zemdikhanova_PiTPM.Pages
{
    /// <summary>
    /// Логика взаимодействия для FilmPage.xaml
    /// </summary>
    public partial class FilmPage : Page
    {
        public FilmPage(Films film)
        {
            InitializeComponent();
            DataContext = film;

            SessionListBox.ItemsSource = Core.Context.Session.Where(s => s.FilmID == film.FilmID).ToList();
        }

        private void SessionListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SessionListBox.SelectedItem is Session selectedSession)
            {
                var film = DataContext as Films;
                Core.Sessions = selectedSession;
                NavigationService.Navigate(new SessionPage(film, selectedSession));
            }
            else
            {
                MessageBox.Show("Зарегистрируйтесь или войдите в свой аккаунт");
                NavigationService.Navigate(new MainPage());
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());

        }
    }
}
