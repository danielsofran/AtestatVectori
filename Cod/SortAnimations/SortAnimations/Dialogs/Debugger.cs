using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortAnimations
{
    public partial class Debuger : Form
    {
        string e;
        string v;
        public string Expresie { get => e; set { e = value; label2.Text = e; this.BringToFront(); } }
        public string Valoare { get =>v; set { v = value; label4.Text = v.ToString(); this.BringToFront(); } }
        public Debuger()
        {
            InitializeComponent();
        }

        private void Debuger_Load(object sender, EventArgs e)
        {

        }
    }
}
