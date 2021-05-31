using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piatnashky
{
    public class Item
    {
        public Item(int ItemNum, int X, int Y)
        {
            this.Item_num = ItemNum;
            this.X = X;
            this.Y = Y;
        }
        public int Item_num { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public void ChangePos(int newX, int newY){
            this.X = newX;
            this.Y = newY;
        }
    }
}
