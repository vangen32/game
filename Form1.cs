using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Piatnashky
{
    public partial class Form1 : Form
    {
        Map map;
        int turn = 0;
        int size = 4;
        public Form1()
        {
            InitializeComponent();
            StartGame((int)this.numericUpDown1.Value);
        }

        #region Methods
        private void StartGame(int size)
        {
            this.map = new Map();
            map.StartGame((int)this.numericUpDown1.Value, this.panelPlayZone.Width);
            AfterTurn();
        }

        private void AfterTurn()
        {
            this.panelPlayZone.Controls.Clear();
            int butSize = this.panelPlayZone.Width / this.size;
            foreach (MapItem item in map.GameField)
            {
                if (item.isFree.Item_num != map.nullableItem)
                {
                    var picture = new PictureBox();
                    picture.Image = item.isFree.img;
                    picture.Size = new Size(butSize, butSize);
                    picture.Location = new Point(item.X * butSize, item.Y * butSize);
                    //picture.Text = item.isFree.Item_num.ToString();
                    picture.MouseClick += this.onButClick;
                    picture.TabIndex = 0;
                    picture.Tag = $"{item.X}, {item.Y}, {item.isFree.Item_num}";
                    this.panelPlayZone.Controls.Add(picture);
                }
            }
            this.panelPlayZone.Refresh();
        }
        #endregion
       
        private void onButClick(object sender, MouseEventArgs e)
        {
            string[] loc = (sender as PictureBox).Tag.ToString().Split(',');
            int x = Int32.Parse(loc[0].Trim());
            int y = Int32.Parse(loc[1].Trim());
            int item_num = Int32.Parse(loc[2].Trim());
            map.doTurn(x, y, item_num);
            this.AfterTurn();
            map.isWin();
            this.turn++;
            this.labelTurn.Text = this.turn.ToString();
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.size = (int)this.numericUpDown1.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            map.StartGame((int)this.numericUpDown1.Value, this.panelPlayZone.Width);
            AfterTurn();
            this.labelTurn.Text = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            var a = Properties.Resources.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.CurrentCulture, true, false);
        }
    }
}
