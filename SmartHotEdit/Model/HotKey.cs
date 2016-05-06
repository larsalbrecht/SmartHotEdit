using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartHotEdit.Model
{
    public sealed class HotKey
    {
        public Keys hotkey;
        public bool isAlt;
        public bool isControl;
        public bool isShift;
        public bool isWin;


        public HotKey()
        {

        }

        public HotKey(Keys hotkey, bool isShift, bool isControl, bool isAlt, bool isWin)
        {
            this.hotkey = hotkey;
            this.isShift = isShift;
            this.isControl = isControl;
            this.isAlt = isAlt;
            this.isWin = isWin;
        }
    }
}
