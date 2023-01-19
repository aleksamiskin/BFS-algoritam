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
    /// Interaction logic for PolygonWindow.xaml
    /// </summary>
    public partial class PolygonWindow : Window
    {

        private MainWindow mw;
        private Polygon pol;

        public PolygonWindow(ref MainWindow value)
        {
            InitializeComponent();
            mw = value;
            mw.dodat = true;
        }

        public PolygonWindow(ref MainWindow value, Polygon pol)
        {
            InitializeComponent();
            mw = value;
            borderTh.Value = (Int32)pol.StrokeThickness;
            fillColor.SelectedColor = ((SolidColorBrush)pol.Fill).Color;
            borderColor.SelectedColor = ((SolidColorBrush)pol.Stroke).Color;
            text.Text = "sup";
            text.FontSize = 20;
            textColor.SelectedColor = ((SolidColorBrush)pol.Fill).Color;
            mw.tacke = pol.Points.ToList();
            this.pol = pol;
            borderTh.IsEnabled = false;
		    text.IsEnabled = false;
       }
        private void Nacrtaj(object sender, RoutedEventArgs e)
        {
            if (borderTh.Text.Trim() == "")
            {
                MessageBox.Show("Popunite validno sva polja!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (Int32.Parse(borderTh.Text) <= -1)
            {

                MessageBox.Show("Broj mora biti pozitivan!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            mw.id++;

            Polygon newObject = new Polygon
            {
                StrokeThickness = double.Parse(borderTh.Text),
                Fill = new BrushConverter().ConvertFromString(fillColor.SelectedColor.ToString()) as SolidColorBrush,
                Stroke = new BrushConverter().ConvertFromString(borderColor.SelectedColor.ToString()) as SolidColorBrush,
                Name = "pol" + mw.id.ToString()
            };

            TextBox tb = new TextBox
            {
                Text = text.Text,
                FontSize = text.FontSize,
                Foreground = text.Foreground,
            };

            double pomeraj1X = mw.tacke[0].X;
            double pomeraj1Y = mw.tacke[0].Y;

            foreach (Point t in mw.tacke)
            {
                newObject.Points.Add(t);
            }

            if (mw.dodat)
            {
                mw.GridCanvas.Children.Add(newObject);
                Canvas.SetLeft(tb, pomeraj1X);
                Canvas.SetTop(tb, pomeraj1Y);
                mw.GridCanvas.Children.Add(tb);
            }
            else
            {
                pol.Stroke = newObject.Stroke;
                pol.Fill = newObject.Fill;
            }



            mw.dodat = false;
            mw.tacke.Clear();
            this.Close();

        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
