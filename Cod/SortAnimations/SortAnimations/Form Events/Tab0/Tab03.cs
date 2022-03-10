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
        private void button28_Click(object sender, EventArgs e)
        {
            v.nrswaps = 0;
            List<int> l = new List<int>(v.Items);
            for (int i = 0; i < l.Count - 1; ++i)
            {
                int imin = i, mn = l[i];
                for (int j = i + 1; j < l.Count; ++j)
                    if (l[j] < mn) { mn = l[j]; imin = j; }
                int aux = l[i]; l[i] = l[imin]; l[imin] = aux;
                if (i != imin) v.Swap(i, imin);
            }
            if (checkBox8.Checked) v.ShowSwaps();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            v.nrswaps = 0;
            List<int> l = new List<int>(v.Items);
            for (int i = 0; i < l.Count - 1; ++i)
                for (int j = i + 1; j < l.Count; ++j)
                {
                    if (l[i] > l[j])
                    {
                        int aux = l[i]; l[i] = l[j]; l[j] = aux;
                        v.Swap(i, j);
                    }
                }
            if (checkBox8.Checked) v.ShowSwaps();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            v.nrswaps = 0;
            List<int> l = new List<int>(v.Items);
            bool sortat;
            do
            {
                sortat = true;
                for (int i = 0; i < l.Count - 1; ++i)
                    if (l[i] > l[i + 1])
                    {
                        int aux = l[i]; l[i] = l[i + 1]; l[i + 1] = aux;
                        v.Swap(i, i + 1);
                        sortat = false;
                    }
            } while (!sortat);
            if (checkBox8.Checked) v.ShowSwaps();
        }

        private void button48_Click(object sender, EventArgs e)
        {
            v.nrswaps = 0;
            List<int> l = new List<int>(v.Items);
            int n = l.Count, nrop = 0;
            v.SetColor(OwnColors.Marked, v.Pause / 2, 0);
            for (int i = 1; i < n; ++i)
            {
                int x = l[i];
                ++nrop;
                v.Erase(i);
                l.RemoveAt(i);
                bool pus = false;
                for (int j = 0; j < i; ++j)
                {
                    if (l[j] > x)
                    {
                        l.Insert(j, x);
                        v.Insert(j, x);
                        ++nrop;
                        v.SetColor(OwnColors.Marked, v.Pause / 2, j);
                        pus = true;
                        break;
                    }
                }
                if (!pus)
                {
                    ++nrop;
                    l.Insert(i, x);
                    v.Insert(i, x);
                    v.SetColor(OwnColors.Marked, v.Pause / 2, i);
                }
            }
            if (checkBox8.Checked)
                v.AtTimeAction(delegate
                {
                    v.Log.AppendText(Environment.NewLine);
                    v.Log.AppendText("Număr de apeluri de funcții: " + nrop);
                    v.Log.AppendText(Environment.NewLine); v.Log.AppendText(Environment.NewLine);
                    v.Sleep(2000);
                    v.ColorDefault();
                });
        }
    }
}
