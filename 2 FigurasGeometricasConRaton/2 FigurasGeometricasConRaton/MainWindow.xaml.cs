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

namespace _2_FigurasGeometricasConRaton
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    /// no se porque sale ese vaerga
    public partial class MainWindow : Window
    {
        private Line LineaTemporal;
        public MainWindow()
        {
            InitializeComponent();
        }      

        private void ActualizarPosicion(object sender, MouseEventArgs e)
        {
            int posX = (int)e.GetPosition(Lienzo).X, posY = (int)e.GetPosition(Lienzo).Y;
            lblPosicion.Content = "X: " + posX + " Y: " + posY;
            if (e.LeftButton==MouseButtonState.Pressed )
            {
                if (btnLinea.IsChecked == true)
                {
                    LineaTemporal.X2 = (int)e.GetPosition(Lienzo).X;
                    LineaTemporal.Y2 = (int)e.GetPosition(Lienzo).Y;
                }
            }
        }

        private void IniciarDibujado(object sender, MouseButtonEventArgs e)
        {
            if (btnLinea.IsChecked == true)
            {
                LineaTemporal = new Line();
                LineaTemporal.X1 = (int)e.GetPosition(Lienzo).X;
                LineaTemporal.Y1 = (int)e.GetPosition(Lienzo).Y;
                LineaTemporal.Stroke=Brushes.Black;
                LineaTemporal.StrokeThickness = 10;
                Lienzo.Children.Add(LineaTemporal);
                
            }
        }
    }
}
