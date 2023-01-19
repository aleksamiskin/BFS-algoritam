using Microsoft.Win32;
using Projekat1;
using Projekat1.Models;
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

namespace Projekat1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ImportAndDraw mainImportDraw = new ImportAndDraw();
        string location = "";
        //public MainWindow()
        //{
        //    InitializeComponent();
        //}

        private void OpenFileBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "xml";
            openFileDialog.Filter = "XML Files|*.xml";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    if (GridCanvas.Children.Count > 0)
                    {
                        string message = "Are you sure? Old data will be lost.";
                        string caption = "Confirmation";
                        MessageBoxButton buttons = MessageBoxButton.YesNo;
                        MessageBoxImage icon = MessageBoxImage.Question;
                        if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
                        {
                            using (new WaitCursor())
                            {
                                mainImportDraw = new ImportAndDraw();
                                GridCanvas.Children.Clear();
                                location = openFileDialog.FileName;
                                mainImportDraw.LoadAndParseXML(location);
                                mainImportDraw.ScaleFromLatLonToCanvas(GridCanvas.Width, GridCanvas.Height);
                                mainImportDraw.ConvertFromLatLonToCanvasCoord();
                                mainImportDraw.DrawElements(this.GridCanvas);
                            }
                        }
                    }
                    else
                    {
                        using (new WaitCursor())
                        {
                            mainImportDraw = new ImportAndDraw();
                            GridCanvas.Children.Clear();
                            location = openFileDialog.FileName;
                            mainImportDraw.LoadAndParseXML(location);
                            mainImportDraw.ScaleFromLatLonToCanvas(GridCanvas.Width, GridCanvas.Height);
                            mainImportDraw.ConvertFromLatLonToCanvasCoord();
                            mainImportDraw.DrawElements(this.GridCanvas);
                        }


                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Please, provide a valid xml file.", "Invalid file", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

        }

        public double X { get; set; }
        public double Y { get; set; }

        public List<System.Windows.Point> tacke { get; set; }

        //public List<UIElement> listaPrethodno { get; set; } //undo
        public List<UIElement> listaObrisano { get; set; }  //clear i undo
        public List<UIElement> listaVraceno { get; set; }  //redo

        public bool dodat;
        public bool clearToken;

        private int brojacTacaka = 0;
        public int id = 0;
        public string oblik = "";

        public MainWindow()
        {
            InitializeComponent();

            dodat = false;
            tacke = new List<System.Windows.Point>();
            //listaPrethodno = new List<UIElement>();
            listaObrisano = new List<UIElement>();
            listaVraceno = new List<UIElement>();
            clearToken = false;

        }

        private void DesniKlik(object sender, MouseButtonEventArgs e)
        {
            var temp = this;
            X = Mouse.GetPosition(GridCanvas).X;
            Y = Mouse.GetPosition(GridCanvas).Y;

            if (oblik == "elipsa")
            {
                EllipseWindow prozor = new EllipseWindow(ref temp);
                prozor.Show();
            }
            else if (oblik == "polygon")
            {
                brojacTacaka++;
                System.Windows.Point p = new System.Windows.Point();
                p.X = Mouse.GetPosition(GridCanvas).X;
                p.Y = Mouse.GetPosition(GridCanvas).Y;
                tacke.Add(p);
                //PolygonWindow prozor = new PolygonWindow(ref temp);
                //prozor.Show();
            }
            else if(oblik == "tekst")
            {
                TextWindow prozor = new TextWindow(ref temp);
                prozor.Show();
            }
        }

        private void Oblik(object sender, RoutedEventArgs e)
        {
            Pomocna();

            if (((MenuItem)sender).IsEnabled)
            {

                if (oblik != ((MenuItem)sender).Name)
                {
                    menu.Background = Brushes.White;
                    ((MenuItem)sender).Background = Brushes.LightBlue;
                    oblik = ((MenuItem)sender).Name;

                }
                else
                {
                    menu.Background = Brushes.White;
                    ((MenuItem)sender).Background = Brushes.Transparent;
                    oblik = "";
                }

            }

        }

        private void LeviKlik(object sender, MouseButtonEventArgs e)
        {

            var temp = this;

            if (oblik == "polygon" && brojacTacaka >= 1)
            {

                brojacTacaka = 0;
                PolygonWindow prozor = new PolygonWindow(ref temp);
                prozor.Show();

            }
            if (e.OriginalSource is Ellipse)
            {
                Ellipse elipsa = (Ellipse)e.OriginalSource;
                EllipseWindow prozor = new EllipseWindow(ref temp, elipsa);
                prozor.Show();
            }
            else if (e.OriginalSource is Polygon)
            {
                Polygon pol = (Polygon)e.OriginalSource;
                PolygonWindow prozor = new PolygonWindow(ref temp, pol);
                prozor.Show();
            }
            else if(e.OriginalSource is Label)
            {
                Label tb = (Label)e.OriginalSource;
                TextWindow prozor = new TextWindow(ref temp, tb);
                prozor.Show();
            }
        }

        private void Obrisi(object sender, RoutedEventArgs e)
        {
            if (!clearToken)
            {
                clearToken = true;
                PomocnaKlasa.CanvasTOCanvas(GridCanvas, listaObrisano);
                GridCanvas.Children.Clear();
            }
        }

        private void Undo(object sender, RoutedEventArgs e)
        {

            if (GridCanvas.Children.Count >= 1)
            {
                listaVraceno.Add(GridCanvas.Children[GridCanvas.Children.Count - 1]);
                GridCanvas.Children.RemoveAt(GridCanvas.Children.Count - 1);
            }
            else if (clearToken)
            {
                clearToken = false;
                PomocnaKlasa.CanvasTOCanvas(listaObrisano, GridCanvas);
                listaObrisano.Clear();
            }

        }

        private void Redo(object sender, RoutedEventArgs e)
        {
            if (listaVraceno.Count >= 1)
            {
                GridCanvas.Children.Add(listaVraceno[listaVraceno.Count - 1]);
                listaVraceno.RemoveAt(listaVraceno.Count - 1);
            }
        }

        private void Pomocna()
        {
            elipsa.Background = Brushes.Transparent;
            polygon.Background = Brushes.Transparent;
        }
    }


    public class WaitCursor : IDisposable
    {
        private Cursor _previousCursor;

        public WaitCursor()
        {
            _previousCursor = Mouse.OverrideCursor;

            Mouse.OverrideCursor = Cursors.Wait;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Mouse.OverrideCursor = _previousCursor;
        }

        #endregion
    }
}
