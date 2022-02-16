using Component;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsDesktop.View
{
    public partial class Main : Form
    {
        MulticastGroup group;
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            group = new MulticastGroup("239.93.11.9", 31109);
            group.Join();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            group.Send("AAAAAAAA");
        }
    }
}
