namespace WorkshopWinForms
{
    partial class LoginForm
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.labelError = new System.Windows.Forms.Label();
            this.labelRole = new System.Windows.Forms.Label();
            this.radioButtonOperator = new System.Windows.Forms.RadioButton();
            this.radioButtonEngineer = new System.Windows.Forms.RadioButton();
            this.radioButtonAdmin = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelTitle.Location = new System.Drawing.Point(12, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(119, 20);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Авторизация";
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(12, 45);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(128, 15);
            this.labelUsername.TabIndex = 1;
            this.labelUsername.Text = "Имя пользователя:";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(12, 61);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(260, 23);
            this.textBoxUsername.TabIndex = 2;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(12, 96);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(49, 15);
            this.labelPassword.TabIndex = 3;
            this.labelPassword.Text = "Пароль:";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(12, 112);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(260, 23);
            this.textBoxPassword.TabIndex = 4;
            // 
            // labelRole
            // 
            this.labelRole.AutoSize = true;
            this.labelRole.Location = new System.Drawing.Point(12, 145);
            this.labelRole.Name = "labelRole";
            this.labelRole.Size = new System.Drawing.Size(80, 15);
            this.labelRole.TabIndex = 5;
            this.labelRole.Text = "Выберите роль:";
            // 
            // radioButtonOperator
            // 
            this.radioButtonOperator.AutoSize = true;
            this.radioButtonOperator.Location = new System.Drawing.Point(12, 163);
            this.radioButtonOperator.Name = "radioButtonOperator";
            this.radioButtonOperator.Size = new System.Drawing.Size(70, 19);
            this.radioButtonOperator.TabIndex = 6;
            this.radioButtonOperator.Text = "Operator";
            this.radioButtonOperator.UseVisualStyleBackColor = true;
            this.radioButtonOperator.Checked = true;
            // 
            // radioButtonEngineer
            // 
            this.radioButtonEngineer.AutoSize = true;
            this.radioButtonEngineer.Location = new System.Drawing.Point(12, 188);
            this.radioButtonEngineer.Name = "radioButtonEngineer";
            this.radioButtonEngineer.Size = new System.Drawing.Size(75, 19);
            this.radioButtonEngineer.TabIndex = 7;
            this.radioButtonEngineer.Text = "Engineer";
            this.radioButtonEngineer.UseVisualStyleBackColor = true;
            // 
            // radioButtonAdmin
            // 
            this.radioButtonAdmin.AutoSize = true;
            this.radioButtonAdmin.Location = new System.Drawing.Point(12, 213);
            this.radioButtonAdmin.Name = "radioButtonAdmin";
            this.radioButtonAdmin.Size = new System.Drawing.Size(60, 19);
            this.radioButtonAdmin.TabIndex = 8;
            this.radioButtonAdmin.Text = "Admin";
            this.radioButtonAdmin.UseVisualStyleBackColor = true;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(12, 245);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(125, 30);
            this.buttonLogin.TabIndex = 9;
            this.buttonLogin.Text = "Войти";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonRegister
            // 
            this.buttonRegister.Location = new System.Drawing.Point(147, 245);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(125, 30);
            this.buttonRegister.TabIndex = 10;
            this.buttonRegister.Text = "Регистрация";
            this.buttonRegister.UseVisualStyleBackColor = true;
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.ForeColor = System.Drawing.Color.Red;
            this.labelError.Location = new System.Drawing.Point(12, 290);
            this.labelError.MaximumSize = new System.Drawing.Size(260, 100);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(0, 15);
            this.labelError.TabIndex = 11;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 320);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.buttonRegister);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.radioButtonAdmin);
            this.Controls.Add(this.radioButtonEngineer);
            this.Controls.Add(this.radioButtonOperator);
            this.Controls.Add(this.labelRole);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.labelUsername);
            this.Controls.Add(this.labelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вход в систему";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.Label labelRole;
        private System.Windows.Forms.RadioButton radioButtonOperator;
        private System.Windows.Forms.RadioButton radioButtonEngineer;
        private System.Windows.Forms.RadioButton radioButtonAdmin;
    }
}