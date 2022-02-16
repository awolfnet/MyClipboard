using Component;
using Component.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsDesktop.Controller;

namespace WindowsDesktop.View
{
    public partial class Main : Form
    {
        IGroup group;
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            group = Group.Instance;
            group.Join("192.168.52.1", 0, "239.93.11.9", 31109);
            group.Search();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            group.Search();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
