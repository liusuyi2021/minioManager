
namespace CloudManager
{
    partial class AddServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.minioPasswordLb = new System.Windows.Forms.Label();
            this.minioUsernameLb = new System.Windows.Forms.Label();
            this.UserTb = new System.Windows.Forms.TextBox();
            this.PasswordTb = new System.Windows.Forms.TextBox();
            this.PortTb = new System.Windows.Forms.TextBox();
            this.ServerTb = new System.Windows.Forms.TextBox();
            this.minioServerLb = new System.Windows.Forms.Label();
            this.minioConsolePortLb = new System.Windows.Forms.Label();
            this.addBtn = new System.Windows.Forms.Button();
            this.serverNameTb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // minioPasswordLb
            // 
            this.minioPasswordLb.AutoSize = true;
            this.minioPasswordLb.Location = new System.Drawing.Point(47, 138);
            this.minioPasswordLb.Name = "minioPasswordLb";
            this.minioPasswordLb.Size = new System.Drawing.Size(41, 12);
            this.minioPasswordLb.TabIndex = 41;
            this.minioPasswordLb.Text = "密码：";
            // 
            // minioUsernameLb
            // 
            this.minioUsernameLb.AutoSize = true;
            this.minioUsernameLb.Location = new System.Drawing.Point(35, 111);
            this.minioUsernameLb.Name = "minioUsernameLb";
            this.minioUsernameLb.Size = new System.Drawing.Size(53, 12);
            this.minioUsernameLb.TabIndex = 39;
            this.minioUsernameLb.Text = "用户名：";
            // 
            // UserTb
            // 
            this.UserTb.Location = new System.Drawing.Point(94, 108);
            this.UserTb.Name = "UserTb";
            this.UserTb.Size = new System.Drawing.Size(100, 21);
            this.UserTb.TabIndex = 40;
            this.UserTb.Text = "admin";
            // 
            // PasswordTb
            // 
            this.PasswordTb.Location = new System.Drawing.Point(94, 135);
            this.PasswordTb.Name = "PasswordTb";
            this.PasswordTb.Size = new System.Drawing.Size(100, 21);
            this.PasswordTb.TabIndex = 42;
            this.PasswordTb.Text = "xzx12345";
            // 
            // PortTb
            // 
            this.PortTb.Location = new System.Drawing.Point(94, 81);
            this.PortTb.Name = "PortTb";
            this.PortTb.Size = new System.Drawing.Size(100, 21);
            this.PortTb.TabIndex = 46;
            this.PortTb.Text = "9001";
            // 
            // ServerTb
            // 
            this.ServerTb.Location = new System.Drawing.Point(94, 54);
            this.ServerTb.Name = "ServerTb";
            this.ServerTb.Size = new System.Drawing.Size(100, 21);
            this.ServerTb.TabIndex = 45;
            this.ServerTb.Text = "127.0.0.1";
            // 
            // minioServerLb
            // 
            this.minioServerLb.AutoSize = true;
            this.minioServerLb.Location = new System.Drawing.Point(35, 57);
            this.minioServerLb.Name = "minioServerLb";
            this.minioServerLb.Size = new System.Drawing.Size(53, 12);
            this.minioServerLb.TabIndex = 44;
            this.minioServerLb.Text = "ip地址：";
            // 
            // minioConsolePortLb
            // 
            this.minioConsolePortLb.AutoSize = true;
            this.minioConsolePortLb.Location = new System.Drawing.Point(47, 84);
            this.minioConsolePortLb.Name = "minioConsolePortLb";
            this.minioConsolePortLb.Size = new System.Drawing.Size(41, 12);
            this.minioConsolePortLb.TabIndex = 43;
            this.minioConsolePortLb.Text = "端口：";
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(83, 185);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(87, 35);
            this.addBtn.TabIndex = 47;
            this.addBtn.Text = "添加";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // serverNameTb
            // 
            this.serverNameTb.Location = new System.Drawing.Point(94, 27);
            this.serverNameTb.Name = "serverNameTb";
            this.serverNameTb.Size = new System.Drawing.Size(100, 21);
            this.serverNameTb.TabIndex = 49;
            this.serverNameTb.Text = "minioapi";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 48;
            this.label1.Text = "服务器名称：";
            // 
            // AddServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 232);
            this.Controls.Add(this.serverNameTb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.PortTb);
            this.Controls.Add(this.ServerTb);
            this.Controls.Add(this.minioServerLb);
            this.Controls.Add(this.minioConsolePortLb);
            this.Controls.Add(this.minioPasswordLb);
            this.Controls.Add(this.minioUsernameLb);
            this.Controls.Add(this.UserTb);
            this.Controls.Add(this.PasswordTb);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "AddServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加服务器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label minioPasswordLb;
        private System.Windows.Forms.Label minioUsernameLb;
        private System.Windows.Forms.TextBox UserTb;
        private System.Windows.Forms.TextBox PasswordTb;
        private System.Windows.Forms.TextBox PortTb;
        private System.Windows.Forms.TextBox ServerTb;
        private System.Windows.Forms.Label minioServerLb;
        private System.Windows.Forms.Label minioConsolePortLb;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.TextBox serverNameTb;
        private System.Windows.Forms.Label label1;
    }
}