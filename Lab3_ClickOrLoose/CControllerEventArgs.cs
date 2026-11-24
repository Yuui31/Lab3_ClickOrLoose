using System;
using System.Windows.Shapes;

namespace Lab3_ClickOrLoose
{
    public class CControllerEventArgs : EventArgs
    {
        public Ellipse sprite;
        public string msg = "";

        public CControllerEventArgs(Ellipse sprite)
        {
            this.sprite = sprite;
        }
    }
}