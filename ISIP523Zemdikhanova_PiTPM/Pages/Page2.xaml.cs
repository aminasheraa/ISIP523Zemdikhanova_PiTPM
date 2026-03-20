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
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page1());
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page3());

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

        /// <summary>
        /// Обработчик кнопки расчета
        /// Проверяет ввод, определяет выбранную функцию и выводит результат
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(XTextBox.Text) || string.IsNullOrWhiteSpace(BTextBox.Text))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            if (!double.TryParse(XTextBox.Text.Replace('.', ','), out double x) ||
                !double.TryParse(BTextBox.Text.Replace('.', ','), out double b))
            {
                MessageBox.Show("Введите корректные числа");
                return;
            }

            int functionType = 0;

            if (RadioButton1.IsChecked == true) functionType = 1;
            else if (RadioButton2.IsChecked == true) functionType = 2;
            else if (RadioButton3.IsChecked == true) functionType = 3;

            if (functionType == 0)
            {
                MessageBox.Show("Выберите функцию f(x)");
                return;
            }

            if (Calculate(x, b, functionType, out double result))
            {
                AnswerTextBlock.Text = Math.Round(result, 7).ToString();
            }
            else
            {
                MessageBox.Show("Ошибка вычисления!");
            }
        }

        /// <summary>
        /// Вычисляет значение функции в зависимости от выбранного варианта f(x) и условий для произведения x и b
        /// </summary>
        /// <param name="x">Переменная X</param>
        /// <param name="b">Переменная B</param>
        /// <param name="functionType">
        /// Тип функции:
        /// 1 - sinh(x)
        /// 2 - x^2
        /// 3 - exp(x)
        /// </param>
        /// <param name="result">Результат вычисления</param>
        /// <returns>true - если вычисление успешно, false - если ошибка</returns>
        public bool Calculate(double x, double b, int functionType, out double result)
        {
            result = 0;

            double fx;

            switch (functionType)
            {
                case 1:
                    fx = Math.Sinh(x);
                    break;
                case 2:
                    fx = Math.Pow(x, 2);
                    break;
                case 3:
                    fx = Math.Exp(x);
                    break;
                default:
                    return false;
            }

            double xb = x * b;

            if (1 < xb && xb < 10)
            {
                result = Math.Exp(fx);
            }
            else if (12 < xb && xb < 40)
            {
                result = Math.Sqrt(Math.Abs(fx + 4 * b));
            }
            else
            {
                result = b * Math.Pow(fx, 2);
            }

            return true;
        }


        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            XTextBox.Clear();
            BTextBox.Clear();
            AnswerTextBlock.Text = " ";
        }

        
    }
}
