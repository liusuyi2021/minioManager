using System;
using System.Windows.Forms;

namespace CloudManager
{
    public partial class DelServer : Form
    {
        public DelServer()
        {
            InitializeComponent();
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            string minioname = this.serverNameTb.Text;          
            McHelper.RemoveServer(minioname);
        }
    }
}
