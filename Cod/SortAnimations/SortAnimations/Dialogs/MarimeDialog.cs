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
    public partial class MarimeDialog : Form
    {
        public Size ItemSize { get => vector.ItemSize; set { vector.ItemSize = value; numericUpDown1.Value = value.Height; } }
        //public int FontSize { get => (int)vector.Font.SizeInPoints; set => vector.Font = vector.FntSize(value); }
        public Padding Spacing { get => vector.Padding; set { vector.Padding = value; numericUpDown3.Value = value.Left; } }

        public Font Font2 { get=>vector.Font; set { vector.Font = value; numericUpDown2.Value = (decimal)value.SizeInPoints; } }

        public Color Fundal { get => pictureBox1.BackColor; set { vector.BackColor = value; pictureBox1.BackColor = value; } }
        public Color TextColor { get => pictureBox2.BackColor; set { for (int i = 0; i < vector.Items.Count; ++i) vector[i].ForeColor = value; pictureBox2.BackColor = value; } }
        public Color ElemColor { get => pictureBox3.BackColor; set { for (int i = 0; i < vector.Items.Count; ++i) vector[i].BackColor = value; pictureBox3.BackColor = value; } }


        VectorControl vector;
        public MarimeDialog()
        {
            InitializeComponent();
            vector = new VectorControl();
            vector.Items = new List<int> { 1, 2, 3};
            vector.Anchor = AnchorStyles.None;
            vector.ContextMenuStrip = null;
            panel1.Controls.Add(vector);
            Center(vector);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            vector.ItemSize = new Size((int)numericUpDown1.Value, (int)numericUpDown1.Value);
            vector.Refresh();
            Center(vector);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            vector.Font = vector.FntSize((int)numericUpDown2.Value);
            Center(vector);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            vector.Padding = new Padding((int)numericUpDown3.Value);
            Center(vector);
        }

        public static void Center(Control ctrlToCenter)
        {
            ctrlToCenter.Left = (ctrlToCenter.Parent.Width - ctrlToCenter.Width) / 2;
            ctrlToCenter.Top = (ctrlToCenter.Parent.Height - ctrlToCenter.Height) / 2;
        }

        public static void CenterLeft(Control ctrlToCenter)
        {
            ctrlToCenter.Left = (ctrlToCenter.Parent.Width - ctrlToCenter.Width) / 2;
        }

        public static void CenterTop(Control ctrlToCenter)
        {
            ctrlToCenter.Top = (ctrlToCenter.Parent.Height - ctrlToCenter.Height) / 2;
        }

        private void MarimeDialog_Resize(object sender, EventArgs e)
        {
            panel1.Height = splitContainer1.Top - 10;
            splitContainer1.Width = this.Width - 16;
        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value = numericUpDown1.Maximum;
            numericUpDown2.Value = numericUpDown2.Maximum;
            numericUpDown3.Value = numericUpDown3.Maximum;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value = numericUpDown1.Minimum;
            numericUpDown2.Value = numericUpDown2.Minimum;
            numericUpDown3.Value = numericUpDown3.Minimum;
        }

        private void schimbăCuloareaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox box = ((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as PictureBox;
            if (colorDialog1.ShowDialog() == DialogResult.OK) box.BackColor = colorDialog1.Color;
        }

        private void pictureBox1_BackColorChanged(object sender, EventArgs e)
        {
            vector.BackColor = pictureBox1.BackColor;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < vector.Items.Count; ++i)
                vector[i].ForeColor = pictureBox2.BackColor;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < vector.Items.Count; ++i)
                vector[i].BackColor = pictureBox3.BackColor;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox box = sender as PictureBox;
            if (colorDialog1.ShowDialog() == DialogResult.OK) box.BackColor = colorDialog1.Color;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value = 50;
            numericUpDown2.Value = 20;
            numericUpDown3.Value = 5;
        }
    }
}
