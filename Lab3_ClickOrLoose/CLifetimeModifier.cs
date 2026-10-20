using System.Windows;
using System.Windows.Media;

namespace Lab3_ClickOrLoose
{
    public class CLifetimeModifier : CCollectable
    {
        public CLifetimeModifier(Point position, double size, double lifetime) : base(position, size, lifetime)
        {
            sprite.Fill = Brushes.Yellow;
        }

        public override bool OnClick(CPlayer player, CController controller, Point mousePosition)
        {
            if (!IsMouseOnObject(mousePosition)) return false;
            controller.IncreaseAllLifetimes(1.0); // добавляем +1 сек всем сферам на экране
            return true;
        }
    }
}