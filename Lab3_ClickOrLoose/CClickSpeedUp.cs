using System.Windows;
using System.Windows.Media;

namespace Lab3_ClickOrLoose
{
    public class CClickSpeedUp : CCollectable
    {
        public CClickSpeedUp(Point position, double size, double lifetime) : base(position, size, lifetime)
        {
            sprite.Fill = Brushes.Red;
        }

        public override bool OnClick(CPlayer player, CController controller, Point mousePosition)
        {
            if (!IsMouseOnObject(mousePosition)) return false;
            player.IncreaseSpeed(0.05); // уменьшаем КД на 50 мс
            return true;
        }
    }
}