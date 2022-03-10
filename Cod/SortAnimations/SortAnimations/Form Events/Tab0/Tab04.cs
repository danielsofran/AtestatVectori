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
        public bool UseNrElem { get => checkBox7.Checked; }
        public string Separator
        {
            get
            {
                string space;
                if (comboBox1.SelectedIndex == 0) space = " ";
                else if (comboBox1.SelectedIndex == 1) space = Environment.NewLine;
                else space = comboBox1.Text;
                return space;
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] lines = File.ReadAllLines(openFileDialog1.FileName);
                bool first = checkBox7.Checked;
                int val;
                List<char> sep = new List<char>();
                List<int> rez = new List<int>();
                for (char i = (char)0; i < 256; ++i)
                    if (!char.IsDigit(i))
                        sep.Add(i);
                foreach (string line in lines)
                {
                    string[] cv = line.Trim().Split(sep.ToArray());
                    foreach (string s in cv)
                    {
                        val = int.Parse(s);
                        if (!first) rez.Add(val);
                        else first = false;
                    }
                }
                v.Items = rez;
            }

        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string space;
                if (comboBox1.SelectedIndex == 0) space = " ";
                else if (comboBox1.SelectedIndex == 1) space = "\n";
                else space = comboBox1.Text;
                StreamWriter sr = new StreamWriter(saveFileDialog1.FileName);
                if (checkBox7.Checked)
                    sr.WriteLine(v.Items.Count);
                foreach (int i in v.Items)
                    sr.Write(i + space);
                sr.Close();
            }
        }

        private void button49_Click(object sender, EventArgs e)
        {
            string space;
            if (comboBox1.SelectedIndex == 0) space = " ";
            else if (comboBox1.SelectedIndex == 1) space = Environment.NewLine;
            else space = comboBox1.Text;
            if (checkBox7.Checked)
            {
                v.Log.AppendText(v.Items.Count.ToString());
                v.Log.AppendText(Environment.NewLine);
            }
            foreach (int i in v.Items)
                v.Log.AppendText(i + space);
            v.Log.AppendText(Environment.NewLine); v.Log.AppendText(Environment.NewLine);
        }
    }
}
