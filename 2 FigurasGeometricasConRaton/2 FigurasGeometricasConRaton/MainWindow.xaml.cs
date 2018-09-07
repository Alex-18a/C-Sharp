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
        private double anchoPincel;
        private Line LineaTemporal;
        private Point centroCirculo;
        private Ellipse circuloTemporal;

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
                if (btnCirculo.IsChecked == true)
                {
                    //se aplica un pitagoras para el punto izquierdo superior y el radio
                    double radio = Math.Sqrt(Math.Pow((posX-centroCirculo.X),2)+Math.Pow((posY-centroCirculo.Y),2));
                    Canvas.SetLeft(circuloTemporal,centroCirculo.X-radio);
                    Canvas.SetTop(circuloTemporal, centroCirculo.Y - radio);

                    circuloTemporal.Width = radio * 2;
                    circuloTemporal.Height = radio * 2;
                    

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
            if (btnCirculo.IsChecked == true)
            {
                circuloTemporal = new Ellipse();
                circuloTemporal.Stroke = Brushes.Black;
                circuloTemporal.StrokeThickness = anchoPincel;
                circuloTemporal.Height = 0;
                circuloTemporal.Width = 0;

                centroCirculo = new Point(e.GetPosition(Lienzo).X, e.GetPosition(Lienzo).Y);

                Canvas.SetLeft(circuloTemporal, e.GetPosition(Lienzo).X);
                Canvas.SetTop(circuloTemporal, e.GetPosition(Lienzo).X);
                Lienzo.Children.Add(circuloTemporal);
            }
        }

        private void ActualizarAnchoDePincel(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            anchoPincel =sldAnchoDePincel.Value;
        }
    }
}
