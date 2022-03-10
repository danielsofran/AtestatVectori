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
        private void button5_Click(object sender, EventArgs e)
        {
            if (vs2.Animating) return;
            vs2.Log = textBox3;
            List<int> l = new List<int>(vs2.Items);
            for (int i = 0; i < l.Count; ++i)
            {
                //MessageBox.Show(l[i].ToString());
                if (l[i] % 2 == 0 || l[i] == 0)
                {
                    vs2.Erase(i);
                    l.RemoveAt(i);
                }
            }
            vs2.AppendText(Environment.NewLine);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (vs2.Animating) return;
            vs2.Log = textBox4;
            for (int i = vs2.Items.Count - 1; i >= 0; --i)
                if (vs2.Items[i] % 2 == 0)
                {
                    vs2.Erase(i);
                }
            vs2.AppendText(Environment.NewLine);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            vs2.Items = new List<int> { 3, 4, 2, 1, 6, 5, 9, 8, 2, 7, 0 };
            textBox3.Clear();
            vs2.Stop();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            vs2.Items = new List<int> { 3, 4, 2, 1, 6, 5, 9, 8, 2, 7, 0 };
            textBox4.Clear();
            vs2.Stop();
        }
    }
}