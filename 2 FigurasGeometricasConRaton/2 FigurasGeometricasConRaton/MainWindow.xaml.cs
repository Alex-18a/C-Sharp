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
        private Point centroCirculo;
        private Rectangle rectanguloTemporal;
        private Ellipse circuloTemporal;       
        private Line LineaTemporal;
        private Polygon poligonoTemporal;
        private int posX, posY;
        bool flag = true;
        private PointCollection puntos=new PointCollection();
        int contador = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ActualizarAnchoDePincel(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            anchoPincel = sldAnchoDePincel.Value;
        }

        private void DibujarPunto(double x, double y, double anchoPincel, Brush color)
        {
            Ellipse punto = new Ellipse();
            punto.Stroke = color;
            punto.StrokeThickness = anchoPincel;
            punto.Fill = color;

            punto.Height = anchoPincel;
            punto.Width = anchoPincel;

            Canvas.SetLeft(punto, x - anchoPincel / 2);
            Canvas.SetTop(punto, y - anchoPincel / 2);

            Lienzo.Children.Add(punto);
        }

        private void ActualizarPosicion(object sender, MouseEventArgs e)
        {
            posX = (int)e.GetPosition(Lienzo).X;
            posY = (int)e.GetPosition(Lienzo).Y;
            lblPosicion.Content = "X: " + posX + " Y: " + posY;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (btnPunto.IsChecked == true)
                {
                    DibujarPunto(e.GetPosition(Lienzo).X, e.GetPosition(Lienzo).Y, anchoPincel, Brushes.Black);
                }
                if (btnRectangulo.IsChecked == true)
                {

                    rectanguloTemporal.Width = e.GetPosition(Lienzo).X - Canvas.GetLeft(rectanguloTemporal);
                    rectanguloTemporal.Height = e.GetPosition(Lienzo).Y - Canvas.GetTop(rectanguloTemporal);

                }
                if (btnLinea.IsChecked == true)
                {
                    LineaTemporal.X2 = (int)e.GetPosition(Lienzo).X;
                    LineaTemporal.Y2 = (int)e.GetPosition(Lienzo).Y;
                }
                if (btnCirculo.IsChecked == true)
                {
                    //se aplica un pitagoras para el punto izquierdo superior y el radio
                    double radio = Math.Sqrt(Math.Pow((posX - centroCirculo.X), 2) + Math.Pow((posY - centroCirculo.Y), 2));
                    Canvas.SetLeft(circuloTemporal, centroCirculo.X - radio);
                    Canvas.SetTop(circuloTemporal, centroCirculo.Y - radio);

                    circuloTemporal.Width = radio * 2;
                    circuloTemporal.Height = radio * 2;
                }
                if (btnPoligono.IsChecked == true)
                {
                    LineaTemporal.X2 = posX;
                    LineaTemporal.Y2 = posY;                   
                }
                if (btnBorrador.IsChecked == true)
                {
                    DibujarPunto(e.GetPosition(Lienzo).X, e.GetPosition(Lienzo).Y, anchoPincel, Brushes.White);
                }    
            }
            
        }

        private void soltar(object sender, MouseButtonEventArgs e)
        {
            if (btnPoligono.IsChecked == true)
            {               
                LineaTemporal = new Line();
                LineaTemporal.X1 = posX;
                LineaTemporal.Y1 = posY;
                puntos.Add(new Point(posX, posY));
                LineaTemporal.X2 = posX;
                LineaTemporal.Y2 = posY;
                puntos.Add(new Point(posX, posY));
                LineaTemporal.Stroke = Brushes.Black;
                LineaTemporal.StrokeThickness = anchoPincel;
                Lienzo.Children.Add(LineaTemporal);
                Evaluardistancia();
            }
        }

        private void IniciarDibujado(object sender, MouseButtonEventArgs e)
        {
            if (btnPunto.IsChecked == true)
            {                
                    DibujarPunto(e.GetPosition(Lienzo).X, e.GetPosition(Lienzo).Y, anchoPincel, Brushes.Black);              
            }
            if (btnRectangulo.IsChecked == true)
            {

                rectanguloTemporal = new Rectangle();
                rectanguloTemporal.Stroke = Brushes.Black;
                rectanguloTemporal.StrokeThickness = anchoPincel;
                rectanguloTemporal.Fill = Brushes.Gray;

                rectanguloTemporal.Height = 0;
                rectanguloTemporal.Width = 0;

                Canvas.SetLeft(rectanguloTemporal, e.GetPosition(Lienzo).X);
                Canvas.SetTop(rectanguloTemporal, e.GetPosition(Lienzo).Y);

                Lienzo.Children.Add(rectanguloTemporal);

            }
            if (btnLinea.IsChecked == true)
            {
                LineaTemporal = new Line();
                LineaTemporal.X1 = (int)e.GetPosition(Lienzo).X;
                LineaTemporal.Y1 = (int)e.GetPosition(Lienzo).Y;
                LineaTemporal.Stroke = Brushes.Black;
                LineaTemporal.StrokeThickness = anchoPincel;
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
            if (btnPoligono.IsChecked == true)
            {
                if (flag)
                {
                    contador = 0;
                    puntos = new PointCollection();
                    puntos.Add(new Point(posX,posY));
                    contador++;
                    LineaTemporal = new Line();
                    LineaTemporal.X1 = (int)e.GetPosition(Lienzo).X;
                    LineaTemporal.Y1 = (int)e.GetPosition(Lienzo).Y;
                    LineaTemporal.Stroke = Brushes.Black;
                    LineaTemporal.StrokeThickness = anchoPincel;
                    Lienzo.Children.Add(LineaTemporal);
                    flag = false;
                }
                else
                {
                    LineaTemporal.X2 = posX;
                    LineaTemporal.Y2 = posY;
                    puntos.Add(new Point(posX, posY));
                    contador++;
                }
            }
            if (btnBorrador.IsChecked == true)
            {

            }
        }
        private void Evaluardistancia()
        {
            if (puntos.Count > 2)
            {
                double distancia =Math.Abs( Math.Sqrt(Math.Pow((puntos[puntos.Count - 1].X - puntos[0].X), 2) + Math.Pow((puntos[puntos.Count - 1].Y - puntos[0].Y), 2)));
                if (distancia < 15)
                {
                    poligonoTemporal = new Polygon();
                    poligonoTemporal.Points = puntos;
                    poligonoTemporal.Stroke = Brushes.Black;
                    poligonoTemporal.StrokeThickness = anchoPincel;                    
                    Lienzo.Children.RemoveRange((Lienzo.Children.Count-contador),Lienzo.Children.Count);
                    Lienzo.Children.Add(poligonoTemporal);
                    flag = true;
                }
            }
        }
    }
}
