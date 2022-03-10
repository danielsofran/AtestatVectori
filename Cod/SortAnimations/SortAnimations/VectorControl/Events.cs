using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SortAnimations.Dialogs;
using System.IO;

namespace SortAnimations
{
    partial class VectorControl
    {
        #region Events
        private void CLICK(object sender, EventArgs e)
        {
            FormMain form = this.ParentForm as FormMain;
            Button b = sender as Button;
            //MessageBox.Show(b.Text);
            if (b.BackColor != SelectionColor) { oldColors[b] = b.BackColor; b.BackColor = SelectionColor; }
            else
            {
                try { b.BackColor = oldColors[b]; }
                catch (Exception ex) { }
            }
        }

        private void micaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            foreach (ToolStripMenuItem x in (item.OwnerItem as ToolStripMenuItem).DropDownItems)
                x.Checked = false;
            foreach (ToolStripMenuItem x in spațiereToolStripMenuItem.DropDownItems)
                x.Checked = false;
            item.Checked = true;
            micăToolStripMenuItem.Checked = true;
            this.ItemSize = new Size(25, 25);
            this.Font = FntSize(11);
            this.Padding = new Padding(3);
            ReOrder("size");
        }

        private void medieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            foreach (ToolStripMenuItem x in (item.OwnerItem as ToolStripMenuItem).DropDownItems)
                x.Checked = false;
            foreach (ToolStripMenuItem x in spațiereToolStripMenuItem.DropDownItems)
                x.Checked = false;
            item.Checked = true;
            medieToolStripMenuItem1.Checked = true;
            this.ItemSize = new Size(50, 50);
            this.Font = FntSize(20);
            this.Padding = new Padding(5);
            ReOrder("size");
        }

        private void mareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            foreach (ToolStripMenuItem x in (item.OwnerItem as ToolStripMenuItem).DropDownItems)
                x.Checked = false;
            foreach (ToolStripMenuItem x in spațiereToolStripMenuItem.DropDownItems)
                x.Checked = false;
            item.Checked = true;
            this.ItemSize = new Size(100, 100);
            this.Font = FntSize(60);
            this.Padding = new Padding(7);
            ReOrder("size");
        }

        private void extraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            foreach (ToolStripMenuItem x in (item.OwnerItem as ToolStripMenuItem).DropDownItems)
                x.Checked = false;
            foreach (ToolStripMenuItem x in spațiereToolStripMenuItem.DropDownItems)
                x.Checked = false;
            item.Checked = true;
            this.ItemSize = new Size(250, 250);
            this.Font = FntSize(175);
            this.Padding = new Padding(20);
            ReOrder("size");
        }

        private void micăToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            foreach (ToolStripMenuItem x in (item.OwnerItem as ToolStripMenuItem).DropDownItems)
                x.Checked = false;
            item.Checked = true;
            this.Padding = new Padding(3);
        }

        private void medieToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            foreach (ToolStripMenuItem x in (item.OwnerItem as ToolStripMenuItem).DropDownItems)
                x.Checked = false;
            item.Checked = true;
            this.Padding = new Padding(5);
        }

        private void mareToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            foreach (ToolStripMenuItem x in (item.OwnerItem as ToolStripMenuItem).DropDownItems)
                x.Checked = false;
            item.Checked = true;
            this.Padding = new Padding(10);
        }

        private void schiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem x in spațiereToolStripMenuItem.DropDownItems)
                x.Checked = false;
            foreach (ToolStripMenuItem x in schimbaMarimeaToolStripMenuItem.DropDownItems)
                x.Checked = false;
            fontDialog.Font = this.Font;
            if (Font.SizeInPoints == 11) micaToolStripMenuItem.Checked = true;
            if (Font.SizeInPoints == 20) medieToolStripMenuItem.Checked = true;
            if (Font.SizeInPoints == 60) mareToolStripMenuItem.Checked = true;
            if (Font.SizeInPoints == 175) exrtaToolStripMenuItem.Checked = true;
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                this.Font = fontDialog.Font;
                foreach (Button b in buttons)
                    b.Font = this.Font;
            }
        }

        private void culoareDeFundalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialogfundal.Color = this.BackColor;
            if (colorDialogfundal.ShowDialog() == DialogResult.OK)
                this.BackColor = colorDialogfundal.Color;
        }

        private void culoareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { colorDialogfore.Color = this[0].ForeColor; } catch (Exception ex) { }
            if (colorDialogfore.ShowDialog() == DialogResult.OK)
                this.ForeColor = colorDialogfore.Color;
        }

        private void culoareButoaneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { colorDialogback.Color = this[0].BackColor; } catch (Exception ex) { }
            if (colorDialogback.ShowDialog() == DialogResult.OK)
                this.ElemColor = colorDialogback.Color;
        }

        private void editeazăElementeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VectorDialog dialog = new VectorDialog();
            dialog.Vector = v;
            if (dialog.ShowDialog() == DialogResult.OK)
                Items = dialog.Vector;
        }

        private void VectorControl_PaddingChanged(object sender, EventArgs e)
        {
            this.Height = ItemSize.Height + Padding.Top + Padding.Top;
            ReOrder();
            this.Width = this[Items.Count - 1].Left + this[Items.Count - 1].Width + Padding.Left;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MarimeDialog dialog = new MarimeDialog();
            dialog.ItemSize = ItemSize; dialog.Spacing = Padding; dialog.Font2 = Font;
            dialog.Fundal = BackColor; dialog.TextColor = this[0].ForeColor; dialog.ElemColor = this[0].BackColor;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.Font = dialog.Font2;
                this.ItemSize = dialog.ItemSize;
                this.Padding = dialog.Spacing;
                this.BackColor = dialog.Fundal;
                this.ForeColor = dialog.TextColor;
                this.ElemColor = dialog.ElemColor;
                ReOrder("size");
                //this.AnimationSpeed = this.ItemSize.Height/5;
            }
        }

        private void VectorControl_FontChanged(object sender, EventArgs e)
        {
            foreach (Button b in buttons)
                b.Font = this.Font;
        }

        private void copiazăToolStripMenuItem_Click(object sender, EventArgs e) => vcopy = new List<int>(Items);

        private void salveazăToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vcopy != null)
            {
                Items = vcopy;
                vcopy = null;
            }
        }

        private void încarcăDinFișierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMain form = this.ParentForm as FormMain;
            if (form.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] lines = File.ReadAllLines(form.openFileDialog1.FileName);
                bool first = form.UseNrElem;
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
                Items = rez;
            }
        }

        private void salveazăÎnFișierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMain form = this.ParentForm as FormMain;
            if (form.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string space = form.Separator;
                StreamWriter sr = new StreamWriter(form.saveFileDialog1.FileName);
                if (form.UseNrElem)
                    sr.WriteLine(Items.Count);
                foreach (int i in Items)
                    sr.Write(i + space);
                sr.Close();
            }
        }
        private void VectorControl_ForeColorChanged(object sender, EventArgs e)
        {
            foreach (Button b in buttons)
            {
                b.ForeColor = ForeColor;
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            AnimationQueue.Clear(); 
            this.Height = ItemSize.Height + Padding.Top + Padding.Top;
            ReMakeButtons();
            this.Width = this[Items.Count - 1].Left + this[Items.Count - 1].Width + Padding.Left;
            ColorDefault();
        }
        private void cntMSgeneral_Opening(object sender, CancelEventArgs e)
        {
            // icons
            toolStripMenuItem4.Image = CreateIcon(SelectionColor);
            culoareDeFundalToolStripMenuItem.Image = CreateIcon(this.BackColor);
            culoareToolStripMenuItem.Image = CreateIcon(this.ForeColor);
            culoareButoaneToolStripMenuItem.Image = CreateIcon(ElemColor);
            // 
            if (vcopy == null) pasebtn.Enabled = false;
            else pasebtn.Enabled = true;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (colorDialogselect.ShowDialog() == DialogResult.OK)
            {
                FormMain form = this.ParentForm as FormMain;
                if (Name == "v") form.colorBox.BackColor = colorDialogselect.Color;
                SelectionColor = colorDialogselect.Color;
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            foreach (ToolStripMenuItem x in (item.OwnerItem as ToolStripMenuItem).DropDownItems)
                x.Checked = false;
            item.Checked = true;
            int val = int.Parse(item.Tag.ToString());
            Pause = val;
        }

        private void scrieÎnConsolăToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMain form = this.ParentForm as FormMain;
            string space = form.Separator;
            if (form.UseNrElem)
            {
                Log.AppendText(v.Count.ToString());
                Log.AppendText(Environment.NewLine);
            }
            foreach (int i in Items)
                Log.AppendText(i + space);
            Log.AppendText(Environment.NewLine);
        }
        #endregion
    }
}
