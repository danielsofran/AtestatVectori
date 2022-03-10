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
        private void button1_Click(object sender, EventArgs e)
        {
            if (vs1.Animating) return;
            for (int i = (int)numericUpDown1.Value; i < vs1.Items.Count - 1; ++i)
            {
                vs1.Select(i, i + 1);
                vs1.SetValue(i, vs1.Items[i + 1]);
            }
            vs1.Pop_back();
            vs1.AtTimeAction(delegate { vsi.Pop_back(); vs1.Log.AppendText(Environment.NewLine); vs1.Log.AppendText(Environment.NewLine); });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            vs1.Items = new List<int> { 2, 3, 7, 4, 1, 5, 9 };
            vsi.Items = new List<int> { 0, 1, 2, 3, 4, 5, 6 };
            textBox1.Clear();
            vs1.Stop();
            vsi.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            vi1.Items = new List<int> { 2, 3, 1, 5, 9 };
            vii.Items = new List<int> { 0, 1, 2, 3, 4 };
            textBox2.Clear();
            vi1.Stop();
            vii.Stop();
            button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (vi1.Animating) return;
            int n = vi1.Items.Count;
            vi1[(int)numericUpDown2.Value].BackColor = OwnColors.Insert;
            vi1.Add(vi1.Items[vi1.Items.Count - 1], false); // !??!?!?!?!?!??!?
            vii.Add(vii.Items.Count);
            vi1.AtTimeAction(delegate {
                vi1.Log.AppendText(string.Format("v[{0}] = {1}", n, vi1.Items[n - 1]));
                vi1.Log.AppendText(Environment.NewLine);
                vi1.ColorDefault();
                vii.ColorDefault();
            });
            vi1.Sleep(100); vii.Sleep(100);
            vi1.InsertSwaps((int)numericUpDown2.Value);
            vi1.FinalizeInsert((int)numericUpDown2.Value, (int)numericUpDown3.Value);
            vi1.AppendText(Environment.NewLine);
        }
    }
}
