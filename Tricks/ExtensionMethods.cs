using System.Windows;
using System.Windows.Controls;

namespace FirstGuiDemo.Tricks
{
    public static class ExtensionMethods
    {
        public static void AddTo(this UIElement element, Panel panel,  
            int row = 0, int column = 0,
            int rowSpan = 1, int columnSpan = 1)
        {
            Grid.SetRow(element, row);
            Grid.SetRowSpan(element, rowSpan);
            Grid.SetColumn(element, column);
            Grid.SetColumnSpan(element, columnSpan);
            panel.Children.Add(element);
        }
    }
}