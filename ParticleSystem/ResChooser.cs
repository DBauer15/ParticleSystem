using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ParticleSystem
{
    public partial class ResChooser : Form
    {
        public int Width { get {return Convert.ToInt32(X.Text);}  }
        public int Height { get { return Convert.ToInt32(Y.Text); } }
        public int Amount { get { return Convert.ToInt32(A.Text); } }
        public bool reduceVel { get { return checkBox1.Checked; } }

        public ResChooser()
        {
            InitializeComponent();
        }
    }
}
