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

using FirstGuiDemo.Tricks;
using static System.Math;

namespace FirstGuiDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, Func<double, double, double>> operations;

        private StringBuilder buffer = new StringBuilder();

        public MainWindow()
        {
            InitializeComponent();

            operations = new Dictionary<string, Func<double, double, double>>
            {
                {"+", (x, y) => x + y}, 
                {"-", (x, y) => x - y}, 
                {"*", (x, y) => x * y}, 
                {"/", (x, y) => x / y}, 
                {"^", Pow}, 
            };

            var digitsGrid = new Grid();

            int colSize = 3;
            for (int i = 0; i < colSize; i++)
                digitsGrid.ColumnDefinitions.Add(new ColumnDefinition());

            int digitBase = 10;
            int rowSize = digitBase / colSize + 1;
            for (int i = 0; i < rowSize; i++)
                digitsGrid.RowDefinitions.Add(new RowDefinition());

            for (int i = 0; i < digitBase; i++)
            {
                var tmp = new Button
                {
                    Content = i.ToString("D"),
                    FontFamily = new FontFamily("Monospace"),
                    FontSize = 18
                };
                tmp.Click += DigitOnClick;
                tmp.AddTo(digitsGrid, i / colSize, i % colSize);
            }

            MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition {MinWidth = 50});
            MainGrid.RowDefinitions.Add(new RowDefinition());
            MainGrid.RowDefinitions.Add(new RowDefinition { MinHeight = 150});
            MainGrid.RowDefinitions.Add(new RowDefinition());


            digitsGrid.AddTo(MainGrid, row: 1, rowSpan: 2);

            var operationPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
            };
            
            foreach (var operation in operations)
            {
                var button = new Button
                {
                    Content = operation.Key,
                    FontFamily = new FontFamily("Monospace"),
                    FontSize = 18,
                    Width = 50,
                };
                button.Click += OperationOnClick;
                button.AddTo(operationPanel);
            }

            operationPanel.AddTo(MainGrid, row: 1, column: 1);

            var result = new Button
            {
                Content = "Result"
            };
            result.Click += ResultOnClick;
            result.AddTo(MainGrid, row: 2, column: 1);
        }

        private void ResultOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var tmp = buffer.ToString();

            var nodes = tmp.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                // WARNING!!!
                var first = double.Parse(nodes[0]);
                var operation = nodes[1];
                var second = double.Parse(nodes[2]);

                var result = operations[operation](first, second).ToString("F");
                MessageBox.Show($"result of [{first} {operation} {second}] is {result}");
                // WARNING!!!
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OperationOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var sourceButton = sender as Button;
            if (sourceButton == null) return;

            var key = sourceButton.Content.ToString();
            MessageBox.Show(key);

            buffer.AppendFormat($" {key} ");
        }

        private void DigitOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var sourceButton = sender as Button;
            if (sourceButton == null) return;

            var digit = sourceButton.Content.ToString();
            if (digit.All(char.IsDigit))
                MessageBox.Show(digit);

            buffer.Append(digit);
        }
    }
}
