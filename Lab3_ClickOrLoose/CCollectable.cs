using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab3_ClickOrLoose
{
    public abstract class CCollectable
    {
        // ТУТ ДОЛЖНО БЫТЬ СЛОВО protected, а не private!
        protected Point position;
        protected Size size;
        protected double lifetime;
        protected Ellipse sprite;

        public CCollectable(Point position, double size, double lifetime)
        {
            this.position = position;
            this.size = new Size(size, size);
            this.lifetime = lifetime;

            sprite = new Ellipse();
            sprite.StrokeThickness = 2;
            sprite.Stroke = Brushes.Black;
            sprite.Width = this.size.Width;
            sprite.Height = this.size.Height;
            sprite.RenderTransform = new TranslateTransform(position.X, position.Y);
        }

        public Ellipse GetSprite() { return sprite; }

        public bool IsMouseOnObject(Point mousePosition)
        {
            return mousePosition.X >= position.X &&
                   mousePosition.X <= position.X + size.Width &&
                   mousePosition.Y >= position.Y &&
                   mousePosition.Y <= position.Y + size.Height;
        }

        public bool UpdateLifetime(double delta)
        {
            lifetime -= delta;
            return lifetime <= 0;
        }

        // Вот этот метод не находился!
        public void AddLifetime(double amount)
        {
            lifetime += amount;
        }

        // И вот этот метод тоже не находился!
        public abstract bool OnClick(CPlayer player, CController controller, Point mousePosition);
    }
}