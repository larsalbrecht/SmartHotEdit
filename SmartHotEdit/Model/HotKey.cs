using System.Windows.Forms;

namespace SmartHotEdit.Model
{
    public sealed class HotKey
    {
        public Keys Hotkey;
        public bool IsAlt;
        public bool IsControl;
        public bool IsShift;
        public bool IsWin;


        public HotKey()
        {
        }

        public HotKey(Keys hotkey, bool isShift, bool isControl, bool isAlt, bool isWin)
        {
            this.Hotkey = hotkey;
            this.IsShift = isShift;
            this.IsControl = isControl;
            this.IsAlt = isAlt;
            this.IsWin = isWin;
        }
    }
}