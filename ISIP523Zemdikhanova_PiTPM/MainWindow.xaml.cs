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

namespace ISIP523Zemdikhanova_PiTPM
{
    /// <summary>
    /// Логика взаимодействия для основного окна приложения.
    /// Реализует графический интерфейс для шифра ADFGVX.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary> Текущая строка символов, представляющая матрицу 6x6. </summary>
        private string _currentMatrix = AdfgvxCipher.GetDefaultMatrix();

        /// <summary>
        /// Конструктор окна. Инициализирует компоненты и отрисовывает начальную матрицу.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DrawMatrix();
        }

        /// <summary>
        /// Отрисовывает визуальную сетку матрицы 6x6.
        /// </summary>
        private void DrawMatrix()
        {
            MatrixGrid.Children.Clear();
            foreach (char c in _currentMatrix)
            {
                var border = new Border { BorderBrush = Brushes.LightGray, BorderThickness = new Thickness(0.5) };
                border.Child = new TextBlock
                {
                    Text = c.ToString(),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontWeight = FontWeights.Medium
                };
                MatrixGrid.Children.Add(border);
            }
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Зашифровать".
        /// Выполняет валидацию и вызывает модуль шифрования.
        /// </summary>
        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateInputs())
                {
                    LogArea.Clear();
                    LogArea.AppendText($"Начало шифрования текста: {InputText.Text}\n");

                    string result = AdfgvxCipher.Encrypt(InputText.Text, KeyInput.Text, _currentMatrix);

                    LogArea.AppendText("Замена через матрицу завершена.\n");
                    LogArea.AppendText($"Транспозиция по ключу {KeyInput.Text} выполнена.\n");

                    OutputText.Text = result;
                    LogArea.ScrollToEnd();
                }
            }
            catch
            {
                LogArea.AppendText($"Ошибка \n");
                MessageBox.Show($"Произошла ошибка при шифровании", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Расшифровать".
        /// </summary>
        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateInputs())
                {
                    LogArea.Clear();
                    LogArea.AppendText($"Попытка дешифровки данных\n");

                    string result = AdfgvxCipher.Decrypt(InputText.Text, KeyInput.Text, _currentMatrix);

                    if (string.IsNullOrEmpty(result))
                    {
                        LogArea.AppendText("Не удалось сопоставить символы шифра с матрицей.\n");
                        MessageBox.Show("Ошибка расшифровки: убедитесь, что текст содержит только символы A,D,F,G,V,X.", "Валидация", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        LogArea.AppendText("Обратная перестановка столбцов завершена.\n");
                        LogArea.AppendText("Обратная подстановка по матрице успешна.\n");
                        OutputText.Text = result;
                    }
                    LogArea.ScrollToEnd();
                }
            }
            catch
            {
                MessageBox.Show($"Произошла ошибка при расшифровке", "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Выполняет проверку полей ввода на пустоту и наличие некорректных данных.
        /// </summary>
        /// <returns>True, если данные валидны.</returns>
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(InputText.Text))
            {
                MessageBox.Show("Поле текста не может быть пустым!", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(KeyInput.Text))
            {
                MessageBox.Show("Ключ перестановки должен содержать хотя бы один символ!", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Обработчик для перемешивания матрицы символов.
        /// </summary>
        private void ShuffleMatrix_Click(object sender, RoutedEventArgs e)
        {
            var rnd = new Random();
            _currentMatrix = new string(_currentMatrix.OrderBy(x => rnd.Next()).ToArray());
            DrawMatrix();

            LogArea.Clear();
            LogArea.AppendText("Матрица успешно перемешана. Алфавит замены изменен.");
        }
    }
}