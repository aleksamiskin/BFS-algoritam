using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Projekat1
{
    public class PomocnaKlasa
    {


        public static void CanvasTOCanvas(Canvas canvas1, List<UIElement> canvas2)
        {
            canvas2.Clear();
            if (canvas1.Children.Count != 0)
            {
                foreach (UIElement ui in canvas1.Children)
                {
                    canvas2.Add(ui);

                }
            }
        }

        public static void CanvasTOCanvas(List<UIElement> canvas1, Canvas canvas2)
        {
            canvas2.Children.Clear();
            if (canvas1.Count != 0)
            {
                foreach (var ui in canvas1)
                {
                    canvas2.Children.Add((UIElement)ui);

                }
            }
        }

        public static void CanvasTOCanvas(Canvas canvas1, Canvas canvas2)
        {
            canvas2.Children.Clear();
            if (canvas1.Children.Count != 0)
            {
                foreach (UIElement ui in canvas1.Children)
                {
                    canvas2.Children.Add((UIElement)ui);

                }
            }
        }

    }
}
