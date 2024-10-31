using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace л7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TextBox _activeTextBox;

        public MainWindow()
        {
            InitializeComponent();


            var gradientBrush = new LinearGradientBrush(Colors.LightBlue, Colors.LightGreen, new Point(0, 0), new Point(1, 1));
            leftGrid.Background = gradientBrush;
            rightGrid.Background = gradientBrush;


            CreateTextFields(leftGrid, "Left");
            CreateTextFields(rightGrid, "Right");


            foreach (var child in leftGrid.Children)
            {
                if (child is TextBox)
                {
                    (child as TextBox).GotFocus += TextBox_GotFocus;
                }
            }
            foreach (var child in rightGrid.Children)
            {
                if (child is TextBox)
                {
                    (child as TextBox).GotFocus += TextBox_GotFocus;
                }
            }


            _activeTextBox = (leftGrid.Children[0] as TextBox);
            _activeTextBox.Style = (Style)Resources["LargeTextStyle"];
        }


        private void CreateTextFields(Grid grid, string prefix)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            for (int i = 0; i < 3; i++)
            {
                var textBox = new TextBox
                {
                    Name = $"{prefix}TextBox{i + 1}",
                    TextWrapping = TextWrapping.Wrap,
                    AcceptsReturn = true
                };


                textBox.Style = (Style)Resources["SmallTextStyle"];

                grid.Children.Add(textBox);
                Grid.SetRow(textBox, i);
                Grid.SetColumn(textBox, 0);
            }
        }


        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {

            if (_activeTextBox != null)
            {
                _activeTextBox.Style = (Style)Resources["SmallTextStyle"];
            }
            _activeTextBox = (sender as TextBox);
            _activeTextBox.Style = (Style)Resources["LargeTextStyle"];

            var animation = new DoubleAnimation
            {
                From = 10,
                To = 20,
                Duration = new Duration(TimeSpan.FromSeconds(0.5)),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            _activeTextBox.BeginAnimation(TextBox.FontSizeProperty, animation);
        }
    }
}