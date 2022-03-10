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
        private void button11_Click(object sender, EventArgs e)
        {
            vi2.Items = new List<int> { 3, 4, 1, 5, 9, 8, 7 };
            textBox6.Clear();
            vi1.Stop();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            vi2.Items = new List<int> { 3, 4, 1, 5, 9, 8, 7 };
            textBox5.Clear();
            vi1.Stop();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (vi2.Animating) return;
            vi2.Log = textBox6;
            List<int> l = new List<int>(vi2.Items);
            for (int i = 0; i < l.Count; ++i)
                if (l[i] % 2 == 0)
                {
                    l.Insert(i + 1, l[i] / 2);
                    int x = l[i + 1];
                    vi2.Insert(i + 1, x);
                }
            vi2.AppendText(Environment.NewLine);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (vi2.Animating) return;
            vi2.Log = textBox5;
            List<int> l = new List<int>(vi2.Items);
            for (int i = l.Count - 1; i >= 0; --i)
                if (l[i] % 2 == 0)
                {
                    l.Insert(i + 1, l[i] / 2);
                    int x = l[i + 1];
                    vi2.Insert(i + 1, x);
                }
            vi2.AppendText(Environment.NewLine);
        }
    }
}