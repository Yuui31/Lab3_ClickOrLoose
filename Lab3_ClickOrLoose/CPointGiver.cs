using System.Windows;
using System.Windows.Media;

namespace Lab3_ClickOrLoose
{
    public class CPointGiver : CCollectable
    {
        private double pointsValue;

        // Вызов base(...) передает данные в конструктор родителя
        public CPointGiver(Point position, double size, double lifetime) : base(position, size, lifetime)
        {
            sprite.Fill = Brushes.BlueViolet;
            pointsValue = ((1 / this.size.Width) / lifetime) * 1000;
        }

        public override bool OnClick(CPlayer player, CController controller, Point mousePosition)
        {
            if (!IsMouseOnObject(mousePosition)) return false;
            controller.PointsIncrease(pointsValue);
            return true;
        }
    }
}