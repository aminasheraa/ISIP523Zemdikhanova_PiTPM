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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(XTextBox.Text) || string.IsNullOrWhiteSpace(BTextBox.Text))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            if (!double.TryParse(XTextBox.Text.Replace('.', ','), out double x) || !double.TryParse(BTextBox.Text.Replace('.', ','), out double b))
            {
                MessageBox.Show("Введите корректные числа");
                return;
            }

            if (RadioButton1.IsChecked != true && RadioButton2.IsChecked != true && RadioButton3.IsChecked != true)
            {
                MessageBox.Show("Выберите функцию f(x)");
                return;
            }

            double fx = CalculateF(x);
            double answer = 0;

            if (1 < (x * b) && (x * b) < 10)
            {
                answer = Math.Exp(fx);
            }
            else if (12 < (x * b) && (x * b) < 40)
            {
                answer = Math.Sqrt(Math.Abs(fx + 4 * b));
            }
            else
            {
                answer = b * Math.Pow(fx, 2);
            }

            AnswerTextBlock.Text = Math.Round(answer, 7).ToString();


        }

        private double CalculateF(double x)
        {
            if (RadioButton1.IsChecked == true)
                return Math.Sinh(x);

            if (RadioButton2.IsChecked == true)
                return Math.Pow(x, 2);

            return Math.Exp(x);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            XTextBox.Clear();
            BTextBox.Clear();
            AnswerTextBlock.Text = " ";
        }

        
    }
}
