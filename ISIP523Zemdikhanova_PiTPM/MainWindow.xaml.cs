using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ISIP523Zemdikhanova_PiTPM
{
    /// <summary>
    /// Главное окно приложения "Закон Ома".
    /// Позволяет рассчитывать напряжение, силу тока или сопротивление в зависимости от выбранного режима.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик выбора первого режима (расчёт силы тока по закону Ома: I = U / R).
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        public void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            TextBlock1.Text = "Напряжение (Вольт): ";
            TextBlock2.Text = "Сопротивление (Ом): ";
            TextBlock3.Text = "Сила тока = ";
            AnswerTextBlock.Text = "";
        }

        /// <summary>
        /// Обработчик выбора второго режима (расчёт напряжения: U = I * R).
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        public void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            TextBlock1.Text = "Сила тока (Амперы): ";
            TextBlock2.Text = "Сопротивление (Ом): ";
            TextBlock3.Text = "Напряжение = ";
            AnswerTextBlock.Text = "";
        }

        /// <summary>
        /// Обработчик выбора третьего режима (расчёт сопротивления: R = U / I).
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        public void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            TextBlock1.Text = "Сила тока (Амперы): ";
            TextBlock2.Text = "Напряжение (Вольт): ";
            TextBlock3.Text = "Сопротивление = ";
            AnswerTextBlock.Text = "";
        }

        /// <summary>
        /// Выполняет расчёт по закону Ома в зависимости от выбранного режима.
        /// </summary>
        /// <param name="value1">Первое введённое значение (зависит от режима)</param>
        /// <param name="value2">Второе введённое значение (зависит от режима)</param>
        /// <param name="mode">Режим расчёта:
        /// <list type="number">
        ///     <item>1 — Сила тока (I = value1 / value2)</item>
        ///     <item>2 — Напряжение (U = value1 * value2)</item>
        ///     <item>3 — Сопротивление (R = value2 / value1)</item>
        /// </list>
        /// </param>
        /// <param name="result">Результат расчёта, округлённый до 4 знаков после запятой</param>
        /// <param name="hasErrors">Флаг, указывающий, произошла ли ошибка (деление на ноль или неверный режим)</param>

        public bool Calculate(double value1, double value2, int mode, out double result, out bool hasErrors)
        {
            result = 0;
            hasErrors = false;

            if (mode == 1) 
            {
                if (value2 == 0)
                {
                    hasErrors = true;
                    return false;
                }
                result = Math.Round(value1 / value2, 4);
            }
            else if (mode == 2)
            {
                result = Math.Round(value1 * value2, 4);
            }
            else if (mode == 3)
            {
                if (value1 == 0)
                {
                    hasErrors = true;
                    return false;
                }
                result = Math.Round(value2 / value1, 4);
            }
            else
            {
                hasErrors = true;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Рассчитать".
        /// Выполняет парсинг введённых значений, определяет режим и вызывает метод расчёта.
        /// </summary>
        /// <param name="sender">Источник события (кнопка)</param>
        /// <param name="e">Аргументы события</param>
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {

            if (!double.TryParse(TextBox1.Text.Replace(',', '.'), NumberStyles.Any,
                    CultureInfo.InvariantCulture, out double value1) ||
                !double.TryParse(TextBox2.Text.Replace(',', '.'), NumberStyles.Any,
                    CultureInfo.InvariantCulture, out double value2))
            {
                MessageBox.Show("Введите корректные числа");
                return;
            }

            int mode = 0;
            if (RadioButton1.IsChecked == true) mode = 1;
            else if (RadioButton2.IsChecked == true) mode = 2;
            else if (RadioButton3.IsChecked == true) mode = 3;

            if (mode == 0)
            {
                MessageBox.Show("Выберите режим расчёта");
                return;
            }

            bool success = Calculate(value1, value2, mode, out double result, out bool hasErrors);

            if (!success)
            {
                if (hasErrors)
                {
                    MessageBox.Show("Ошибка! Деление на ноль или некорректный режим");
                }
                return;
            }

            AnswerTextBlock.Text = result.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Предварительная проверка ввода в текстовое поле.
        /// Разрешает только цифры, точку, запятую и минус (в начале числа).
        /// </summary>
        /// <param name="sender">Текстовое поле</param>
        /// <param name="e">Аргументы события ввода текста</param>
        public void TextBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            if (!char.IsDigit(e.Text[0]) && e.Text != "." && e.Text != "," && e.Text != "-")
            {
                e.Handled = true;
                return;
            }

            if (e.Text == "-" && textBox.SelectionStart != 0)
            {
                e.Handled = true;
            }
        }
    }
}