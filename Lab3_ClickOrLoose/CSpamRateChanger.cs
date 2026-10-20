using System.Windows;
using System.Windows.Media;

namespace Lab3_ClickOrLoose
{
    public class CSpamRateChanger : CCollectable
    {
        public CSpamRateChanger(Point position, double size, double lifetime) : base(position, size, lifetime)
        {
            sprite.Fill = Brushes.HotPink;
        }

        public override bool OnClick(CPlayer player, CController controller, Point mousePosition)
        {
            if (!IsMouseOnObject(mousePosition)) return false;
            controller.DecreaseSpawnRate(0.05); // сферы появляются чаще
            return true;
        }
    }
}