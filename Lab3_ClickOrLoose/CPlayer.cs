using System.Windows;

namespace Lab3_ClickOrLoose
{
    public class CPlayer
    {
        private CCountdownTimer countdownTimer;

        public CPlayer(double timeBeforeClick)
        {
            countdownTimer = new CCountdownTimer(timeBeforeClick);
        }

        public void Update(double delta)
        {
            countdownTimer.Update(delta);
        }

        public void MouseClick(Point mousePosition, CController controller)
        {
            // Если перезарядка не прошла — клик игнорируется
            if (!countdownTimer.IsReady()) return;

            // Передаем клик в контроллер. Он проверит, попали ли мы по сфере.
            if (controller.HandleClick(this, mousePosition))
            {
                // Если попали — запускаем перезарядку заново
                countdownTimer.Reset();
            }
        }

        // Этот метод вызовет красная сфера
        public void IncreaseSpeed(double speedModifier)
        {
            countdownTimer.DecreaseTargetTime(speedModifier);
        }
    }
}