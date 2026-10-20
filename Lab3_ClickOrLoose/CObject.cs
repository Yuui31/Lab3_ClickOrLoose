using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab3_ClickOrLoose // Убедись, что тут твой namespace!
{
    public class CObject
    {
        private Point position;
        private Size size;
        private double lifetime;
        private double pointsValue;
        private Ellipse sprite;

        public CObject(Point position, double size, double lifetime)
        {
            this.position = position;
            this.size = new Size(size, size);
            this.lifetime = lifetime;

            sprite = new Ellipse();
            sprite.Fill = Brushes.BlueViolet;
            sprite.StrokeThickness = 2;
            sprite.Stroke = Brushes.Black;
            sprite.Width = this.size.Width;
            sprite.Height = this.size.Height;
            sprite.RenderTransform = new TranslateTransform(position.X, position.Y);

            pointsValue = ((1 / this.size.Width) / lifetime) * 1000;
        }

        public Ellipse GetSprite() { return sprite; }
        public double GetPointsValue() { return pointsValue; }

        // Проверка: попал ли клик по кружку?
        public bool IsMouseOnObject(Point mousePosition)
        {
            return mousePosition.X >= position.X &&
                   mousePosition.X <= position.X + size.Width &&
                   mousePosition.Y >= position.Y &&
                   mousePosition.Y <= position.Y + size.Height;
        }

        // Обновление времени жизни (true = объект умер)
        public bool UpdateLifetime(double delta)
        {
            lifetime -= delta;
            return lifetime <= 0;
        }
    }
}