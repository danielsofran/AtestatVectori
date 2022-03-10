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
        #region STL
        public void Swap(int i, int j)
        {
            ++nrswaps;
            if (i < 0 || j < 0 || i == j) return;
            if (!Animating && AnimationQueue.Count==0) Swap_One(i, j);
            else AnimationQueue.Enqueue(() => Swap_One(i, j));
        }
        public void Erase(int i)
        {
            if (i >= 0 && i < v.Count)
            {
                if (!Animating && AnimationQueue.Count == 0)
                {
                    if (i == Items.Count - 1) Erase_Last();
                    else Erase_One(i);
                }
                else
                {
                    if (i == Items.Count - 1) AnimationQueue.Enqueue(() => Erase_Last());
                    else AnimationQueue.Enqueue(() => Erase_One(i));
                }
            }
            else MessageBox.Show("eroare");
        }
        public void Insert(int i, int val, bool txt = true)
        {
            if (!Animating && AnimationQueue.Count == 0)
            {
                if (i == Items.Count) Insert_Last(val, txt);
                else Insert_One(i, val, txt);
            }
            else
            {
                if (i == Items.Count) AnimationQueue.Enqueue(() => Insert_Last(val, txt));
                else AnimationQueue.Enqueue(() => Insert_One(i, val, txt));
            }
        }
        public void Pop_back() => Erase(Items.Count - 1);
        public void Pop_front() => Erase(0);
        public void Add(int val, bool txt = true) => Insert(Items.Count, val, txt);
        public void Select(params int[] indexes)
        {
            if (!Animating && AnimationQueue.Count == 0) Select_Items(indexes);
            else AnimationQueue.Enqueue(() => Select_Items(indexes));
        }
        public void Mark(int pause, params int[] indexes)
        {
            if (!Animating && AnimationQueue.Count == 0) Mark_Items(pause, indexes);
            else AnimationQueue.Enqueue(() => Mark_Items(pause, indexes));
        }
        public void SetValue(int i, int val, bool txt = true)
        {
            if (!Animating && AnimationQueue.Count == 0)
            {
                SetValue_One(i, val, txt);
            }
            else
            {
                AnimationQueue.Enqueue(() => SetValue_One(i, val, txt));
            }
        }
        public void SetColor(Color c, int pause, params int[] indexes)
        {
            if (!Animating && AnimationQueue.Count == 0) Color_Items(pause, c, indexes);
            else AnimationQueue.Enqueue(() => Color_Items(pause, c, indexes));
        }
        public void Sleep(int millis)
        {
            if (!Animating && AnimationQueue.Count == 0) Sleep_One(millis);
            else AnimationQueue.Enqueue(() => Sleep_One(millis));
        }
        public void AtTimeAction(Action action, bool manual_stop = false)
        {
            Action action1 = delegate { action.Invoke(); if (!manual_stop) { Animating = false; ProceedNextInQueue(); } };
            if (!Animating && AnimationQueue.Count == 0) action1.Invoke();
            else AnimationQueue.Enqueue(action1);
        }
        public void AppendText(string log)
        {
            Action a = delegate {
                Log.Text += log;
                ProceedNextInQueue();
            };
            if (!Animating&&AnimationQueue.Count==0) a.Invoke();
            else AnimationQueue.Enqueue(a);
        }
        public void ToEnd()
        {
            Log.SelectionStart = Log.Text.Length;
            Log.SelectionLength = 0;
        }
        public void ShowSwaps()
        {
            Action a = new Action(delegate { Log.AppendText(Environment.NewLine); Log.Text += "Număr de interschimbări: " + nrswaps; Log.AppendText(Environment.NewLine); nrswaps = 0; });
            if (!Animating && AnimationQueue.Count == 0) a.Invoke();
            else AnimationQueue.Enqueue(a);
        }
        public void InsertSwaps(int index)
        {
            for (int i = Items.Count - 2; i >= index; --i)
            {
                Select(i, i + 1);
                SetValue(i + 1, Items[i]);
            }
        }
        public void FinalizeInsert(int index, int val)
        {
            Action a = delegate
            {
                /*Color c = OwnColors.Select;
                OwnColors.Select = OwnColors.Insert;
                Select(index);
                SetValue(index, val);
                this.AtTimeAction(delegate { OwnColors.Select = c; ColorDefault(); });
                MessageBox.Show("in finalize action");*/
                Items[index] = val;
                ReMakeButtons();
                toolStripMenuItem3_Click(this, null);
            };
            if (!Animating && AnimationQueue.Count==0) a.Invoke();
            else AnimationQueue.Enqueue(a);
        }
        public void ColorDefault()
        {
            foreach (Button b in buttons)
                b.BackColor = ElemColor;
        }
        #endregion
    }
}
