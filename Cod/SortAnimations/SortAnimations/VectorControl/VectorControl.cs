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
    public partial class VectorControl : UserControl
    {
        public VectorControl()
        {
            InitializeComponent();
        }

        private void VectorControl_Load(object sender, EventArgs e)
        {
            this.Width = (v.Count +1) * this.Padding.Left + v.Count * ItemSize.Width;
            this.Height = 2*this.Padding.Top + ItemSize.Height;
            int x = this.Padding.Left, y = this.Padding.Top;

            // butoanele
            for(int i=0;i< v.Count; ++i)
            {
                Button b = new Button();
                b.Click += CLICK;
                b.Margin = new Padding(0);
                b.Font = this.Font;
                b.TextAlign = ContentAlignment.MiddleCenter;
                b.Text = v[i].ToString();
                b.FlatStyle = FlatStyle.Flat;
                b.Tag = i.ToString();
                b.Location = new Point(x, y);
                b.Size = getsize(b);
                buttons.Add(b);
                x += b.Width + Padding.Left;
            }
            this.Width = x;
        }

        private void ReMakeButtons(string modified="all")
        {
            Button prototype = new Button();
            if(modified == "all")
            {
                try
                {
                    prototype.Size = this[0].Size;
                    prototype.BackColor = this[0].BackColor;
                    prototype.ForeColor = this[0].ForeColor;
                    prototype.Margin = this[0].Margin;
                }catch(Exception ex){prototype.Size = ItemSize;}
                Dictionary<Color, int> F = new Dictionary<Color, int>();
                Dictionary<Color, int> B = new Dictionary<Color, int>();
                int fmax = 0, bmax = 0;
                foreach (Button btn in buttons)
                {
                    int b = 0, f = 0;
                    try { b = B[btn.BackColor]++; } catch (Exception ex) { B.Add(btn.BackColor, 1); }
                    try { f = F[btn.ForeColor]++; } catch (Exception ex) { F.Add(btn.ForeColor, 1); }
                    if (b > bmax) { bmax = b; prototype.BackColor = btn.BackColor; }
                    if (f > fmax) { fmax = f; prototype.ForeColor = btn.ForeColor; }
                }
            }
            else if(modified == "size")
            {
                prototype.Size = ItemSize;
                prototype.BackColor = ElemColor;
                prototype.ForeColor = this.ForeColor;
            }
            List<Color> clr = new List<Color>();
            if(buttons.Count == v.Count)
            foreach (Button b in buttons)
                clr.Add( b.BackColor);
            this.Controls.Clear();
            this.buttons.Clear();
            this.Width = (v.Count + 1) * this.Padding.Left + v.Count * ItemSize.Width;
            this.Height = 2 * this.Padding.Top + ItemSize.Height;
            int x = this.Padding.Left, y = this.Padding.Top;

            // butoanele
            for (int i = 0; i < v.Count; ++i)
            {
                Button b = new Button();
                b.Click += CLICK;
                b.Size = new Size(prototype.Size.Height, prototype.Size.Height);
                b.Margin = prototype.Margin;
                b.ForeColor = prototype.ForeColor;
                try { b.BackColor = clr[i]; } catch (Exception ex) { b.BackColor = prototype.BackColor; }
                b.Font = this.Font;
                b.TextAlign = ContentAlignment.MiddleCenter;
                b.Text = v[i].ToString();
                b.FlatStyle = FlatStyle.Flat;
                b.Tag = i.ToString();
                b.Location = new Point(x, y);
                b.Size = getsize(b);
                this.Controls.Add(b);
                buttons.Add(b);
                x += b.Width + Padding.Left;
            }
            this.Width = x;
        }
        private void ReOrder(string modified = "")
        {
            int x = Padding.Left, y = Padding.Top;
            if (modified == "size") this.Height = 2 * y + ItemSize.Height;
            for(int i=0;i<v.Count;++i)
            {
                this[i].Location = new Point(x, y);
                if(modified == "size") this[i].Size = getsize(this[i]);
                x += this[i].Width + Padding.Left;
            }
            if (modified == "size") { this.Width = x; }
            buttons.RemoveRange(v.Count, buttons.Count - v.Count);
        }

        private void ExecuteString(string S, bool Instant = true)
        {
            if (Instant)
            {
                string[] s = S.Split();
                if (s[0] == "swap")
                {
                    int i = int.Parse(s[1]), j = int.Parse(s[2]);
                    int auxi = v[i]; v[i] = v[j]; v[j] = auxi;
                    Button aux = this[i]; this[i] = this[j]; this[j] = aux;
                }
                else if (s[0] == "erase")
                {
                    int i = int.Parse(s[1]);
                    v.RemoveAt(i);
                    buttons.RemoveAt(i);
                    ReMakeButtons();
                }
                else if (s[0] == "insert")
                {
                    int i = int.Parse(s[1]), val = int.Parse(s[2]);
                    v.Insert(i, val);
                    ReMakeButtons();
                }
            } else throw new NotImplementedException();
        }
        private void ReverseExecuteString(string S, bool Instant = true)
        {
            string[] s = S.Split();
            if (s[0] == "swap") ExecuteString(string.Format("swap {1} {0}", s[1], s[2]));
            else if (s[0] == "erase") ExecuteString(string.Format("insert {0} {1}", s[1], v[int.Parse(s[1])]));
            else if(s[0] == "insert") AnimationQueue.Enqueue(delegate {ExecuteString(string.Format("erase {0}", s[1])); });
        }

        public void Stop() 
        { 
            AnimationQueue.Clear();
            if (Animating) ReverseExecuteString(currentTimer.Tag.ToString());
        }

        public Font FntSize(int size) => new Font(this.Font.FontFamily, size, this.Font.Style, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
    }
}
