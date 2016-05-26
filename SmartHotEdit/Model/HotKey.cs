using System.Windows.Forms;

namespace SmartHotEdit.Model
{
    public sealed class HotKey
    {
        public readonly Keys Hotkey;
        public readonly bool IsAlt;
        public readonly bool IsControl;
        public readonly bool IsShift;
        public readonly bool IsWin;

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