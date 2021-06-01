using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piatnashky
{
    static class Picture
    {
        public static Bitmap CutImage(Bitmap src, Rectangle rect)
        {

            Bitmap bmp = new Bitmap(src.Width, src.Height); //создаем битмап

            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(src, 0, 0, rect, GraphicsUnit.Pixel); //перерисовываем с источника по координатам

            return bmp;

           
        }
       
    }
}
