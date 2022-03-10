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
        private void button25_Click(object sender, EventArgs e)
        {
            if ((int)numericUpDown4.Value < v.Items.Count && numericUpDown4.Value >= 0)
                v.Erase((int)numericUpDown4.Value);
            else MessageBox.Show("Index invalid!\nOperația nu va avea loc.", "Atenție!", MessageBoxButtons.OKCancel);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < v.Items.Count; --i)
                if (v.Items[i] == numericUpDown5.Value)
                {
                    v.Erase(i);
                }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if ((int)numericUpDown4.Value <= v.Items.Count)
                v.Insert((int)numericUpDown4.Value, (int)numericUpDown5.Value);
            else MessageBox.Show("Index invalid!\nOperația nu va avea loc.", "Atenție!", MessageBoxButtons.OKCancel);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            v.Add((int)numericUpDown5.Value);
        }
    }
}
