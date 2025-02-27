using log4cxx;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace CloudManager
{
    public partial class CloudManagerForm : Form
    {
        public static CloudManagerForm f;
        public CloudManagerForm()
        {
            InitializeComponent();
            f = this;
        }

        private void CloudManagerForm_Load(object sender, EventArgs e)
        {
            ServerNumCb.SelectedIndex = 0;

            CmdHelper.ProcessInit();
            this.minioUsernameTb.Text = tools.GetConfigValue("MINIO_ROOT_USER");
            this.minioPasswordTb.Text = tools.GetConfigValue("MINIO_ROOT_PASSWORD");
            this.minioConsolePortTb.Text = tools.GetConfigValue("MINIO_CONSOLE_PORT");
            this.minioApiPortTb.Text = tools.GetConfigValue("MINIO_API_PORT");
            this.PrometheusIdTb.Text = tools.GetConfigValue("MINIO_PROMETHEUS_JOB_ID");
            this.PrometheusUrlTb.Text = tools.GetConfigValue("MINIO_PROMETHEUS_URL");
            this.AuthTypeTb.Text = tools.GetConfigValue("MINIO_PROMETHEUS_AUTH_TYPE");
            if (singleCloudRd.Checked)//单机云
            {
                this.minioServerTb.Enabled = false;
            }
            else
            {
                this.minioServerTb.Enabled = true;
            }
        }


        /// <summary>
        /// 结束进程
        /// </summary>
        /// <param name="threadName"></param>
        /// <param name="kill"></param>
        /// <returns></returns>
        public bool ThreadExitis(string threadName, bool kill)
        {
            bool bo = false;

            System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process process in processList)
            {
                if (process.ProcessName.ToLower() == threadName.ToLower())
                {
                    if (kill)
                    {
                        bo = false;
                        process.Kill(); //结束进程 
                    }
                    else
                    {
                        bo = true;
                    }
                }
            }

            return bo;
        }


        //开始部署
        private void StartInstallBtn_Click_1(object sender, EventArgs e)
        {
            tools.SetConfigValue("MINIO_CONSOLE_PORT", this.minioConsolePortTb.Text);
            tools.SetConfigValue("MINIO_API_PORT", this.minioApiPortTb.Text);
            string cmd = "";
            this.MessageShowTb.Text = "";
            ThreadExitis("minio", true);
            int count = minioListView.Items.Count;
            if (count < 1)
            {
                MessageBox.Show("必须先添加挂载磁盘");
                return;
            }
            this.MessageShowTb.AppendText("开始部署..." + "\r\n");

            string consolePort = this.minioConsolePortTb.Text;
            string apiPort = this.minioApiPortTb.Text;
            this.MessageShowTb.AppendText("api端口：" + apiPort + "\r\n");
            if (singleCloudRd.Checked)//单机云
            {
                this.MessageShowTb.AppendText("组建模式：单机云" + "\r\n");
                CmdHelper.ProcessInit();
                string driver = minioListView.Items[0].SubItems[1].Text;
                cmd = "start minio.exe server " + driver + " --console-address \":" + consolePort + "\" --address \":" + apiPort + "\"";
                CmdHelper.ExecuteTclCommand(cmd);
                this.MessageShowTb.AppendText(CmdHelper.outputstring);
            }
            else//分布式集群云
            {

                this.MessageShowTb.AppendText("组建模式：分布式集群云" + "\r\n");
                if (minioListView.Items.Count < 4 || minioListView.Items.Count > 16)
                {
                    MessageBox.Show("集群模式必须最小4节点最大16节点");
                    this.MessageShowTb.AppendText("终止执行" + "\r\n");
                    return;
                }
                string node = "";
                foreach (ListViewItem item in minioListView.Items)
                {
                    string server = item.SubItems[0].Text;
                    string data = item.SubItems[1].Text;
                    node = node + "http://" + server + "/" + data + " ";
                }
                cmd = "start minio.exe server " + node + " --console-address \":" + consolePort + "\" --address \":" + apiPort + "\"";
                CmdHelper.ExecuteTclCommand(cmd);
                this.MessageShowTb.AppendText(CmdHelper.outputstring);
                this.MessageShowTb.AppendText("注意：所有节点都要执行！" + "\r\n");
            }

            this.MessageShowTb.AppendText("启动成功,不要关闭弹出的服务窗口！！！" + "\r\n");
            this.MessageShowTb.AppendText("登录http://节点IP:" + consolePort + "进行后续配置" + "\r\n");

        }
        //选择云组建模式
        private void singleCloudRd_CheckedChanged(object sender, EventArgs e)
        {
            if (singleCloudRd.Checked)//单机云
            {
                this.minioServerTb.Enabled = false;

            }
            else
            {
                this.minioServerTb.Enabled = true;
            }
        }
        //添加服务器和挂载磁盘
        private void minioListViewAddBtn_Click_1(object sender, EventArgs e)
        {
            string server = this.minioServerTb.Text;
            string driver = this.minioDriverTb.Text;
            if (singleCloudRd.Checked)//单机云
            {
                int count = minioListView.Items.Count;
                if (count >= 1)
                {
                    MessageBox.Show("单机云部署无法添加多台服务器");
                }
                else
                {
                    ListViewItem item = new ListViewItem(new string[] { server, driver });
                    item.Name = server;
                    minioListView.Items.Add(item);
                }
            }
            else
            {
                ListViewItem item = new ListViewItem(new string[] { server, driver });
                item.Name = server;
                //if (minioListView.Items.ContainsKey(item.Name))
                //{
                //    MessageBox.Show("服务器已存在");
                //    return;
                //}
                minioListView.Items.Add(item);
            }
        }

        private void minioListViewDelBtn_Click_1(object sender, EventArgs e)
        {
            if (minioListView.SelectedItems.Count > 0)//判断lv有被选中项
            {
                minioListView.Items.Remove(minioListView.SelectedItems[0]);   //按项移除
            }
        }
        //配置环境变量（用户名和密码）
        private void ConfigEnvBtn_Click(object sender, EventArgs e)
        {
            string username = this.minioUsernameTb.Text;
            string password = this.minioPasswordTb.Text;
            string prometheusID = this.PrometheusIdTb.Text;
            string prometheusUrl = this.PrometheusUrlTb.Text;
            string authType = this.AuthTypeTb.Text;

            CmdHelper.ExecuteTclCommand("SETX MINIO_ROOT_USER " + username + "&" + "SETX MINIO_ROOT_PASSWORD " + password
                + "&" + "SETX MINIO_PROMETHEUS_JOB_ID " + prometheusID + "&" + "SETX MINIO_PROMETHEUS_URL " + prometheusUrl
                + "&" + "SETX MINIO_PROMETHEUS_AUTH_TYPE " + authType);
            tools.SetConfigValue("MINIO_ROOT_USER", username);
            tools.SetConfigValue("MINIO_ROOT_PASSWORD", password);
            tools.SetConfigValue("MINIO_PROMETHEUS_JOB_ID", prometheusID);
            tools.SetConfigValue("MINIO_PROMETHEUS_URL", prometheusUrl);
            tools.SetConfigValue("MINIO_PROMETHEUS_AUTH_TYPE", authType);

            if (MessageBox.Show("配置完成重启软件生效，是否立即关闭？", "提示", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.ExitThread();
            }
        }



        //委托违法展示
        delegate void delegate_showMessage(string msg);
        public void showMessage(string msg)
        {
            if (this.resultTb.InvokeRequired)
            {
                delegate_showMessage bv = new delegate_showMessage(showMessage);
                this.Invoke(bv, new object[] { msg });
            }
            else
            {
                this.resultTb.AppendText(msg + "\r\n");
            }
        }
        //服务器查询
        private void getServerBtn_Click(object sender, EventArgs e)
        {
            McHelper.GetServers();
        }
        //桶查询
        private void findBucketBtn_Click(object sender, EventArgs e)
        {
            McHelper.GetListBuckets(this.serverNameTb.Text);
        }
        //清空显示
        private void clearTb_Click(object sender, EventArgs e)
        {
            this.resultTb.Text = "";
        }
        //服务器添加
        private void addServerBtn_Click(object sender, EventArgs e)
        {
            AddServer addServer = new AddServer();
            addServer.Show();
        }
        //服务器移除
        private void removeServerBtn_Click(object sender, EventArgs e)
        {
            DelServer delServer = new DelServer();
            delServer.Show();
        }
        //添加桶
        private void addBucketBtn_Click(object sender, EventArgs e)
        {
            McHelper.addBucket(this.serverNameTb.Text, this.bucketNameTb.Text);
        }
        //删除桶
        private void removeBucketBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.serverNameTb.Text))
            {
                MessageBox.Show("服务器不能为空");
                return;
            }
            if (string.IsNullOrEmpty(this.bucketNameTb.Text))
            {
                MessageBox.Show("桶不能为空");
                return;
            }
            McHelper.delBucket(this.serverNameTb.Text, this.bucketNameTb.Text);
        }

        //上传对象
        private void addObjectBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "任意文件(*.*)|*.*";//文件的类型及说明
            if (dlg.ShowDialog() == DialogResult.OK)//选中确定后
            {
                string filePath = dlg.FileName;
                objectNameTb.Text = filePath;

            }
            if (this.objectNameTb.Text.Equals("双击导入") || string.IsNullOrEmpty(this.objectNameTb.Text))
            {
                MessageBox.Show("未导入对象");
                return;
            }
            if (string.IsNullOrEmpty(this.serverNameTb.Text))
            {
                MessageBox.Show("服务器不能为空");
                return;
            }
            if (string.IsNullOrEmpty(this.bucketNameTb.Text))
            {
                MessageBox.Show("桶不能为空");
                return;
            }
            McHelper.addObject(this.serverNameTb.Text, this.bucketNameTb.Text, this.objectNameTb.Text);
        }
        //查询指定桶
        private void getBucketBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.serverNameTb.Text))
            {
                MessageBox.Show("服务器不能为空");
                return;
            }
            if (string.IsNullOrEmpty(this.bucketNameTb.Text))
            {
                MessageBox.Show("桶不能为空");
                return;
            }
            McHelper.getBucket(this.serverNameTb.Text, this.bucketNameTb.Text);
        }
        //下载对象
        private void downloadObjectBtn_Click(object sender, EventArgs e)
        {
            if (this.objectNameTb.Text.Equals("双击导入") || string.IsNullOrEmpty(this.objectNameTb.Text))
            {
                MessageBox.Show("未指定对象");
                return;
            }
            if (string.IsNullOrEmpty(this.objectNameTb.Text))
            {
                MessageBox.Show("下载对象名称不能为空");
                return;
            }
            if (string.IsNullOrEmpty(this.serverNameTb.Text))
            {
                MessageBox.Show("服务器不能为空");
                return;
            }
            if (string.IsNullOrEmpty(this.bucketNameTb.Text))
            {
                MessageBox.Show("桶不能为空");
                return;
            }
            string filePath = "";
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "任意文件(*.*)|*.*";//文件的类型及说明
            sf.FileName = this.objectNameTb.Text;
            if (sf.ShowDialog() == DialogResult.OK)//选中确定后
            {
                filePath = sf.FileName;
            }
            McHelper.downloadObject(this.serverNameTb.Text, this.bucketNameTb.Text, this.objectNameTb.Text, filePath);
        }
        //查询服务器存储状态
        private void getDuBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.serverNameTb.Text))
            {
                MessageBox.Show("服务器不能为空");
                return;
            }
            McHelper.getServerDu(this.serverNameTb.Text);
        }
        //查询桶存储状态
        private void getBucketDuBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.serverNameTb.Text))
            {
                MessageBox.Show("服务器不能为空");
                return;
            }
            if (string.IsNullOrEmpty(this.bucketNameTb.Text))
            {
                MessageBox.Show("桶不能为空");
                return;
            }
            McHelper.getBucketDu(this.serverNameTb.Text, this.bucketNameTb.Text);
        }
        //通过url获取文件
        private void getShareUrlBtn_Click(object sender, EventArgs e)
        {
            if (this.objectNameTb.Text.Equals("双击导入") || string.IsNullOrEmpty(this.objectNameTb.Text))
            {
                MessageBox.Show("未指定对象");
                return;
            }
            if (string.IsNullOrEmpty(this.objectNameTb.Text))
            {
                MessageBox.Show("对象不能为空");
                return;
            }
            if (string.IsNullOrEmpty(this.serverNameTb.Text))
            {
                MessageBox.Show("服务器不能为空");
                return;
            }
            if (string.IsNullOrEmpty(this.bucketNameTb.Text))
            {
                MessageBox.Show("桶不能为空");
                return;
            }
            McHelper.getShareUrl(this.serverNameTb.Text, this.bucketNameTb.Text, this.objectNameTb.Text);
        }
        //通过url上传文件
        private void getUploadBtn_Click(object sender, EventArgs e)
        {
            if (this.objectNameTb.Text.Equals("双击导入") || string.IsNullOrEmpty(this.objectNameTb.Text))
            {
                MessageBox.Show("未导入对象");
                return;
            }
            if (string.IsNullOrEmpty(this.objectNameTb.Text))
            {
                MessageBox.Show("对象不能为空");
                return;
            }
            if (string.IsNullOrEmpty(this.serverNameTb.Text))
            {
                MessageBox.Show("服务器不能为空");
                return;
            }
            if (string.IsNullOrEmpty(this.bucketNameTb.Text))
            {
                MessageBox.Show("桶不能为空");
                return;
            }
            McHelper.getUploadUrl(this.serverNameTb.Text, this.bucketNameTb.Text, this.objectNameTb.Text);
        }
        //删除对象
        private void removeObjectBtn_Click(object sender, EventArgs e)
        {
            if (this.objectNameTb.Text.Equals("双击导入") || string.IsNullOrEmpty(this.objectNameTb.Text))
            {
                MessageBox.Show("未指定对象");
                return;
            }
            if (string.IsNullOrEmpty(this.objectNameTb.Text))
            {
                MessageBox.Show("对象不能为空");
                return;
            }
            if (string.IsNullOrEmpty(this.serverNameTb.Text))
            {
                MessageBox.Show("服务器不能为空");
                return;
            }
            if (string.IsNullOrEmpty(this.bucketNameTb.Text))
            {
                MessageBox.Show("桶不能为空");
                return;
            }
            McHelper.delObject(this.serverNameTb.Text, this.bucketNameTb.Text, this.objectNameTb.Text);
        }
        //纠删码计算器
        private void calculationBtn_Click(object sender, EventArgs e)
        {
            int serverNum = int.Parse(ServerNumCb.Text);
            if (serverNum < 4)
            {
                MessageBox.Show("纠删码集群服务器最小4台");
                return;
            }
            int driverNum = int.Parse(DriverNumCb.Text);
            int driverCap = int.Parse(DriveCapacityCb.Text);
            double totalCap = serverNum * driverNum * driverCap;

            RawCapacityLb.Text = totalCap.ToString();

            double ErasureCode = int.Parse(ErasureCodeParityCb.Text);
            double StripeSize = int.Parse(StripeSizeLb.Text);
            double Efficiency = 1 - (ErasureCode / StripeSize);
            StorageEfficiencyLb.Text = ((int)(Efficiency * 100)).ToString() + "%";

            double UsableCapacity = totalCap * Efficiency;
            UsableCapacityLb.Text = UsableCapacity.ToString();

            if ((StripeSize / ErasureCode) == 2)
            {
                double DriveFailureNum = (StripeSize - (ErasureCode + 1)) * serverNum * driverNum / StripeSize;
                DriveFailureNumLb.Text = DriveFailureNum.ToString();
                ServerFailureNumLb.Text = ((int)(DriveFailureNum / driverNum)).ToString();
            }
            else
            {
                double DriveFailureNum = serverNum * driverNum * ErasureCode / StripeSize;

                DriveFailureNumLb.Text = DriveFailureNum.ToString();
                ServerFailureNumLb.Text = ((int)(DriveFailureNum / driverNum)).ToString();
            }

        }
        //获取磁盘数量对应的纠删码条带大小
        public int IsDivisble(int driveNum, int jz)
        {
            for (int i = jz - 1; i > 0; i--)
            {
                if ((driveNum % i) == 0)
                {
                    return i;
                }

            }
            return 0;
        }
        //服务器combobox值改变事件
        private void ServerNumCb_SelectedValueChanged(object sender, EventArgs e)
        {
            int StripeSize = 4;
            int serverNum = int.Parse(ServerNumCb.Text);
            DriverNumCb.SelectedIndex = 1;
            int driverNum = int.Parse(DriverNumCb.Text);
            if (serverNum * driverNum <= 16)
            {
                StripeSize = serverNum * driverNum;
            }
            else
            {
                int num = 0;
                for (int j = 1; j <= driverNum; j++)
                {
                    if (j * serverNum > 16)
                    {
                        num = j;
                        break;
                    }
                }

                int x = IsDivisble(driverNum, num);
                StripeSize = serverNum * x;
            }
            ErasureCodeParityCb.Items.Clear();
            int y = StripeSize / 2;
            for (int i = 2; i <= y; i++)
            {
                ErasureCodeParityCb.Items.Add(i);
            }
            StripeSizeLb.Text = StripeSize.ToString();
            ErasureCodeParityCb.SelectedIndex = ErasureCodeParityCb.Items.Count - 1;
        }
        //磁盘combobox值改变事件
        private void DriverNumCb_SelectedValueChanged(object sender, EventArgs e)
        {
            int StripeSize = 4;
            int serverNum = int.Parse(ServerNumCb.Text);
            int driverNum = int.Parse(DriverNumCb.Text);
            if (serverNum * driverNum <= 16)
            {
                StripeSize = serverNum * driverNum;
            }
            else
            {
                int num = 0;
                for (int j = 1; j <= driverNum; j++)
                {
                    if (j * serverNum > 16)
                    {
                        num = j;
                        break;
                    }
                }
                int x = IsDivisble(driverNum, num);
                StripeSize = serverNum * x;
            }
            ErasureCodeParityCb.Items.Clear();
            int y = StripeSize / 2;
            for (int i = 2; i <= y; i++)
            {
                ErasureCodeParityCb.Items.Add(i);
            }
            StripeSizeLb.Text = StripeSize.ToString();
            ErasureCodeParityCb.SelectedIndex = ErasureCodeParityCb.Items.Count - 1;
        }
        //集群信息
        private void ClusterInfoBtn_Click(object sender, EventArgs e)
        {
            McHelper.GetClusterInfo(this.serverNameTb.Text);
        }

        //回车输入cmd
        private void cmdTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                McHelper.InputCmd(cmdTb.Text);
            }
        }
        //手动输入cmd命令
        private void sendBtn_Click(object sender, EventArgs e)
        {
            McHelper.InputCmd(cmdTb.Text);
        }
        int flag = -1;
        private void encodingBtn_Click(object sender, EventArgs e)
        {
            if (flag < 0)
            {
                encodingBtn.Text = "utf-8";
                flag = 1;
                CmdHelper.encode = "utf-8";
            }
            else
            {
                encodingBtn.Text = "gb2312";
                flag = -1;
                CmdHelper.encode = "gb2312";
            }
        }
    }
}
