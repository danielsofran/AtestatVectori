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
        private void button17_Click(object sender, EventArgs e)
        {
            vsb.Items = new List<int> { 47, 32, 51, 17, 34, 56, 75, 61, 19, 21 };
            vsb.Stop();
            textBox12.Clear();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            vsb.nrswaps = 0;
            if (vsb.Animating) return;
            List<int> l = new List<int>(vsb.Items);
            bool sortat;
            do
            {
                sortat = true;
                for (int i = 0; i < l.Count - 1; ++i)
                    if (l[i] > l[i + 1])
                    {
                        int aux = l[i]; l[i] = l[i + 1]; l[i + 1] = aux;
                        vsb.Swap(i, i + 1);
                        sortat = false;
                    }
            } while (!sortat);
            vsb.ShowSwaps();
        }
    }
}