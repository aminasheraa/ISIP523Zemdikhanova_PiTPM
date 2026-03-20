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
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;

namespace ISIP523Zemdikhanova_PiTPM.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();

            ChartFunction.ChartAreas.Clear();
            ChartFunction.Series.Clear();

            ChartArea chartArea = new ChartArea("Main");
            chartArea.AxisX.Title = "X";
            chartArea.AxisX.TitleFont = new System.Drawing.Font("Arial", 12);
            chartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea.AxisX.Interval = 1;

            chartArea.AxisY.Title = "Y";
            chartArea.AxisY.TitleFont = new System.Drawing.Font("Arial", 12);
            chartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;

            ChartFunction.ChartAreas.Add(chartArea);

            Series series = new Series("Функция");
            series.ChartType = SeriesChartType.Line;

            series.ToolTip = "X = #VALX, Y = #VALY";
            series.MarkerStyle = MarkerStyle.Circle;

            ChartFunction.Series.Add(series);

            Title title = new Title();
            title.Text = "График функции y(x)";
            title.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

            ChartFunction.Titles.Add(title);
            series.ToolTip = "X = #VALX, Y = #VALY";
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page2());
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
        /// Вычисляет значения функции и сообщает о наличии некорректных значений.
        /// </summary>
        /// <param name="x0">Начальное значение X</param>
        /// <param name="xk">Конечное значение X</param>
        /// <param name="dx">Шаг</param>
        /// <param name="b">Параметр B</param>
        /// <param name="result">Список значений (x, y)</param>
        /// <param name="hasErrors">Были ли пропущенные значения (из-за отрицательного корня)</param>
        /// <returns>true - если входные данные корректны</returns>
        public bool Calculate(double x0, double xk, double dx, double b,
            out List<(double x, double y)> result,
            out bool hasErrors)
        {
            result = new List<(double x, double y)>();
            hasErrors = false;

            if (dx <= 0 || x0 > xk || dx > Math.Abs(xk - x0))
                return false;

            for (double x = x0; x <= xk; x += dx)
            {
                double underRoot = Math.Pow(x, 3) + Math.Pow(b, 3);

                if (underRoot < 0)
                {
                    hasErrors = true;
                    continue;
                }

                double y = 9 * (x + 15 * Math.Sqrt(underRoot));
                result.Add((x, y));
            }

            return true;
        }

        /// <summary>
        /// Обработчик кнопки построения графика
        /// Выполняет проверку ввода и отображает результаты вычислений
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChartFunction.Series[0].Points.Clear();

            if (!double.TryParse(X0TextBox.Text.Replace('.', ','), out double x0) ||
                !double.TryParse(XKTextBox.Text.Replace('.', ','), out double xk) ||
                !double.TryParse(DXTextBox.Text.Replace('.', ','), out double dx) ||
                !double.TryParse(BTextBox.Text.Replace('.', ','), out double b))
            {
                MessageBox.Show("Введите корректные числа");
                return;
            }

            if (!Calculate(x0, xk, dx, b, out var values, out bool hasErrors))
            {
                MessageBox.Show("Ошибка входных данных");
                return;
            }

            var series = ChartFunction.Series.First();
            series.Points.Clear();
            AnswerTextBlock.Text = "";

            foreach (var (x, y) in values)
            {
                series.Points.AddXY(x, y);
                AnswerTextBlock.Text += $"x = {x:F2}   y = {y:F4}\n";
            }

            if (hasErrors)
            {
                MessageBox.Show("Некоторые значения пропущены (подкоренное выражение < 0)");
            }
        }


        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            AnswerTextBlock.Text = " ";
            X0TextBox.Clear();
            XKTextBox.Clear();
            DXTextBox.Clear();
            BTextBox.Clear();

            ChartFunction.Series.First().Points.Clear();
        }
    }
}
