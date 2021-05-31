using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piatnashky
{
    public class MapItem : Item
    {   
        public MapItem(int ItemNum, int X, int Y, Item item) : base(ItemNum, X, Y)
        {
            isFree = item;
        }
        public Item isFree { get; set; }
    }
}
