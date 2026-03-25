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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            LoadFilms();
        }

        private void LoadFilms()
        {
            FilmListBox.ItemsSource = Core.Context.Films.ToList();
        }

        private void SearchBox_Changed(object sender, TextChangedEventArgs e)
        {
            string search = SearchBox.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(search))
            {
                LoadFilms();
                return;
            }

            var searchedFilm = Core.Context.Films.Where(f => f.FilmName.ToLower().Contains(search)).ToList();

            FilmListBox.ItemsSource = searchedFilm;
        }

        private void Sort_Checked(object sender, RoutedEventArgs e)
        {
            var films = Core.Context.Films.ToList();

            if (SortByName.IsChecked == true)
            {
                films = films.OrderBy(f => f.FilmName).ToList();
            }
            else if (SortByRating.IsChecked == true)
            {
                films = films.OrderByDescending(f => f.Rating).ToList();
            }

            FilmListBox.ItemsSource = films.ToList();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistratPage());

        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Core.CurrentUser != null)
            {
                RegButton.Visibility = Visibility.Collapsed;
                AuthButton.Visibility = Visibility.Collapsed;
                LogOutButton.Visibility = Visibility.Visible;
                AccountButton.Visibility = Visibility.Visible;

            }
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            Core.CurrentUser = null;
            NavigationService.Navigate(new MainPage());
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorisPage());
        }

        private void FilmListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (FilmListBox.SelectedItem is Films selectedFilm)
            {
                NavigationService.Navigate(new FilmPage(selectedFilm));
            }
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AccountPage());

        }

        private void GenreComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GenreComboBox.SelectedItem == null)
                return;

            var selectedItem = GenreComboBox.SelectedItem as ComboBoxItem;
            if (selectedItem == null)
                return;

            string selectedGenreName = selectedItem.Content.ToString();

            var selectedGenre = Core.Context.Genre.FirstOrDefault(g => g.GenreName == selectedGenreName);

            if (selectedGenre == null)
            {
                LoadFilms();
                return;
            }

            var genreId = selectedGenre.GenreID;
            var films = Core.Context.Films.Where(f => f.FilmGenre.Any(fg => fg.GenreID == genreId)).ToList();


            if (FilmListBox != null && films != null)
            {
                FilmListBox.ItemsSource = films;
            }
        }
    }
}
