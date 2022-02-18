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
        Group group = Group.Instance;
        Controller.Clipboard clipboard = Controller.Clipboard.Instance;

        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            group.Join("192.168.52.1", 31109, "239.93.11.9", 31109);
            group.Announce("WolfDesktop", "Windows", "192.168.52.1", 31109, Guid.NewGuid());
            group.Search();
            group.Discovered += Group_Discovered;
        }

        private void Group_Discovered(object sender, DiscoveredEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            group.Search();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            clipboard.Changed += Clipboard_Changed;
        }

        private void Clipboard_Changed(object sender, Controller.Clipboard.ClipboardChangedEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                textBox1.Text = (string)e.DataObject.GetData(DataFormats.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            group.Announce("WolfDesktop", "Windows", "192.168.52.1", 31109, Guid.NewGuid());
        }
    }
}
