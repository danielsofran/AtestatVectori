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
        private void button45_Click(object sender, EventArgs e)
        {
            vi.Stop();
            vi.AtTimeAction(delegate { vi.Items = new List<int> { 47, 32, 51, 17, 34, 56, 75, 61, 19, 21 }; });
            vi.ColorDefault();
            textBox13.Clear();
            textBox14.Clear();
        }

        private void button44_Click(object sender, EventArgs e)
        {
            vi.nrswaps = 0;
            List<int> l = new List<int>(vi.Items);
            int n = l.Count, nrop = 0, nrcomp = 0;
            vi.SetColor(OwnColors.Marked, 50, 0);
            for (int i = 1; i < n; ++i)
            {
                int x = l[i];
                ++nrop;
                vi.Erase(i);
                l.RemoveAt(i);
                bool pus = false;
                for (int j = 0; j < i; ++j)
                {
                    if (l[j] > x)
                    {
                        l.Insert(j, x);
                        vi.Insert(j, x);
                        ++nrop; ++nrcomp;
                        vi.SetColor(OwnColors.Marked, 50, j);
                        pus = true;
                        textBox14.AppendText(l[j] + ">=" + x);
                        textBox14.AppendText(Environment.NewLine);
                        break;
                    }
                    else
                    {
                        ++nrcomp;
                        textBox14.AppendText(l[j] + "<=" + x);
                        textBox14.AppendText(Environment.NewLine);
                    }
                }
                if (!pus)
                {
                    ++nrop;
                    l.Insert(i, x);
                    vi.Insert(i, x);
                    vi.SetColor(OwnColors.Marked, 50, i);
                }
            }
            textBox14.AppendText(Environment.NewLine);
            textBox14.AppendText("Număr de comparări: " + nrcomp);
            textBox14.AppendText(Environment.NewLine); textBox14.AppendText(Environment.NewLine);
            vi.AtTimeAction(delegate
            {
                vi.Log.AppendText(Environment.NewLine);
                vi.Log.AppendText("Număr de apeluri de funcții: " + nrop);
                vi.Log.AppendText(Environment.NewLine); vi.Log.AppendText(Environment.NewLine);
                vi.Sleep(2000);
                vi.ColorDefault();
            });
        }
    }
}