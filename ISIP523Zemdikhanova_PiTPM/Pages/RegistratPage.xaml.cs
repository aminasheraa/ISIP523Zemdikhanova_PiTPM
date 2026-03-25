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
    /// Логика взаимодействия для RegistratPage.xaml
    /// </summary>
    public partial class RegistratPage : Page
    {
        public RegistratPage()
        {
            InitializeComponent();
        }

        private void RegistBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTB.Text) || string.IsNullOrWhiteSpace(PasswordTB.Text) || string.IsNullOrWhiteSpace(EmailTB.Text))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            if (PasswordTB.Text.Length != 8)
            {
                MessageBox.Show("Пароль должен содержать 8 символов");
                return;
            }

            if (!EmailTB.Text.Contains("@"))
            {
                MessageBox.Show("Введите корректный адрес электронной почты");
                return;
            }

            if (DateBirthDP.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату рождения");
                return;
            }

            DateTime birthDate = DateBirthDP.SelectedDate.Value;
            if (birthDate > DateTime.Now)
            {
                MessageBox.Show("Дата рождения не может быть больше текущей");
                return;
            }

            var user = new User
            {
                Username = LoginTB.Text,
                Password = PasswordTB.Text,
                DateOfBirth = birthDate,
                Email = EmailTB.Text
            };

            Core.Context.User.Add(user);
            Core.Context.SaveChanges();
            Core.CurrentUser = user;
            MessageBox.Show("Успешная регистрация!");
            NavigationService.Navigate(new MainPage());


        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
