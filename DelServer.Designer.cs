
namespace CloudManager
{
    partial class DelServer
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
            this.serverNameTb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.delBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serverNameTb
            // 
            this.serverNameTb.Location = new System.Drawing.Point(100, 59);
            this.serverNameTb.Name = "serverNameTb";
            this.serverNameTb.Size = new System.Drawing.Size(100, 21);
            this.serverNameTb.TabIndex = 51;
            this.serverNameTb.Text = "minioapi";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 50;
            this.label1.Text = "服务器名称：";
            // 
            // delBtn
            // 
            this.delBtn.Location = new System.Drawing.Point(76, 160);
            this.delBtn.Name = "delBtn";
            this.delBtn.Size = new System.Drawing.Size(87, 35);
            this.delBtn.TabIndex = 52;
            this.delBtn.Text = "删除";
            this.delBtn.UseVisualStyleBackColor = true;
            this.delBtn.Click += new System.EventHandler(this.delBtn_Click);
            // 
            // DelServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 230);
            this.Controls.Add(this.delBtn);
            this.Controls.Add(this.serverNameTb);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DelServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "删除服务器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox serverNameTb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button delBtn;
    }
}