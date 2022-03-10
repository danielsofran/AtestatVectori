using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortAnimations
{
    partial class FormMain
    {
        private void button47_Click(object sender, EventArgs e)
        {
            //colorBox.BackColor = Color.Yellow;
            colorBox1.BackColor = Color.Yellow;
            colorBox2.BackColor = Color.LimeGreen;
            colorBox3.BackColor = Color.Red;
            colorBox4.BackColor = Color.DarkOrange;
        }

        private void colorBox1_Click(object sender, EventArgs e)
        {
            PictureBox box = sender as PictureBox;
            if (box == null) return;
            if (box.Name.EndsWith("1")) OwnColors.Select = box.BackColor;
            else if (box.Name.EndsWith("2")) OwnColors.Insert = box.BackColor;
            else if (box.Name.EndsWith("3")) OwnColors.Erase = box.BackColor;
            else if (box.Name.EndsWith("4")) OwnColors.Marked = box.BackColor;
            else v.SelectionColor = box.BackColor;
        }
        private void colorBox_Click(object sender, EventArgs e)
        {
            PictureBox box = sender as PictureBox;
            colorDialog1.Color = box.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                box.BackColor = colorDialog1.Color;
        }
    }
}
