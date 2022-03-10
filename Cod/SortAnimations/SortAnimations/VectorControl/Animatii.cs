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
        #region Animation basis
        private void Swap_One(int i, int j)
        {
            if (i == j) ;
            int aux;
            if (i > j) { aux = i; i = j; j = aux; }
            Color ci = this[i].BackColor, cj = this[j].BackColor;
            this[i].BackColor = OwnColors.Select;
            this[j].BackColor = OwnColors.Select;
            this[j].BringToFront();
            this[i].BringToFront();
            for (int k = 0; k < Items.Count; ++k)
                if (i != k && j != k)
                    this[k].SendToBack();
            Animating = true;

            //animatie cu timer, trb ActionQueue ca sa nu se suprapuna...
            Log.Text += "swap(" + i + ", " + j + "): " + v[i] + " " + v[j]; Log.AppendText(Environment.NewLine);
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer(); currentTimer = timer;
            timer.Tag = string.Format("swap {0} {1}", i, j);
            int xi = this[i].Location.X, xj = this[j].Location.X;
            timer.Interval = AnimationSpeed * (ItemSize.Height / 5) / (Math.Abs(j - i) == 1 ? 2 : Math.Abs(j - i));
            int pauze = Math.Max(Pause / timer.Interval, 1);
            bool InPauza = false;
            timer.Tick += delegate (object sender, EventArgs args) {
                if (InPauza)
                {
                    --pauze;
                    if (pauze <= 0)
                    {
                        timer.Stop();
                        Animating = false;
                        if (AutoAnimationMode) ProceedNextInQueue();
                    }
                }
                else
                {
                    if (this[i].Left < xj) this[i].Left += AnimationSpeed * Math.Max(this[i].Width / 50, 1);
                    if (this[j].Left > xi) this[j].Left -= AnimationSpeed * Math.Max(this[j].Width / 50, 1);
                    if (this[i].Left >= xj && this[j].Left <= xi)
                    {
                        this[i].Left = xj; this[j].Left = xi;
                        Button auxb = this[i]; this[i] = this[j]; this[j] = auxb;
                        aux = Items[i]; Items[i] = Items[j]; Items[j] = aux;
                        this[i].BackColor = cj;
                        this[j].BackColor = ci;
                        InPauza = true;
                        ReOrder();
                    }
                }
            };
            timer.Start();
        }

        private void Erase_One(int i)
        {
            if (i >= Items.Count - 1 || i < 0) return;
            int aux;
            //if (i > j) { aux = i; i = j; j = aux; }
            this[i].BackColor = OwnColors.Erase;
            this[i].SendToBack();
            //Thread.Sleep(Pause);
            Animating = true;

            //animatie cu timer, trb ActionQueue ca sa nu se suprapuna...
            Log.Text += "erase(" + i + "): " + v[i]; Log.AppendText(Environment.NewLine);
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer(); currentTimer = timer;
            timer.Tag = string.Format("erase {0}", i);
            timer.Interval = AnimationSpeed;
            int value = Math.Max(ItemSize.Width / 30, 1);// *Math.Max(ItemSize.Height/50, 1);
            // if (ItemSize.Height >= 100) value = (int)Math.Sqrt(value);
            int pauze = Math.Max(Pause / timer.Interval, 1);
            bool InPauza = false;

            timer.Tick += delegate (object sender, EventArgs args) {
                if (InPauza)
                {
                    --pauze;
                    if (pauze <= 0)
                    {
                        timer.Stop();
                        Animating = false;
                        if (AutoAnimationMode) ProceedNextInQueue();
                    }
                }
                else
                {
                    if (this[i + 1].Left > this[i].Left)
                    {
                        this.Width -= value;
                        this[i].Width -= value;
                        for (int k = i + 1; k < Items.Count; ++k)
                            this[k].Left -= value;
                    }
                    else// if (this[i+1].Left <= this[i].Left)
                    {
                        /*for (int k = i + 1; k < Items.Count; ++k)
                            this[k].Left += this[i].Left-this[i+1].Left;*/
                        // stergere din vectori!!!
                        this.Controls.Remove(this[i]);
                        v.RemoveAt(i);
                        buttons.RemoveAt(i);
                        ReOrder();
                        InPauza = true;
                    }
                }
            };
            timer.Start();
        }

        private void Erase_Last()
        {
            int i = v.Count - 1;
            this[i].BackColor = OwnColors.Erase;
            //Thread.Sleep(Pause);
            Animating = true;

            //animatie cu timer, trb ActionQueue ca sa nu se suprapuna...
            Log.Text += "erase(" + (v.Count - 1) + "): " + v.Last(); Log.AppendText(Environment.NewLine);
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer(); currentTimer = timer;
            timer.Tag = string.Format("erase {0}", v.Count - 1);
            timer.Interval = AnimationSpeed;
            int value = Math.Max(ItemSize.Width / 30, 1);
            int pauze = Math.Max(Pause / timer.Interval, 1);
            bool InPauza = false;
            int x = this[i].Left;
            timer.Tick += delegate (object sender, EventArgs args) {
                if (InPauza)
                {
                    --pauze;
                    if (pauze <= 0)
                    {
                        timer.Stop();
                        Animating = false;
                        if (AutoAnimationMode) ProceedNextInQueue();
                    }
                }
                else
                {
                    if (this[i].Width > 0) { this[i].Width -= value; this.Width -= value; }
                    else
                    {
                        // stergere din vectori!!!
                        this.Controls.Remove(this[i]);
                        this.Width -= this.Padding.Left;
                        v.RemoveAt(i);
                        buttons.RemoveAt(i);
                        InPauza = true;
                    }
                }
            };
            timer.Start();
        }

        private void Insert_One(int i, int val, bool txt)
        {
            Button b = new Button();
            b.Click += CLICK;
            b.BackColor = OwnColors.Insert;
            b.ForeColor = this.ForeColor;
            b.Height = ItemSize.Height;
            b.Width = 0; // +
            b.Margin = this.Padding;
            b.Font = this.Font;
            b.TextAlign = ContentAlignment.MiddleCenter;
            b.Text = val.ToString();
            b.FlatStyle = FlatStyle.Flat;
            b.Tag = i.ToString();
            b.Location = this[i].Location;//new Point((i+1)*this.Padding.Left + i*ItemSize.Width, this.Padding.Top);
            this.Controls.Add(b);
            for (int k = i; k < Items.Count; ++k)
                this[k].Left += this.Padding.Left;
            //Thread.Sleep(Pause);
            Animating = true;

            //animatie cu timer, trb ActionQueue ca sa nu se suprapuna...
            if (txt) { Log.Text += "insert(" + i + "): " + val; Log.AppendText(Environment.NewLine); }
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer(); currentTimer = timer;
            timer.Tag = string.Format("insert {0} {1}", i, val);
            timer.Interval = AnimationSpeed;
            int value = Math.Max(ItemSize.Width / 30, 1);
            int pauze = Math.Max(Pause / timer.Interval, 1);
            bool InPauza = false;
            int plus = getsize(b).Width - ItemSize.Width;

            timer.Tick += delegate (object sender, EventArgs args) {
                if (InPauza)
                {
                    --pauze;
                    if (pauze <= 0)
                    {
                        timer.Stop();
                        Animating = false;
                        this[i].BackColor = ElemColor;
                        if (AutoAnimationMode) ProceedNextInQueue();
                    }
                }
                else
                {
                    if (b.Width < ItemSize.Width + plus)
                    {
                        b.Width += value;
                        for (int k = i; k < Items.Count; ++k)
                            this[k].Left += value;
                        this.Width += value;
                    }
                    else// if (this[i+1].Left <= this[i].Left)
                    {
                        b.Width = ItemSize.Width + plus;
                        int x = b.Left + this.Padding.Left + b.Width;
                        for (int k = i; k < Items.Count; ++k)
                        {
                            this[k].Left = x;
                            x += this[k].Width + this.Padding.Left;
                        }
                        this.Width = x;
                        // stergere din vectori!!!
                        v.Insert(i, val);
                        buttons.Insert(i, b);
                        ReOrder();
                        InPauza = true;
                    }
                }
            };
            timer.Start();
        }

        private void Insert_Last(int val, bool txt)
        {
            int i = Items.Count;//+1
            Button b = new Button();
            b.Click += CLICK;
            b.BackColor = OwnColors.Insert;
            b.ForeColor = this.ForeColor;
            b.Height = ItemSize.Height;
            b.Width = 0; // +
            b.Margin = this.Padding;
            b.Font = this.Font;
            b.TextAlign = ContentAlignment.MiddleCenter;
            b.Text = val.ToString();
            b.FlatStyle = FlatStyle.Flat;
            b.Tag = i.ToString();
            if (v.Count > 0) b.Location = new Point(this[i - 1].Left + this[i - 1].Width + this.Padding.Left, this.Padding.Top);
            else b.Location = new Point(this.Padding.Left, this.Padding.Top);
            this.Controls.Add(b);
            this.Width += this.Padding.Left;
            //Thread.Sleep(Pause);
            Animating = true;

            //animatie cu timer, trb ActionQueue ca sa nu se suprapuna...
            if (txt) { Log.Text += "add: " + val; Log.AppendText(Environment.NewLine); }
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer(); currentTimer = timer;
            timer.Tag = string.Format("insert {0} {1}", v.Count, val);
            timer.Interval = AnimationSpeed;
            int value = Math.Max(ItemSize.Width / 30, 1);
            int pauze = Math.Max(Pause / timer.Interval, 1);
            bool InPauza = false;
            int plus = getsize(b).Width - ItemSize.Width;
            timer.Tick += delegate (object sender, EventArgs args) {
                if (InPauza)
                {
                    --pauze;
                    if (pauze <= 0)
                    {
                        timer.Stop();
                        Animating = false;
                        this[i].BackColor = ElemColor;
                        if (AutoAnimationMode) ProceedNextInQueue();
                    }
                }
                else
                {
                    if (b.Width < ItemSize.Width + plus)
                    {
                        b.Width += value;
                        this.Width += value;
                    }
                    else// if (this[i+1].Left <= this[i].Left)
                    {
                        b.Width = ItemSize.Width + plus;
                        // inserare in vectori!!!
                        buttons.Insert(v.Count, b);
                        v.Add(val);
                        //buttons.Add(b);
                        ReOrder();
                        InPauza = true;
                    }
                }
            };
            timer.Start();
        }

        private void Select_Items(params int[] indexes)
        {
            Color[] l = new Color[Items.Count];
            foreach (int i in indexes)
                if (i >= 0 && i < buttons.Count)
                {
                    l[i] = this[i].BackColor;
                    this[i].BackColor = OwnColors.Select;
                }
            //Sleep(Pause);
            //if (AutoAnimationMode) ProceedNextInQueue();
            Timer timer = new Timer(); currentTimer = timer;
            timer.Tag = "select";
            timer.Interval = 1; // pt simpitate:)
            int ticks = 0;
            timer.Tick += delegate (object sender, EventArgs e)
            {
                ++ticks;
                if (ticks == Pause)
                {
                    timer.Stop();
                    Animating = false;
                    foreach (int i in indexes)
                        if (i >= 0 && i < Items.Count)
                        {
                            this[i].BackColor = l[i];
                        }
                    if (AutoAnimationMode) ProceedNextInQueue();
                }
            };
            Animating = true;
            timer.Start();
        }
        private void Mark_Items(int pause, params int[] indexes)
        {
            Timer timer = new Timer(); currentTimer = timer;
            timer.Tag = "mark";
            timer.Interval = pause;
            bool Pauza = false;
            int ticks = Pause / pause;
            int i = 0;
            timer.Tick += delegate (object sender, EventArgs e)
            {
                if (Pauza)
                {
                    --ticks;
                    if (ticks <= 0)
                    {
                        timer.Stop();
                        Animating = false;
                    }
                    ProceedNextInQueue();
                }
                else
                {
                    this[indexes[i]].BackColor = OwnColors.Marked;
                    if (i < indexes.Length - 1) ++i;
                    else Pauza = true;
                }
            };
            Animating = true;
            timer.Start();
        }
        private void Color_Items(int pause, Color c, params int[] indexes)
        {
            foreach (int i in indexes)
                if (i >= 0 && i < Items.Count)
                    this[i].BackColor = c;
            Sleep(pause);
            if(AutoAnimationMode) ProceedNextInQueue();
        }
        private void SetValue_One(int index, int value, bool txt = true)
        {
            this[index].Text = value.ToString();
            this[index].Size = getsize(this[index]);
            ReOrder();
            v[index] = value;
            if (txt) Log.Text += "v[" + index + "] = " + value;
            if (txt) Log.AppendText(Environment.NewLine);
            Animating = true;

            Timer timer = new Timer(); currentTimer = timer;
            timer.Tag = string.Format("set_value {0} {1}", index, value);
            timer.Interval = 1; // pt simpitate:)
            int ticks = 0;
            timer.Tick += delegate (object sender, EventArgs e)
            {
                ++ticks;
                if (ticks >= Pause)
                {
                    timer.Stop();
                    Animating = false;
                    if (AutoAnimationMode) ProceedNextInQueue();
                }
            };
            timer.Start();
        }
        private void Sleep_One(int s)
        {
            if (s == 0) return;
            Timer timer = new Timer(); currentTimer = timer;
            timer.Tag = "sleep";
            timer.Interval = 1; // pt simpitate:)
            int ticks = 0;
            timer.Tick += delegate (object sender, EventArgs e)
            {
                ++ticks;
                if (ticks == s)
                {
                    timer.Stop();
                    Animating = false;
                    if (AutoAnimationMode) ProceedNextInQueue();
                }
            };
            Animating = true;
            timer.Start();
        }
        public void ProceedNextInQueue() { if (AnimationQueue.Count > 0) AnimationQueue.Dequeue().Invoke(); }
        #endregion
    }
}
