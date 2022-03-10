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
        private void button33_Click(object sender, EventArgs e) => v.Stop();

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void button38_Click(object sender, EventArgs e)
        {
            textBox11.Clear();
        }

        private void button43_Click(object sender, EventArgs e)
        {
            //LoadControls(@"C:\Users\SOFRAN ROMULUS\Desktop\c1.txt");
            tabControl1.SelectedTab = tabPage12;
        }

        private void veziExplicațieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Button b = ((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as Button;
            if (b == button26) tabControl1.SelectedIndex = 2;
            else if (b.Text.Contains("Șterge") || b.Text.Contains("Inserează") || b.Text.Contains("Adaugă"))
                tabControl1.SelectedIndex = 1;
            else if (b.Text.Contains("Directă")) tabControl1.SelectedIndex = 4;
            else if (b.Text.Contains("Indirectă")) tabControl1.SelectedIndex = 5;
            else if (b.Text.Contains("Bule")) tabControl1.SelectedIndex = 6;
            else if (b.Text.Contains("Inserție")) tabControl1.SelectedIndex = 7;
        }
    }
}