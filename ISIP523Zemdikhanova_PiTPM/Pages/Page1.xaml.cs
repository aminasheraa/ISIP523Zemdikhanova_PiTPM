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



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(XTextBox.Text) || string.IsNullOrWhiteSpace(YTextBox.Text) || string.IsNullOrWhiteSpace(ZTextBox.Text))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            if (!double.TryParse(XTextBox.Text, out double x) ||
                !double.TryParse(YTextBox.Text, out double y) ||
                !double.TryParse(ZTextBox.Text, out double z))
            {
                MessageBox.Show("Введите корректные числа");
                return;
            }

            if (x < -1 || x > 1)
            {
                MessageBox.Show("Для расчета Acos(x) значение X должно быть от -1 до 1");
                return;
            }

            double denominator = Math.Abs(x - y) * z + Math.Pow(x, 2);
            if (denominator == 0)
            {
                MessageBox.Show("Деление на ноль!");
                return;
            }

            var answer = 5 * Math.Atan(x) - 0.25 * Math.Acos(x) * (x + 3 * Math.Abs(x - y) + Math.Pow(x, 2)) / denominator;
            AnswerTextBlock.Text = Math.Round(answer, 7).ToString();

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;

            string fullText = textBox.Text.Remove(textBox.SelectionStart, textBox.SelectionLength).Insert(textBox.SelectionStart, e.Text);

            fullText = fullText.Replace('.', ',');

            bool isValid = (fullText == "-") ||
                           (fullText.Count(c => c == ',') <= 1 && fullText.EndsWith(",") && double.TryParse(fullText.TrimEnd(','), out _)) ||
                           double.TryParse(fullText, out _);

            e.Handled = !isValid;
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
