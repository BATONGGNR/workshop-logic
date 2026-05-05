namespace WorkshopWinForms
{
    partial class ChangePasswordForm
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
            this.labelOldPassword = new System.Windows.Forms.Label();
            this.textBoxOldPassword = new System.Windows.Forms.TextBox();
            this.labelNewPassword = new System.Windows.Forms.Label();
            this.textBoxNewPassword = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelOldPassword
            // 
            this.labelOldPassword.AutoSize = true;
            this.labelOldPassword.Location = new System.Drawing.Point(12, 9);
            this.labelOldPassword.Name = "labelOldPassword";
            this.labelOldPassword.Size = new System.Drawing.Size(84, 15);
            this.labelOldPassword.TabIndex = 0;
            this.labelOldPassword.Text = "Старый пароль:";
            // 
            // textBoxOldPassword
            // 
            this.textBoxOldPassword.Location = new System.Drawing.Point(12, 25);
            this.textBoxOldPassword.Name = "textBoxOldPassword";
            this.textBoxOldPassword.PasswordChar = '*';
            this.textBoxOldPassword.Size = new System.Drawing.Size(260, 23);
            this.textBoxOldPassword.TabIndex = 1;
            // 
            // labelNewPassword
            // 
            this.labelNewPassword.AutoSize = true;
            this.labelNewPassword.Location = new System.Drawing.Point(12, 60);
            this.labelNewPassword.Name = "labelNewPassword";
            this.labelNewPassword.Size = new System.Drawing.Size(78, 15);
            this.labelNewPassword.TabIndex = 2;
            this.labelNewPassword.Text = "Новый пароль:";
            // 
            // textBoxNewPassword
            // 
            this.textBoxNewPassword.Location = new System.Drawing.Point(12, 76);
            this.textBoxNewPassword.Name = "textBoxNewPassword";
            this.textBoxNewPassword.PasswordChar = '*';
            this.textBoxNewPassword.Size = new System.Drawing.Size(260, 23);
            this.textBoxNewPassword.TabIndex = 3;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(12, 115);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 30);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(97, 115);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 30);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // ChangePasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 161);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textBoxNewPassword);
            this.Controls.Add(this.labelNewPassword);
            this.Controls.Add(this.textBoxOldPassword);
            this.Controls.Add(this.labelOldPassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangePasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Смена пароля";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label labelOldPassword;
        private System.Windows.Forms.TextBox textBoxOldPassword;
        private System.Windows.Forms.Label labelNewPassword;
        private System.Windows.Forms.TextBox textBoxNewPassword;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}