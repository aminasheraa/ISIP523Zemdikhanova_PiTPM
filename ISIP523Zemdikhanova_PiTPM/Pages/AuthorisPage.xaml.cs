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

        /// <summary>
        /// Выполняет авторизацию пользователя по логину и паролю.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>
        /// Возвращает <c>true</c>, если авторизация прошла успешно;
        /// иначе <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Метод выполняет следующие проверки:
        /// <list type="bullet">
        /// <item><description>Проверка на пустые значения логина и пароля.</description></item>
        /// <item><description>Проверка длины пароля (от 8 до 32 символов).</description></item>
        /// <item><description>Поиск пользователя в базе данных по логину (без учета регистра).</description></item>
        /// <item><description>Проверка соответствия пароля (с учетом регистра).</description></item>
        /// </remarks>
        public bool Auth(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль");
                return false;
            }

            if (password.Length < 8 || password.Length > 32)
            {
                MessageBox.Show("Пароль должен быть от 8 до 32 символов");
                return false;
            }

            var user = Core.Context.User.FirstOrDefault(u => u.Username.Trim().ToLower() == login.Trim().ToLower());

            if (user == null || user.Password != password)
            {
                MessageBox.Show("Неверный логин или пароль");
                return false;
            }

            Core.CurrentUser = user;

            MessageBox.Show("Успешный вход в аккаунт");

            if (NavigationService != null)
            {
                NavigationService.Navigate(new MainPage());
            }

            return true;
        }

        /// <summary>
        /// Обработчик нажатия кнопки авторизации.
        /// Запускает процесс проверки введённых пользователем логина и пароля.
        /// </summary>
        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            Auth(LoginTB.Text, PasswordTB.Text);
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
