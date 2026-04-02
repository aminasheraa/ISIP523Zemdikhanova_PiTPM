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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            TextBlock1.Text = "Напряжение (Вольт): ";
            TextBlock2.Text = "Сопротивление (Ом): ";
            TextBlock3.Text = "Сила тока = ";
            AnswerTextBlock.Text = "";
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            TextBlock1.Text = "Сила тока (Амперы): ";
            TextBlock2.Text = "Сопротивление (Ом): ";
            TextBlock3.Text = "Напряжение = ";
            AnswerTextBlock.Text = "";
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            TextBlock1.Text = "Сила тока (Амперы): ";
            TextBlock2.Text = "Напряжение (Вольт): ";
            TextBlock3.Text = "Сопротивление = ";
            AnswerTextBlock.Text = "";
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(TextBox1.Text.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double value1) ||
                !double.TryParse(TextBox2.Text.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double value2))
            {
                MessageBox.Show("Введите корректные числа");
                return;
            }

            double result;

            if (RadioButton1.IsChecked == true) 
            {
                if (value2 == 0)
                {
                    MessageBox.Show("Ошибка! Деление на ноль");
                    return;
                }

                result = Math.Round((value1 / value2), 4);
                AnswerTextBlock.Text = result.ToString();
            }

            else if (RadioButton2.IsChecked == true)
            {
                result = Math.Round((value1 * value2), 4);
                AnswerTextBlock.Text = result.ToString();
            }

            else if (RadioButton3.IsChecked == true) 
            {
                if (value1 == 0)
                {
                    MessageBox.Show("Ошибка! Деление на ноль");
                    return;
                }
                result = Math.Round((value2 / value1), 4);
                AnswerTextBlock.Text = result.ToString();
            }
            else
            {
                MessageBox.Show("Выберите режим расчёта");
            }
        }

        private void TextBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            string currentText = textBox.Text;

            if (!char.IsDigit(e.Text[0]) && e.Text != "." && e.Text != "," && e.Text != "-")
            {
                e.Handled = true;
                return;
            }

            if (e.Text == "-" && textBox.SelectionStart != 0)
            {
                e.Handled = true;
                return;
            }


        }
    }
}
