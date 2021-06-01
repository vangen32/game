using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piatnashky
{
    class Map
    {
        public MapItem[,] GameField { get; set; }

        private int freeX = 0;
        private int freeY = 0;
        private int size;
        public int nullableItem = 0;
        public void StartGame(int playGroundSize, int playGroundPxSize)
        {
            this.nullableItem = 0;
            this.size = playGroundSize;
            int picPxSize = playGroundPxSize / this.size;
            int counter = 0;
            this.GameField = new MapItem[this.size, this.size];
            for(int i=0; i< this.size; i++)
            {
                for(int k = 0; k < this.size; k++)
                {
                    counter++;
                    var a = new Item(counter, k, i);
                    
                    Image temp = Properties.Resources.original;
                    Bitmap src = new Bitmap(temp, playGroundPxSize, playGroundPxSize);
                    Rectangle rect = new Rectangle(new Point(picPxSize*k, picPxSize*i), new Size(picPxSize, picPxSize));
                    a.img = Picture.CutImage(src, rect);

                    this.GameField[i, k] = new MapItem(counter, k, i, a);
                    if (counter == GameField.Length)
                    {
                        this.freeX = k;
                        this.freeY = i;
                        this.nullableItem = counter;
                    }
                }
            }
            this.size = playGroundSize;
            this.MixIt();
        }

        private void turnOnY(int newFreeY)
        {
            var a = GameField[freeY, freeX].isFree;
            GameField[freeY, freeX].isFree = GameField[newFreeY, freeX].isFree;
            GameField[freeY, freeX].isFree.ChangePos(freeX, newFreeY);
            a.ChangePos(freeY, freeX);
            GameField[newFreeY, freeX].isFree = a;
            freeY = newFreeY;
        }
        private void turnOnX(int newFreeX)
        {
            var a = GameField[freeY, freeX].isFree;
            GameField[freeY, freeX].isFree = GameField[freeY, newFreeX].isFree;
            GameField[freeY, freeX].isFree.ChangePos(newFreeX, freeY);
            a.ChangePos(freeY, freeX);
            GameField[freeY, newFreeX].isFree = a;
            freeX = newFreeX;
        }
        private void onDownClick(int item_num)
        {
            int newFreeY = freeY - 1;
            if (newFreeY < 0 || GameField[newFreeY, freeX].isFree.Item_num != item_num)
                return;
            if (GameField[freeY, freeX].isFree.Item_num == this.nullableItem)
            {
                this.turnOnY(newFreeY);
            }
        }
        private void onUpClick(int item_num)
        {
            int newFreeY = freeY + 1;
            if (newFreeY >= size || GameField[newFreeY, freeX].isFree.Item_num != item_num)
                return;
            if (GameField[freeY, freeX].isFree.Item_num == this.nullableItem)
            {
                this.turnOnY(newFreeY);
            }
        }
        private void onRightClick(int item_num)
        {
            int newFreeX = freeX - 1;
            if (newFreeX < 0 || GameField[freeY, newFreeX].isFree.Item_num != item_num)
                return;
            if (GameField[freeY, freeX].isFree.Item_num == this.nullableItem)
            {
                this.turnOnX(newFreeX);
            }
        }
        private void onLeftClick(int item_num)
        {
            int newFreeX = freeX + 1;
            if (newFreeX >= size || GameField[freeY, newFreeX].isFree.Item_num != item_num)
                return;
            if (GameField[freeY, freeX].isFree.Item_num == this.nullableItem)
            {
                this.turnOnX(newFreeX);
            }
        }

        public void doTurn(int x, int y, int Item_num)
        {   
            if (y==this.freeY)
            {
                switch (x < this.freeX)
                {
                    case true:
                        this.onRightClick(Item_num);
                        break;
                    case false:
                        this.onLeftClick(Item_num);
                        break;
                }
            }
            else if (x == this.freeX)
            {
                switch (y < this.freeY)
                {
                    case true:
                        this.onDownClick(Item_num);
                        break;
                    case false:
                        this.onUpClick(Item_num);
                        break;
                }
            }
        }

        public void isWin()
        {
            for (int i = 0; i < size; i++)
                for (int k = 0; k < size; k++)
                    if(this.GameField[i,k].isFree.Item_num != this.GameField[i, k].Item_num)
                        return;
            System.Windows.Forms.MessageBox.Show("You win");
        }
        private void MixIt()
        {
            var rnd = new Random();
            int id = 0;
            for (int i = 0; i < (200*this.size); i++)
            {
                var moveble = this.isMoveble();
                var a = rnd.Next(0, moveble.Count);
                if (moveble[a].Item_num != id)
                {
                    var b = rnd.Next(1, 5);
                    switch (b)
                    {
                        case 1:
                            this.onUpClick(moveble[a].Item_num);
                            break;
                        case 2:
                            this.onLeftClick(moveble[a].Item_num);
                            break;
                        case 3:
                            this.onRightClick(moveble[a].Item_num);
                            break;
                        case 4:
                            this.onDownClick(moveble[a].Item_num);
                            break;
                    }
                    id = moveble[a].Item_num;
                }
                else
                {
                    i--;
                }
            }
        }
        public List<Item> isMoveble()
        {
            List<Item> list = new List<Item>();
            int leftX = freeX - 1;
            int rightX = freeX + 1;
            int upY = freeY - 1;
            int downY = freeY + 1;
            if (leftX >= 0)
            {
                list.Add(this.GameField[freeY, leftX].isFree);
            }
            if (rightX < size)
            {
                list.Add(this.GameField[freeY, rightX].isFree);
            }
            if (upY >= 0)
            {
                list.Add(this.GameField[upY, freeX].isFree);
            }
            if (downY < size)
            {
                list.Add(this.GameField[downY, freeX].isFree);
            }
            return list;
        }
    }
}
