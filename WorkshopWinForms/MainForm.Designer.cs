namespace WorkshopWinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.labelWelcome = new System.Windows.Forms.Label();
            this.labelUserInfo = new System.Windows.Forms.Label();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.buttonChangePassword = new System.Windows.Forms.Button();
            this.panelAdmin = new System.Windows.Forms.Panel();
            this.buttonChangeRole = new System.Windows.Forms.Button();
            this.labelAdminPanel = new System.Windows.Forms.Label();
            this.panelAdmin.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelWelcome
            // 
            this.labelWelcome.AutoSize = true;
            this.labelWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelWelcome.Location = new System.Drawing.Point(12, 9);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(154, 20);
            this.labelWelcome.TabIndex = 0;
            this.labelWelcome.Text = "Добро пожаловать!";
            // 
            // labelUserInfo
            // 
            this.labelUserInfo.AutoSize = true;
            this.labelUserInfo.Location = new System.Drawing.Point(12, 40);
            this.labelUserInfo.Name = "labelUserInfo";
            this.labelUserInfo.Size = new System.Drawing.Size(150, 15);
            this.labelUserInfo.TabIndex = 1;
            this.labelUserInfo.Text = "Пользователь: ... Роль: ...";
            // 
            // buttonChangePassword
            // 
            this.buttonChangePassword.Location = new System.Drawing.Point(12, 70);
            this.buttonChangePassword.Name = "buttonChangePassword";
            this.buttonChangePassword.Size = new System.Drawing.Size(260, 35);
            this.buttonChangePassword.TabIndex = 2;
            this.buttonChangePassword.Text = "Сменить пароль";
            this.buttonChangePassword.UseVisualStyleBackColor = true;
            this.buttonChangePassword.Click += new System.EventHandler(this.buttonChangePassword_Click);
            // 
            // panelAdmin
            // 
            this.panelAdmin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAdmin.Controls.Add(this.buttonChangeRole);
            this.panelAdmin.Controls.Add(this.labelAdminPanel);
            this.panelAdmin.Location = new System.Drawing.Point(12, 120);
            this.panelAdmin.Name = "panelAdmin";
            this.panelAdmin.Size = new System.Drawing.Size(260, 100);
            this.panelAdmin.TabIndex = 3;
            this.panelAdmin.Visible = false;
            // 
            // labelAdminPanel
            // 
            this.labelAdminPanel.AutoSize = true;
            this.labelAdminPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelAdminPanel.ForeColor = System.Drawing.Color.DarkRed;
            this.labelAdminPanel.Location = new System.Drawing.Point(10, 10);
            this.labelAdminPanel.Name = "labelAdminPanel";
            this.labelAdminPanel.Size = new System.Drawing.Size(200, 17);
            this.labelAdminPanel.TabIndex = 0;
            this.labelAdminPanel.Text = "ПАНЕЛЬ АДМИНИСТРАТОРА";
            // 
            // buttonChangeRole
            // 
            this.buttonChangeRole.Location = new System.Drawing.Point(13, 40);
            this.buttonChangeRole.Name = "buttonChangeRole";
            this.buttonChangeRole.Size = new System.Drawing.Size(230, 35);
            this.buttonChangeRole.TabIndex = 1;
            this.buttonChangeRole.Text = "Сменить роль пользователя";
            this.buttonChangeRole.UseVisualStyleBackColor = true;
            this.buttonChangeRole.Click += new System.EventHandler(this.buttonChangeRole_Click);
            // 
            // buttonLogout
            // 
            this.buttonLogout.Location = new System.Drawing.Point(12, 235);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(260, 35);
            this.buttonLogout.TabIndex = 4;
            this.buttonLogout.Text = "Выйти";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 280);
            this.Controls.Add(this.buttonLogout);
            this.Controls.Add(this.panelAdmin);
            this.Controls.Add(this.buttonChangePassword);
            this.Controls.Add(this.labelUserInfo);
            this.Controls.Add(this.labelWelcome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главное меню";
            this.panelAdmin.ResumeLayout(false);
            this.panelAdmin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label labelWelcome;
        private System.Windows.Forms.Label labelUserInfo;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.Button buttonChangePassword;
        private System.Windows.Forms.Panel panelAdmin;
        private System.Windows.Forms.Button buttonChangeRole;
        private System.Windows.Forms.Label labelAdminPanel;
    }
}