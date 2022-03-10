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
        private void button13_Click(object sender, EventArgs e)
        {
            vsi1.Items = new List<int> { 47, 32, 51, 17, 34, 56, 75, 61, 19, 21 };
            vsi1.Stop();
            textBox7.Clear();
            textBox8.Clear();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            vsi1.nrswaps = 0;
            if (vsi1.Animating) return;
            List<int> l = new List<int>(vsi1.Items);
            for (int i = 0; i < l.Count - 1; ++i)
            {
                int imin = i, mn = l[i];
                for (int j = i + 1; j < l.Count; ++j)
                    if (l[j] < mn) { mn = l[j]; imin = j; textBox8.Text += l[j] + " <= " + mn; textBox8.AppendText(Environment.NewLine); }
                    else { textBox8.Text += l[j] + " >= " + mn; textBox8.AppendText(Environment.NewLine); }
                int aux = l[i]; l[i] = l[imin]; l[imin] = aux;
                if (i != imin) vsi1.Swap(i, imin);
            }
            textBox8.Text += Environment.NewLine;
            textBox8.Text += "Număr comparări: " + (textBox8.Lines.Length - 2) + Environment.NewLine + Environment.NewLine;
            vsi1.ShowSwaps();
        }
    }
}