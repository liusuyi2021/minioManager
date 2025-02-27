using System;
using System.Windows.Forms;

namespace CloudManager
{
    public partial class AddServer : Form
    {
        public AddServer()
        {
            InitializeComponent();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            string minioname = this.serverNameTb.Text;
            string username = UserTb.Text;
            string password = PasswordTb.Text;
            string server = ServerTb.Text;
            string port = PortTb.Text;
            McHelper.AddServer(minioname,server, port,username,password);

        }
    }
}
