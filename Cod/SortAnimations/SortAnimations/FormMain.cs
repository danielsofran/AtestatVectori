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
    public partial class FormMain : Form
    {
        Debuger debuger = new Debuger();
        public FormMain()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.icon;
            debuger.Expresie = "this.Size";
            debuger.Valoare = this.Size.ToString();
            //debuger.Show();
            //this.MinimumSize = this.Size;
            numericUpDown4.Maximum = v.Items.Count;
            comboBox1.SelectedIndex = 0;
            CopyControl[this.Name] = this.Bounds;
            CopyControls(this.Controls as ControlCollection);
            colorBox.BackColor = OwnColors.Select;
            colorBox1.BackColor = OwnColors.Select;
            colorBox2.BackColor = OwnColors.Insert;
            colorBox3.BackColor = OwnColors.Erase;
            colorBox4.BackColor = OwnColors.Marked;
            tabControl1.SelectedIndex = 0;//7
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            debuger.Location = new Point(this.Left, 0);
            this.Location = new Point(this.Location.X, debuger.Height + 10);
            tabControl1_SelectedIndexChanged(sender, e);
            //SaveControls(@"C:\Users\SOFRAN ROMULUS\Desktop\c1.txt");
            vs2.NextButton = buttonn1;
            vs2.CheckButton = checkBox1;
            vi2.NextButton = button19;
            vi2.CheckButton = checkBox2;
            vsi1.NextButton = button20;
            vsi1.CheckButton = checkBox3;
            vsd.NextButton = button21;
            vsd.CheckButton = checkBox4;
            vsb.NextButton = button22;
            vsb.CheckButton = checkBox5;
            v.NextButton = button23;
            v.CheckButton = checkBox6;
            vi.NextButton = button46;
            vi.CheckButton = checkBox9;
            //this.webBrowser1.Url = new Uri(Application.StartupPath + "\\Help.html");
            webBrowser1.DocumentText = Properties.Resources.Help1;
        }

        #region multi reference enevts/fct
        private void textBox11_KeyPress(object sender, KeyPressEventArgs e){ e.Handled = !char.IsControl(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '\n';}
        
        Font FntSize(Font font, float size) => new Font(font.FontFamily, size, font.Style, font.Unit, font.GdiCharSet, font.GdiVerticalFont);
        void Center(params Control[] controls) { foreach (Control control in controls) SortAnimations.Dialogs.MarimeDialog.CenterLeft(control); }
        private void vs1_SizeChanged(object sender, EventArgs e)
        {
            VectorControl vector = sender as VectorControl;
            if (vector.Animating) return;
            if (vector.Name.Contains("s"))
            {
                vsi.Padding = vs1.Padding;
                if (vector.Items.Count == vsi.Items.Count) return;
                List<int> l = new List<int>();
                for (int i = 0; i < vector.Items.Count; ++i) l.Add(i);
                vsi.Items = l;
                
            }
            else
            {
                vii.Padding = vi1.Padding;
                if (vector.Items.Count == vii.Items.Count) return;
                List<int> l = new List<int>();
                for (int i = 0; i < vector.Items.Count; ++i) l.Add(i);
                vii.Items = l;
                
            }
        }
        private void LoadFile(RichTextBox r, string s)
        {
            r.SelectAll();
            switch (s)
            {
                case "stergere.rtf": r.SelectedRtf = Properties.Resources.stergere; break;
                case "stergere2.rtf": r.SelectedRtf = Properties.Resources.stergere2; break;
                case "stergere2b.rtf": r.SelectedRtf = Properties.Resources.stergere2b; break;
                case "stergere2bM.rtf": r.SelectedRtf = Properties.Resources.stergere2bM; break;
                case "stergere2M.rtf": r.SelectedRtf = Properties.Resources.stergere2M; break;
                case "stergereM.rtf": r.SelectedRtf = Properties.Resources.stergereM; break;
                case "inserare.rtf": r.SelectedRtf = Properties.Resources.inserare; break;
                case "inserare2.rtf": r.SelectedRtf = Properties.Resources.inserare2; break;
                case "inserare2b.rtf": r.SelectedRtf = Properties.Resources.inserare2b; break;
                case "inserare2bM.rtf": r.SelectedRtf = Properties.Resources.inserare2bM; break;
                case "inserare2M.rtf": r.SelectedRtf = Properties.Resources.inserare2M; break;
                case "inserareM.rtf": r.SelectedRtf = Properties.Resources.inserareM; break;
                case "inserts.rtf": r.SelectedRtf = Properties.Resources.inserts; break;
                case "inserts1.rtf": r.SelectedRtf = Properties.Resources.inserts1; break;
                case "inserts2.rtf": r.SelectedRtf = Properties.Resources.inserts2; break;
                case "inserts3.rtf": r.SelectedRtf = Properties.Resources.inserts3; break;
                case "sortb.rtf": r.SelectedRtf = Properties.Resources.sortb; break;
                case "sortb1.rtf": r.SelectedRtf = Properties.Resources.sortb1; break;
                case "sortb2.rtf": r.SelectedRtf = Properties.Resources.sortb2; break;
                case "sortb3.rtf": r.SelectedRtf = Properties.Resources.sortb3; break;
                case "sortd.rtf": r.SelectedRtf = Properties.Resources.sortd; break;
                case "sortd1.rtf": r.SelectedRtf = Properties.Resources.sortd1; break;
                case "sortd2.rtf": r.SelectedRtf = Properties.Resources.sortd2; break;
                case "sorti1.rtf": r.SelectedRtf = Properties.Resources.sorti1; break;
                case "sorti2.rtf": r.SelectedRtf = Properties.Resources.sorti2; break;
                case "sorti22.rtf": r.SelectedRtf = Properties.Resources.sorti22; break;
                case "sorti3.rtf": r.SelectedRtf = Properties.Resources.sorti3; break;
                default: break;
            }
        }
        #endregion
    }
}