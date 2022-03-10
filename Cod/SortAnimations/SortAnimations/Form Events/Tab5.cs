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
        private void button15_Click(object sender, EventArgs e)
        {
            vsd.Items = new List<int> { 47, 32, 51, 17, 34, 56, 75, 61, 19, 21 };
            vsd.Stop();
            textBox9.Clear();
            textBox10.Clear();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            vsd.nrswaps = 0;
            if (vsd.Animating) return;
            List<int> l = new List<int>(vsd.Items);
            for (int i = 0; i < l.Count - 1; ++i)
                for (int j = i + 1; j < l.Count; ++j)
                {
                    if (l[i] > l[j])
                    {
                        textBox9.Text += l[i] + " >= " + l[j];
                        textBox9.AppendText(Environment.NewLine);
                        int aux = l[i]; l[i] = l[j]; l[j] = aux;
                        vsd.Swap(i, j);
                    }
                    else
                    {
                        textBox9.Text += l[i] + " <= " + l[j];
                        textBox9.AppendText(Environment.NewLine);
                    }
                }
            vsd.ShowSwaps();
            textBox9.AppendText(Environment.NewLine);
            textBox9.Text += "Număr comparări: " + (textBox9.Lines.Length - 2) + Environment.NewLine + Environment.NewLine;
        }
    }
}