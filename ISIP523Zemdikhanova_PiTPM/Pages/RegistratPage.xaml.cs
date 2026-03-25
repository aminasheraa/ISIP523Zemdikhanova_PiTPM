using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

        /// <summary>
        /// Обработчик нажатия кнопки регистрации.
        /// Вызывает метод регистрации пользователя и при успешном результате
        /// отображает сообщение и выполняет переход на главную страницу.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события нажатия кнопки.</param>
        private void RegistBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = Register(
                LoginTB.Text,
                PasswordTB.Text,
                EmailTB.Text,
                DateBirthDP.SelectedDate
            );

            if (result)
            {
                MessageBox.Show("Успешная регистрация!");
                NavigationService.Navigate(new MainPage());
            }
        }

        /// <summary>
        /// Выполняет регистрацию пользователя с проверкой входных данных.
        /// Проверяет заполненность полей, корректность пароля, email и даты рождения.
        /// В случае успешной валидации создаёт пользователя и сохраняет его в базу данных.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <param name="password">Пароль пользователя (должен содержать 8 символов).</param>
        /// <param name="email">Адрес электронной почты пользователя.</param>
        /// <param name="birthDate">Дата рождения пользователя.</param>

        public bool Register(string login, string password, string email, DateTime? birthDate)
        {

            if (string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Заполните все поля");
                return false;
            }

            if (password.Length < 8 || password.Length > 32)
            {
                MessageBox.Show("Пароль должен быть от 8 до 32 символов");
                return false;
            }

            if (!email.Contains("@"))
            {
                MessageBox.Show("Введите корректный адрес электронной почты");
                return false;
            }

            if (birthDate == null)
            {
                MessageBox.Show("Выберите дату рождения");
                return false;
            }

            if (birthDate > DateTime.Now)
            {
                MessageBox.Show("Дата рождения не может быть больше текущей");
                return false;
            }

            var user = new User
            {
                Username = login,
                Password = password,
                DateOfBirth = birthDate.Value,
                Email = email
            };


            Core.Context.User.Add(user);
            try
            {
                Core.Context.User.Add(user);
                Core.Context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errors = new StringBuilder();

                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        errors.AppendLine($"Поле: {ve.PropertyName} — Ошибка: {ve.ErrorMessage}");
                    }
                }

                MessageBox.Show(errors.ToString());
                throw;
            }
            Core.CurrentUser = user;

            return true;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
