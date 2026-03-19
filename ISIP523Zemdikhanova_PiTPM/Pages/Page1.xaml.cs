using System;
using System.Collections.Generic;
using System.Globalization;
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
using static System.Net.Mime.MediaTypeNames;

namespace ISIP523Zemdikhanova_PiTPM.Pages
{

    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }



        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page2());
        }


        /// <summary>
        /// Вычисляет значение функции
        /// </summary>
        /// <param name="x">Переменная X</param>
        /// <param name="y">Переменная Y</param>
        /// <param name="z">Переменная Z</param>
        /// <param name="result">Результат вычисления</param>
        /// <returns>true - если успешно, false - если ошибка</returns>
        public bool Calculate(double x, double y, double z, out double result)
        {
            result = 0;

            if (x < -1 || x > 1)
                return false;

            double denominator = Math.Abs(x - y) * z + Math.Pow(x, 2);

            if (denominator == 0)
                return false;

            result = 5 * Math.Atan(x) - 0.25 * Math.Acos(x) * (x + 3 * Math.Abs(x - y) + Math.Pow(x, 2)) / denominator;

            return true;
        }

        /// <summary>
        /// Обработчик события нажатия кнопки "Рассчитать".
        /// Выполняет проверку введённых пользователем данных,
        /// вызывает метод вычисления математической функции
        /// и отображает результат либо сообщение об ошибке.
        /// </summary>
        /// <param name="sender">Источник события (кнопка).</param>
        /// <param name="e">Данные события.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(XTextBox.Text.Replace('.', ','), out double x) ||
                    !double.TryParse(YTextBox.Text.Replace('.', ','), out double y) ||
                    !double.TryParse(ZTextBox.Text.Replace('.', ','), out double z))
            {
                MessageBox.Show("Введите корректные числа");
                return;
            }

            if (Calculate(x, y, z, out double result))
            {
                AnswerTextBlock.Text = Math.Round(result, 7).ToString();
            }
            else
            {
                MessageBox.Show("Ошибка вычисления!");
            }

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;


            if (!char.IsDigit(e.Text, 0) && e.Text != "." && e.Text != "," && e.Text != "-")
            {
                e.Handled = true;
                return;
            }


            if ((e.Text == "." || e.Text == ",") && textBox.SelectionStart == 0)
            {
                e.Handled = true;
                return;
            }


            if (e.Text == "-" && textBox.SelectionStart != 0)
            {
                e.Handled = true;
                return;
            }

            string fullText = textBox.Text.Remove(textBox.SelectionStart, textBox.SelectionLength).Insert(textBox.SelectionStart, e.Text);

            if (fullText.Count(c => c == ',' || c == '.') > 1)
            {
                e.Handled = true;
                return;
            }
        }


        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            XTextBox.Clear();
            YTextBox.Clear();
            ZTextBox.Clear();
            AnswerTextBlock.Text = " ";
        }
    }
}
