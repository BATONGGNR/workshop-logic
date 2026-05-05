using System;
using System.Windows.Forms;
using WorkshopLogic;

namespace WorkshopWinForms
{
    public partial class ChangePasswordForm : Form
    {
        private User _user;

        public ChangePasswordForm(User user)
        {
            InitializeComponent();
            _user = user;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                _user.ChangePassword(textBoxOldPassword.Text, textBoxNewPassword.Text);
                MessageBox.Show("Пароль успешно изменён!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}