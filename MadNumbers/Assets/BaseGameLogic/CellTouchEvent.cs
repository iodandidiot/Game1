using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.BaseGameLogic
{
    public class CellTouchEvent
    {
        public List<List<CellParam>> cellParamList { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Number { get; set; }

    }
}
