using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortAnimations.Dialogs
{
    public partial class VectorDialog : Form
    {
        public List<int> Vector
        {
            get
            {
                List<int> l = new List<int>();
                string[] s = textBox1.Text.Trim().Split();
                numericUpDown1.Maximum = s.Length;
                int sgn = 1;
                foreach (string x in s)
                    try { l.Add(sgn*int.Parse(x)); if (sgn == -1) sgn = 1; } 
                    catch (Exception ex) { if (x.Contains("-")) sgn = -1; }
                return l;
            }
            set
            {
                this.ActiveControl = null;
                numericUpDown1.Value = value.Count;
                foreach (int x in value)
                    textBox1.Text = textBox1.Text + x + ' ';
                textBox1.Text.TrimEnd();
                this.ActiveControl = null;
                //textBox1.SelectionLength = 0;
                textBox1.Select(textBox1.Text.Length-1, 0);
            }
        }

        int initw, initsw;
        public VectorDialog()
        {
            InitializeComponent();
            initw = textBox1.Width;
            initsw = this.Width;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !" -".Contains(e.KeyChar);
        }

        private void VectorDialog_Load(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Focus();
            string[] s = textBox1.Text.Trim().Split(); //MessageBox.Show(s.Length.ToString());
            try
            {
                textBox1.Select(textBox1.Text.IndexOf(s[(int)numericUpDown1.Value - 1]), 0);
            }
            catch (Exception ex) { numericUpDown1.Value--; }
        }

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            int w = this.Width - (initsw - initw);
            if(w>=label1.Width) textBox1.Width = w;
        }
    }
}
