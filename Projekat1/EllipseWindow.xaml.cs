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
    /// Interaction logic for EllipseWindow.xaml
    /// </summary>
    public partial class EllipseWindow : Window
    {
        private MainWindow mw;
        private Ellipse ell;

        public EllipseWindow(ref MainWindow value)
        {
            InitializeComponent();
            mw = value;
            mw.dodat = true;
            ell = null;
        }

        public EllipseWindow(ref MainWindow value, Ellipse ellipse)
        {
            InitializeComponent();
            mw = value;
            width.Value = (Int32)ellipse.Width;
            height.Value = (Int32)ellipse.Height;
            borderTh.Value = (Int32)ellipse.StrokeThickness;
            fillColor.SelectedColor = ((SolidColorBrush)ellipse.Fill).Color;
            borderColor.SelectedColor = ((SolidColorBrush)ellipse.Stroke).Color;
            text.Text = "sup";
            text.FontSize = 20;
            textColor.SelectedColor = ((SolidColorBrush)ellipse.Fill).Color;
            ell = ellipse;
            width.IsEnabled = false;
            height.IsEnabled = false;
            borderTh.IsEnabled = false;
            text.IsEnabled = false;
        }

        private void Nacrtaj(object sender, RoutedEventArgs e)
        {
            if (height.Text.Trim() == "" || width.Text.Trim() == "" || borderTh.Text.Trim() == "")
            {
                MessageBox.Show("Popunite validno sva polja!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (Int32.Parse(height.Text) <= 0 || Int32.Parse(width.Text) <= 0 || Int32.Parse(borderTh.Text) <= -1)
            {

                MessageBox.Show("Broj mora biti pozitivan!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            mw.id++;

            Ellipse newObject = new Ellipse
            {
                Width = double.Parse(width.Text),
                Height = double.Parse(height.Text),
                StrokeThickness = double.Parse(borderTh.Text),
                Fill = new BrushConverter().ConvertFromString(fillColor.SelectedColor.ToString()) as SolidColorBrush,
                Stroke = new BrushConverter().ConvertFromString(borderColor.SelectedColor.ToString()) as SolidColorBrush,

                Name = "ell" + mw.id.ToString()
            };

            TextBox tb = new TextBox
            {
                Text = text.Text,
                FontSize = text.FontSize,
                Foreground = text.Foreground,
            };

            double pomerajX = newObject.Width / 2;
            double pomerajY = newObject.Height / 2;

            if (mw.dodat)
            {
                Canvas.SetLeft(newObject, mw.X);
                Canvas.SetTop(newObject, mw.Y);
                mw.GridCanvas.Children.Add(newObject);
                Canvas.SetLeft(tb, mw.X + pomerajX);
                Canvas.SetTop(tb, mw.Y + pomerajY);
                mw.GridCanvas.Children.Add(tb);
            }
            else
            {
                ell.Fill = newObject.Fill;
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
