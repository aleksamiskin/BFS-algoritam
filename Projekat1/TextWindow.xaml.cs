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
using System.Windows.Shapes;

namespace Projekat1
{
    /// <summary>
    /// Interaction logic for TextWindow.xaml
    /// </summary>
    public partial class TextWindow : Window
    {
        private MainWindow mw;
        private Label tb;
        public TextWindow(ref MainWindow value)
        {
            InitializeComponent();
            mw = value;
            mw.dodat = true;
            tb = null;
        }

        public TextWindow(ref MainWindow value, Label textBox)
        {
            InitializeComponent();
            mw = value;
            text.ValueDataType = (Type)textBox.Content;
            size.Value = (Int32)textBox.FontSize;
            fillColor.SelectedColor = ((SolidColorBrush)textBox.Foreground).Color;
            tb = textBox;
            text.IsEnabled = false;
        }

        private void Nacrtaj(object sender, RoutedEventArgs e)
        {
            if(text.Text.Trim() == "")
            {
                MessageBox.Show("Popunite validno sva polja!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (Int32.Parse(size.Text) <= 0)
            {
                MessageBox.Show("Broj mora biti pozitivan!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            mw.id++;

            Label newObject = new Label
            {
                Content = text.Text,
                FontSize = Double.Parse(size.Text.ToString()),
                Foreground = new BrushConverter().ConvertFromString(fillColor.SelectedColor.ToString()) as SolidColorBrush,
            };

            if (mw.dodat)
            {
                Canvas.SetLeft(newObject, mw.X);
                Canvas.SetTop(newObject, mw.Y);
                mw.GridCanvas.Children.Add(newObject);

            }
            else
            {
                tb.Foreground = newObject.Foreground;
            }

            mw.dodat = false;
            this.Close();

        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
