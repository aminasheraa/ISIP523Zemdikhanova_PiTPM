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
    /// Логика взаимодействия для AuthorisPage.xaml
    /// </summary>
    public partial class AuthorisPage : Page
    {
        public AuthorisPage()
        {
            InitializeComponent();
        }

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTB.Text) || string.IsNullOrWhiteSpace(PasswordTB.Text))
            {
                MessageBox.Show("Введите логин и пароль");
                return;
            }

            var user = Core.Context.User.FirstOrDefault(u => u.Username == LoginTB.Text && u.Password == PasswordTB.Text);

            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }

            Core.CurrentUser = user;

            MessageBox.Show("Успешный вход в аккаунт");
            NavigationService.Navigate(new MainPage());
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
