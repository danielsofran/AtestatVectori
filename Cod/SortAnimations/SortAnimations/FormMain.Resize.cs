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
        // variabile
        Dictionary<string, Rectangle> CopyControl = new Dictionary<string, Rectangle>();
        void CopyControls(Control.ControlCollection source)
        {
            foreach (Control control in source)
            {
                CopyControl[control.Name] = control.Bounds;
                //control.
                //if (control is RichTextBox t) t.BorderStyle = BorderStyle.None;
                if (control is Button b)
                {
                    if (b.Text == "Rulează") toolTip1.SetToolTip(b, "Rulează codul");
                    else if (b.Text.StartsWith("Reset")) toolTip1.SetToolTip(b, "Resetează valorile vectorului, oprește animația și șterge consola");
                    else if (b.Text == "Next Step") toolTip1.SetToolTip(b, "Trece la pasul următor dacă căsuța de Auto Play nu este bifată.\nNu are efect în caz contrar sau dacă o animație este în curs de desfășurare.");
                }
                else if (control is CheckBox c && c.Text == "Auto Play") toolTip1.SetToolTip(c, "Schimbă modul de executare a animației.\nPoate fi schimbat în timpul animației.\nÎn cazul în care este dezactivat, animația continuă\ndoar la apăsarea butonului Next Step.");
                else if (control is RichTextBox r) { r.ScrollBars = RichTextBoxScrollBars.Both; r.WordWrap = false; }
                if (control.HasChildren)
                    CopyControls(control.Controls);
            }
        }
        void RestoreControls(params Control[] controls)
        {
            foreach (Control control in controls)
            {
                if (control is Label)
                    control.Location = CopyControl[control.Name].Location;
                else
                    control.Bounds = CopyControl[control.Name];
            }
        }

        // tab uri
        void tab0()
        {
            //tabPage7.Font = FntSize(this.Font, 16);
            // minim size
            if (button43.Left + 15 < label40.Right) button43.Visible = false;
            else button43.Visible = true;
            // panels
            splitContainerE.Panel1MinSize = CopyControl[tabControl2.Name].Width + 2 * tabControl2.Margin.Left;
            splitContainerH.Panel2MinSize = CopyControl[button38.Name].Height + 6;
            textBox11.Height = splitContainerH.Panel2.Height - splitContainerH.SplitterWidth - button43.Top - button43.Height - 5;
            tabControl2.Height = splitContainerH.Panel1.Height - splitContainerH.SplitterWidth - 3;
            splitContainerE.Height = tabControl2.Height + 5;
            // v size changed
            panel1.Height = Math.Min(v.Height + 2 * panel1.AutoScrollMargin.Height + 20, splitContainerE.Height - 3);
            panel1.Width = Math.Min(v.Width + 10, splitContainerE.Panel2.Width);
            splitContainerE.Panel2MinSize = panel1.Width;
            v.AtTimeAction(delegate { numericUpDown4.Maximum = v.Items.Count; });
            if (splitContainerE.SplitterDistance > splitContainerE.Width - splitContainerE.Panel2MinSize)
                splitContainerE.SplitterDistance = splitContainerE.Width - splitContainerE.Panel2MinSize;
            // center
            Dialogs.MarimeDialog.Center(panel1);
            Dialogs.MarimeDialog.CenterLeft(tabControl2);
        }
        void tab1()
        {
            // insert/remove simplu
            int height_spacing = 20;
            if (this.Width <= 1199)
            {
                splitContainer1.Font = FntSize(this.Font, 16);
                button2.Font = FntSize(button2.Font, 12); button1.Image = null;
                button3.Font = FntSize(button3.Font, 12); button4.Image = null;
                button1.Font = button4.Font = FntSize(this.Font, 14);
                height_spacing = 10;
                vs1.ItemSize = vsi.ItemSize = vi1.ItemSize = vii.ItemSize = new Size(30, 30);
                vs1.Font = vsi.Font = vs1.FntSize(13);
                vi1.Font = vii.Font = vi1.FntSize(13);
                vs1.Left = label2.Right + height_spacing / 2;
                vsi.Left = label3.Right + height_spacing / 2;
                vi1.Left = label11.Right + height_spacing / 2;
                vii.Left = label10.Right + height_spacing / 2;
            }
            else
            {
                splitContainer1.Font = FntSize(this.Font, 20.25f);
                button2.Font = FntSize(button2.Font, 16);
                button3.Font = FntSize(button3.Font, 16);
                button1.Font = button4.Font = FntSize(this.Font, 16);
                vs1.ItemSize = vsi.ItemSize = vi1.ItemSize = vii.ItemSize = new Size(40, 40);
                vs1.Font = vsi.Font = vs1.FntSize(16); button1.Image = Properties.Resources.minus;
                vi1.Font = vii.Font = vi1.FntSize(16); button4.Image = Properties.Resources.add;
                vs1.Left = label2.Right + height_spacing / 2;
                vsi.Left = label3.Right + height_spacing / 2;
                vi1.Left = label11.Right + height_spacing / 2;
                vii.Left = label10.Right + height_spacing / 2;
            }
            numericUpDown1.Left = label5.Right + height_spacing ;
            button1.Left = numericUpDown1.Right + height_spacing ;
            numericUpDown2.Left = label8.Right + height_spacing / 4;
            label13.Left = numericUpDown2.Right + height_spacing / 4;
            numericUpDown3.Left = label13.Right + height_spacing / 4;
            button4.Left = numericUpDown3.Right + height_spacing / 4;
            splitContainer11.Height = splitContainer12.Height = splitContainer1.Panel1.Height - (numericUpDown1.Bottom + height_spacing/2);
            if (splitContainer11.Top + 2*textBox1.Bottom+ height_spacing/2 < splitContainer1.Panel1.Height)
            {
                //stanga
                splitContainer11.Orientation = Orientation.Horizontal;
                splitContainer11.FixedPanel = FixedPanel.None;
                splitContainer11.SplitterDistance = splitContainer11.Height/2;
                textBox1.Width = splitContainer11.Panel1.Width - 2*height_spacing;
                richTextBox1.Width = splitContainer11.Panel2.Width - 2*height_spacing;
                Center(textBox1, richTextBox1);
                label4.Left = textBox1.Left;
                label6.Left = richTextBox1.Left;
                LoadFile(richTextBox1, "stergereM.rtf");
                // dreapta
                splitContainer12.Orientation = Orientation.Horizontal;
                splitContainer12.FixedPanel = FixedPanel.None;
                splitContainer12.SplitterDistance = splitContainer12.Height / 2;
                textBox2.Width = splitContainer12.Panel1.Width - 2 * height_spacing;
                richTextBox2.Width = splitContainer12.Panel2.Width - 2 * height_spacing;
                Center(textBox2, richTextBox2);
                label9.Left = textBox2.Left;
                label7.Left = richTextBox2.Left;
                LoadFile(richTextBox2, "inserareM.rtf");
            }
            else
            {
                splitContainer11.SplitterDistance = splitContainer11.Width - splitContainer11.SplitterWidth -( height_spacing + CopyControl[richTextBox1.Name].Width);
                splitContainer12.SplitterDistance = splitContainer12.Width - splitContainer12.SplitterWidth - (height_spacing + CopyControl[richTextBox2.Name].Width);
                //stanga
                splitContainer11.Orientation = Orientation.Vertical;
                splitContainer11.FixedPanel = FixedPanel.Panel2;
                textBox1.Width = splitContainer11.Panel1.Width -  height_spacing;
                richTextBox1.Width = splitContainer11.Panel2.Width - height_spacing;
                Center(textBox1, richTextBox1);
                label4.Left = textBox1.Left;
                label6.Left = richTextBox1.Left;
                LoadFile(richTextBox1, "stergere.rtf");
                // dreapta
                splitContainer12.Orientation = Orientation.Vertical;
                splitContainer12.FixedPanel = FixedPanel.Panel2;
                textBox2.Width = splitContainer12.Panel1.Width - height_spacing;
                richTextBox2.Width = splitContainer12.Panel2.Width - height_spacing;
                Center(textBox2, richTextBox2);
                label9.Left = textBox2.Left;
                label7.Left = richTextBox2.Left;
                LoadFile(richTextBox2, "inserare.rtf");
            }
        }
        void tab2()
        {
            // stergere multipla
            flowLayoutPanel2.Width = buttonn1.Left - flowLayoutPanel2.Left - 22;
            splitContainer2.Height = tabPage2.Height - flowLayoutPanel2.Top - flowLayoutPanel2.Height - 10;
            if (splitContainer2.Height < 500)
            {
                RestoreControls(button5, button6, label18, richTextBox3, textBox3, label19, button8, button7, label20, richTextBox4, textBox4);
                label18.Text = label20.Text = "Consolă " + Environment.NewLine + "operații";
                LoadFile(richTextBox4, "stergere2b.rtf");
                LoadFile(richTextBox3, "stergere2.rtf");
                textBox3.Font = textBox4.Font = FntSize(textBox3.Font, 15.75f);
            }
            else if (splitContainer2.Height >= 500)
            {
                //st
                button6.Top = button5.Top = label17.Top;
                button5.Left = label17.Left + label17.Width + 20;
                button6.Left = button5.Left + button5.Width + 20;
                richTextBox3.Top = button5.Top + button5.Height + 20;
                richTextBox3.Left = label17.Left;
                richTextBox3.Height = 230;
                label18.Text = label20.Text = "Consolă operații";
                label18.Top = richTextBox3.Top + richTextBox3.Height + 20;
                textBox3.Top = label18.Top + label18.Height + 10;
                textBox3.Left = label18.Left;
                textBox3.Font = FntSize(textBox3.Font, 20.25f);
                LoadFile(richTextBox3, "stergere2M.rtf");
                //dr
                button7.Top = button8.Top = label19.Top;
                button8.Left = label19.Left + label19.Width + 20;
                button7.Left = button8.Left + button8.Width + 20;
                richTextBox4.Top = button8.Top + button8.Height + 20;
                richTextBox4.Left = label19.Left;
                richTextBox4.Height = 230;
                label20.Top = richTextBox4.Top + richTextBox4.Height + 20;
                textBox4.Top = label20.Top + label20.Height + 10;
                textBox4.Left = label20.Left;
                LoadFile(richTextBox4, "stergere2bM.rtf");
                textBox4.Font = FntSize(textBox4.Font, 20.25f);
            }
            int margin_right = (this.Width < CopyControl[this.Name].Width) ? 10 : 54;
            //checkBox1.Left = tabPage2.Width - checkBox1.Width - margin_right;
            //buttonn1.Left = checkBox1.Left - 15 - buttonn1.Width;
            richTextBox3.Width = splitContainer2.Panel1.Width - richTextBox3.Left - margin_right;
            richTextBox4.Width = splitContainer2.Panel2.Width - richTextBox4.Left - margin_right;
            if (splitContainer2.Height > 322) textBox3.Height = splitContainer2.Height - textBox3.Top - 20;
            if (splitContainer2.Height > 322) textBox4.Height = splitContainer2.Height - textBox4.Top - 20;
            textBox3.Width = splitContainer2.Panel1.Width - textBox3.Left - margin_right;
            textBox4.Width = splitContainer2.Panel2.Width - textBox4.Left - margin_right;
            Dialogs.MarimeDialog.CenterTop(vs2);
        }
        void tab3()
        {
            // inserare multipla
            textBox15.Width = tabPage3.Width - textBox15.Left - 50;
            flowLayoutPanel3.Width = button19.Left - flowLayoutPanel3.Left - 22;
            splitContainer3.Height = tabPage3.Height - flowLayoutPanel3.Top - flowLayoutPanel3.Height - 10;
            if (splitContainer3.Height < 500)
            {
                RestoreControls(button12, button11, label24, richTextBox6, textBox6, label21, button10, button9, label22, richTextBox5, textBox5);
                textBox5.Font = textBox6.Font = FntSize(textBox5.Font, 16.75f);
                label22.Text = label24.Text = "Consolă " + Environment.NewLine + "operații";
                LoadFile(richTextBox6, "inserare2.rtf");
                LoadFile(richTextBox5, "inserare2b.rtf");
            }
            else if (splitContainer3.Height >= 500)
            {
                //st
                button11.Top = button12.Top = label23.Top;
                button12.Left = label23.Left + label23.Width + 20;
                button11.Left = button12.Left + button12.Width + 20;
                richTextBox6.Top = button12.Top + button12.Height + 20;
                richTextBox6.Left = label23.Left;
                richTextBox6.Height = 260;
                label22.Text = label24.Text = "Consolă operații";
                label24.Top = richTextBox6.Top + richTextBox6.Height + 20;
                textBox6.Top = label24.Top + label24.Height + 10;
                textBox6.Left = label24.Left;
                LoadFile(richTextBox6, "inserare2M.rtf");
                textBox6.Font = FntSize(textBox4.Font, 20.25f);
                //dr
                button9.Top = button10.Top = label21.Top;
                button10.Left = label21.Left + label21.Width + 20;
                button9.Left = button10.Left + button10.Width + 20;
                richTextBox5.Top = button10.Top + button10.Height + 20;
                richTextBox5.Left = label21.Left;
                richTextBox5.Height = 260;
                label22.Top = richTextBox5.Top + richTextBox5.Height + 20;
                textBox5.Top = label22.Top + label22.Height + 10;
                textBox5.Left = label22.Left;
                LoadFile(richTextBox5, "inserare2bM.rtf");
                textBox5.Font = FntSize(textBox4.Font, 20.25f);
            }
            int margin = (this.Width > 1200) ? 54 : 10;
            richTextBox6.Width = splitContainer3.Panel1.Width - richTextBox6.Left - margin;
            richTextBox5.Width = splitContainer3.Panel2.Width - richTextBox5.Left - margin;
            if (splitContainer3.Height > 322) textBox6.Height = splitContainer3.Height - textBox6.Top - 20;
            if (splitContainer3.Height > 322) textBox5.Height = splitContainer3.Height - textBox5.Top - 20;
            textBox6.Width = splitContainer3.Panel1.Width - textBox6.Left - margin;
            textBox5.Width = splitContainer3.Panel2.Width - textBox5.Left - margin;
        }
        void tab4()
        {
            // sort indirecta
            splitContainer4.Height = tabPage4.Height - flowLayoutPanel4.Top - flowLayoutPanel4.Height - 10;
            flowLayoutPanel4.Width = tabPage4.Width - flowLayoutPanel4.Left - 30;
            textBox16.Width = flowLayoutPanel4.Width;
            richTextBox7.Height = Math.Max(splitContainer4.Height - 39, button20.Bottom);
            richTextBox7.Width = splitContainer4.Panel1.Width - richTextBox7.Left - 10;
            textBox7.Height = textBox8.Height = splitContainer4.Height - 39 - textBox7.Top;
            if (richTextBox7.Width >= 600) LoadFile(richTextBox7, "sorti3.rtf");
            else if (richTextBox7.Width >= 500) LoadFile(richTextBox7, "sorti22.rtf");
            else if (richTextBox7.Width >= 400) LoadFile(richTextBox7, "sorti2.rtf");
            else LoadFile(richTextBox7, "sorti1.rtf");
            label32.Text = "Consolă intershimbări";
            if (label32.Width > splitContainer5.Panel1.Width) label32.Text = "Interschimbări";
            label33.Text = "Consolă comparări";
            if (label33.Width > splitContainer5.Panel2.Width) label33.Text = "Comparări";
            if (textBox7.Width + 10 > splitContainer5.Panel1.Width) textBox7.Width = textBox8.Width = splitContainer5.Panel1.Width - 10;
            Center(label32, label33, textBox7, textBox8);
        }
        void tab5()
        {
            splitContainer6.Height = tabPage5.Height - flowLayoutPanel5.Top - flowLayoutPanel5.Height - 10;
            flowLayoutPanel5.Width = tabPage5.Width - flowLayoutPanel5.Left - 50;
            textBox17.Width = flowLayoutPanel5.Width;
            richTextBox8.Height = Math.Max(splitContainer6.Height - 39, button21.Bottom);
            richTextBox8.Width = splitContainer6.Panel1.Width - richTextBox8.Left - 10;
            textBox10.Height = textBox9.Height = splitContainer6.Height - 39 - textBox10.Top;
            if (richTextBox8.Width >= 500) LoadFile(richTextBox8, "sortd2.rtf");
            else if (richTextBox8.Width >= 370) LoadFile(richTextBox8, "sortd1.rtf");
            else LoadFile(richTextBox8, "sortd.rtf");
            label36.Text = "Consolă intershimbări";
            if (label36.Width > splitContainer7.Panel1.Width) label36.Text = "Interschimbări";
            label34.Text = "Consolă comparări";

            if (label34.Width > splitContainer7.Panel2.Width) label34.Text = "Comparări";
            if (textBox10.Width + 10 > splitContainer7.Panel1.Width) textBox10.Width = textBox9.Width = splitContainer7.Panel1.Width - 10;
            Center(label36, label34, textBox9, textBox10);
        }
        void tab6()
        {
            // sort bule
            splitContainer8.Height = tabPage6.Height - flowLayoutPanel6.Top - flowLayoutPanel6.Height - 10;
            flowLayoutPanel6.Width = tabPage6.Width - flowLayoutPanel6.Left - 30;
            textBox18.Width = flowLayoutPanel6.Width;
            Center(label42, textBox12);
            richTextBox9.Height = Math.Max(splitContainer8.Height - 39, button22.Bottom);
            richTextBox9.Width = splitContainer8.Panel1.Width - richTextBox9.Left - 60;
            textBox12.Height = splitContainer8.Height - 39 - textBox12.Top;
            if (richTextBox9.Width >= 600) LoadFile(richTextBox9, "sortb3.rtf");
            else if (richTextBox9.Width >= 500) LoadFile(richTextBox9, "sortb2.rtf");
            else if (richTextBox9.Width >= 400) LoadFile(richTextBox9, "sortb1.rtf");
            else LoadFile(richTextBox9, "sortb.rtf");
        }
        void tab7()
        {
            //insert sort
            splitContainer9.Height = tabPage13.Height - flowLayoutPanel7.Top - flowLayoutPanel7.Height - 10;
            flowLayoutPanel7.Width = tabPage13.Width - flowLayoutPanel7.Left - 30;
            textBox19.Width = flowLayoutPanel7.Width;
            int margin = (this.Width >= 1135) ? 60 : 10;
            richTextBox10.Height = Math.Max(splitContainer9.Height - 39, button46.Bottom);
            richTextBox10.Width = splitContainer9.Panel1.Width - richTextBox10.Left - margin;
            textBox13.Height = textBox14.Height = splitContainer9.Height - 39 - textBox13.Top;
            if (richTextBox10.Width >= 600) LoadFile(richTextBox10, "inserts3.rtf");
            else if (richTextBox10.Width >= 500) LoadFile(richTextBox10, "inserts2.rtf");
            else if (richTextBox10.Width >= 400) LoadFile(richTextBox10, "inserts1.rtf");
            else LoadFile(richTextBox10, "inserts.rtf");
            if (textBox13.Width + 10 > splitContainer10.Panel1.Width)
                textBox13.Width = textBox14.Width = splitContainer10.Panel1.Width - 10;
            Center(label50, label51, textBox13, textBox14);
        }
        private void v_SizeChanged(object sender, EventArgs e) 
        {
            panel1.Height = Math.Min(v.Height + 2 * panel1.AutoScrollMargin.Height + 20, splitContainerE.Height - 3);
            panel1.Width = Math.Min(v.Width + 10, splitContainerE.Panel2.Width);
            Dialogs.MarimeDialog.Center(panel1);
            v.AtTimeAction(delegate { numericUpDown4.Maximum = v.Items.Count; });
            splitContainerE.Panel2MinSize = panel1.Width;
            if (splitContainerE.SplitterDistance > splitContainerE.Width - splitContainerE.Panel2MinSize)
                splitContainerE.SplitterDistance = splitContainerE.Width - splitContainerE.Panel2MinSize;
        }
        private void splitContainerE_SplitterMoved(object sender, SplitterEventArgs e) => tab0();
        private void splitContainerH_SplitterMoved(object sender, SplitterEventArgs e) => tab0();

        // apeluri finale
private void FormMain_Resize(object sender, EventArgs e) => tabControl1_SelectedIndexChanged(sender, e);
private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
{
    if (tabControl1.SelectedIndex == 0) tab0(); // editor
    else if (tabControl1.SelectedIndex == 1) tab1(); // insert/remove simplu
    else if (tabControl1.SelectedIndex == 2) tab2(); // stergere multipla
    else if (tabControl1.SelectedIndex == 3) tab3(); // inserare multipla
    else if (tabControl1.SelectedIndex == 4) tab4(); // sort indirecta
    else if (tabControl1.SelectedIndex == 5) tab5(); // sort directa
    else if (tabControl1.SelectedIndex == 6) tab6(); // sort bule
    else if (tabControl1.SelectedIndex == 7) tab7(); // insert sort 
    //debuger.Valoare = this.Size.ToString();
}
    }
}
