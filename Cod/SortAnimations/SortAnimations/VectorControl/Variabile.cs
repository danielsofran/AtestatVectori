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
        #region Variabile globale
        // date 
        List<int> v = new List<int>();
        static List<int> vcopy = null;

        public List<int> Items
        {
            get => v;
            set
            {
                v = value;
                ReMakeButtons();
                nrswaps = 0;
            }
        }

        public List<Button> buttons = new List<Button>();
        Dictionary<Button, Color> oldColors = new Dictionary<Button, Color>();

        public Button this[int i] { get => buttons[i]; set => buttons[i] = value; }

        // design
        Size _itemsize = new Size(40, 40);
        public Size ItemSize
        {
            get => _itemsize;
            set
            {
                _itemsize = value;
                ReOrder("size");
                nrswaps = 0;
            }
        }

        // animatii
        public int AnimationSpeed { get; set; } = 5;
        public int Pause { get; set; } = 100; // milisec

        public bool Animating = false;
        private Queue<Action> AnimationQueue = new Queue<Action>();
        Timer currentTimer;

        public bool AutoAnimationMode { get; set; } = true;
        public Button NextButton { set { value.Click += new EventHandler(delegate (object sender, EventArgs args) { if (!AutoAnimationMode && !Animating) ProceedNextInQueue(); }); } }
        public CheckBox CheckButton { set { value.CheckedChanged += new EventHandler(delegate (object sender, EventArgs args) { AutoAnimationMode = value.Checked; if (!Animating) ProceedNextInQueue(); }); } }

        public TextBox Log { get; set; } = new TextBox();
        public int nrswaps = 0;

        bool E = false, C = false;
        public bool IsEditOnly
        {
            get => E;
            set
            {
                if (C == true) return;
                E = value;
                if (E == true) C = false;
                string[] notedit = "1 4".Split();
                foreach (ToolStripItem c in cntMSgeneral.Items)
                {
                    if (c.Tag == null) continue;
                    ToolStripMenuItem item;
                    if (c is ToolStripMenuItem) item = (c as ToolStripMenuItem);
                    else continue;
                    if (notedit.Contains(item.Tag.ToString()))
                        item.Enabled = !value;
                    else item.Enabled = true;
                }
            }
        }

        public bool IsColorOnly
        {
            get => C;
            set
            {
                C = value;
                if (C == true) E = false;
                string[] notedit = "0 1 4 5 8 9 17 18".Split();
                foreach (ToolStripItem c in cntMSgeneral.Items)
                {
                    if (c.Tag == null) continue;
                    ToolStripMenuItem item;
                    if (c is ToolStripMenuItem) item = (c as ToolStripMenuItem);
                    else continue;
                    if (notedit.Contains(item.Tag.ToString()))
                        item.Enabled = !value;
                    else item.Enabled = true;
                }
            }
        }

        // culori
        Color b_color = SystemColors.Control;
        public Color ElemColor
        {
            get => b_color;
            set
            {
                foreach (Button b in buttons)
                    if (b.BackColor == b_color)
                        b.BackColor = value;
                b_color = value;
            }
        }
        public Color SelectionColor { get; set; } = OwnColors.Select;
        Bitmap CreateIcon(Color c)
        {
            Bitmap bitmap = new Bitmap(100, 100);
            for (var x = 0; x < bitmap.Width; x++)
            {
                for (var y = 0; y < bitmap.Height; y++)
                {
                    bitmap.SetPixel(x, y, c);
                }
            }
            return bitmap;
        }
        Size getsize(Button b)
        {
            b.Text = b.Text.Trim();
            int r = ItemSize.Width;
            r += (int)(b.Font.SizeInPoints * (b.Text.Length - 1));
            if (b.Text[0] == '-') r -= (int)(b.Font.SizeInPoints / 3);
            return new Size(r, ItemSize.Height);
        }

        #endregion
    }
}
