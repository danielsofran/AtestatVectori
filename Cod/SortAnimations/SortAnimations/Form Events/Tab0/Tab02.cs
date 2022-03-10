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
        private void button34_Click(object sender, EventArgs e)
        {
            if (v.Items.Count == 0) return;
            int p = v.Pause;
            v.Pause /= 10;
            v.Pop_back();
            v.AtTimeAction(() => v.Pause = p);
            v.Log.Text += "S-a eliminat ultimul element: " + v.Items.Last();
            v.Log.Text += Environment.NewLine;
            v.ToEnd();
        }

        private void button36_Click(object sender, EventArgs e)
        {
            if (v.Items.Count == 0) return;
            int p = v.Pause;
            v.Pause /= 10;
            v.Pop_front();
            v.AtTimeAction(() => v.Pause = p);
            v.Log.Text += "S-a eliminat primul element: " + v.Items.First();
            v.Log.Text += Environment.NewLine;
            v.ToEnd();
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if (v.Items.Count == 0) return;
            int p = v.Pause;
            v.Pause /= 10;
            v.Select(v.Items.Count - 1);
            v.AtTimeAction(() => v.Pause = p);
            v.Log.Text += "Ultimul element este: " + v.Items.Last();
            v.Log.Text += Environment.NewLine;
            v.ToEnd();
        }

        private void button37_Click(object sender, EventArgs e)
        {
            if (v.Items.Count == 0) return;
            int p = v.Pause;
            v.Pause /= 10;
            v.Select(0);
            v.AtTimeAction(() => v.Pause = p);
            v.Log.Text += "Primul element este: " + v.Items.First();
            v.Log.Text += Environment.NewLine;
            v.ToEnd();
        }

        private void button39_Click(object sender, EventArgs e)
        {
            int s = 0;
            if (v.Items.Count == 0)
            {
                v.Log.Text += ("Vectorul este vid. Suma elementelor este nulă.");
                v.Log.Text += Environment.NewLine; v.Log.Text += Environment.NewLine;
            }
            Color c = OwnColors.Select;
            int p = v.Pause;
            OwnColors.Select = Color.LimeGreen;
            v.Pause /= 10;
            for (int i = 0; i < v.Items.Count - 1; ++i)
            {
                s += v.Items[i];
                v.Select(i);
                v.Log.Text += (v.Items[i] + "+");
            }
            s += v.Items.Last();
            v.Select(v.Items.Count - 1);
            v.Log.Text += (String.Format("{0}= {1}", v.Items.Last(), s));
            v.Log.Text += (Environment.NewLine); v.Log.Text += (Environment.NewLine);
            v.AtTimeAction(delegate { OwnColors.Select = c; v.Pause = p; });
            v.ToEnd();
        }

        private void button42_Click(object sender, EventArgs e)
        {
            if (v.Items.Count == 0)
            {
                v.Log.Text += ("Vectorul este vid. Media elementelor este nulă.");
                v.Log.Text += (Environment.NewLine); v.Log.Text += (Environment.NewLine);
            }
            double s = 0;
            Color c = OwnColors.Select;
            int p = v.Pause;
            OwnColors.Select = Color.LimeGreen;
            v.Pause /= 10;
            v.Log.Text += ("(");
            for (int i = 0; i < v.Items.Count - 1; ++i)
            {
                s += (double)v.Items[i];
                v.Select(i);
                v.Log.Text += (v.Items[i] + "+");
            }
            s += (double)v.Items.Last();
            s /= (double)v.Items.Count;
            v.Select(v.Items.Count - 1);
            v.Log.Text += (v.Items.Last() + ")/" + v.Items.Count);
            //v.Sleep(20);
            v.Log.Text += (String.Format("= {0:0.00} - media elementelor", s));
            v.Log.Text += (Environment.NewLine); v.Log.Text += (Environment.NewLine);
            v.AtTimeAction(delegate { OwnColors.Select = c; v.Pause = p; });
            v.ToEnd();
        }

        private void button40_Click(object sender, EventArgs e)
        {
            if (v.Items.Count == 0)
            {
                v.Log.Text += ("Vectorul este vid. Nu există element minim.");
                v.Log.Text += (Environment.NewLine); v.Log.Text += (Environment.NewLine);
            }
            int mn = 2000000000;
            Color c = v[0].BackColor;
            int p = v.Pause;
            v.Pause /= 10;
            for (int i = 0; i < v.Items.Count; ++i)
            {
                int x = v.Items[i];
                v.Select(i);
                if (x < mn)
                {
                    //reset
                    //for (int j = 0; j < i; ++j)
                        //v.SetColor(c, 1, j);
                    //set
                    mn = x;
                    //v.SetColor(OwnColors.Marked, 50, i);
                }
                //else if (x == mn) v.SetColor(OwnColors.Marked, 1, i);
            }
            v.Log.Text += (String.Format("Elementul minim este: {0}", mn));
            v.Log.Text += (Environment.NewLine); v.Log.Text += (Environment.NewLine);
            //v.Sleep(200);
            v.AtTimeAction(delegate { v.Pause = p; });
            v.ToEnd();
        }

        private void button41_Click(object sender, EventArgs e)
        {
            if (v.Items.Count == 0)
            {
                v.Log.Text += ("Vectorul este vid. Nu există element maxim.");
                v.Log.Text += (Environment.NewLine); v.Log.Text += (Environment.NewLine);
            }
            int mx = -1;
            Color c = v[0].BackColor;
            int p = v.Pause;
            v.Pause /= 10;
            for (int i = 0; i < v.Items.Count; ++i)
            {
                int x = v.Items[i];
                v.Select(i);
                if (x > mx)
                {
                    //reset
                    //for (int j = 0; j < i; ++j)
                        //v.SetColor(c, 1, j);
                    //set
                    mx = x;
                    //v.SetColor(OwnColors.Marked, 50, i);
                }
                //else if (x == mx) v.SetColor(OwnColors.Marked, 50, i);
            }
            v.Log.Text += (String.Format("Elementul maxim este: {0}", mx));
            v.Log.Text += (Environment.NewLine); v.Log.Text += (Environment.NewLine);
            //v.Sleep(200);
            v.AtTimeAction(delegate { v.Pause = p; });
            v.ToEnd();
        }
    }
}
