namespace PokemonWinFormsApp.Auth
{
    partial class ResetPasswordForm
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
            this.textUsername = new System.Windows.Forms.TextBox();
            this.textNewPassword = new System.Windows.Forms.TextBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // textUsername
            this.textUsername.Location = new System.Drawing.Point(30, 50);
            this.textUsername.Name = "textUsername";
            this.textUsername.Size = new System.Drawing.Size(220, 23);
            this.textUsername.TabIndex = 0;

            // textNewPassword
            this.textNewPassword.Location = new System.Drawing.Point(30, 110);
            this.textNewPassword.Name = "textNewPassword";
            this.textNewPassword.PasswordChar = '*';
            this.textNewPassword.Size = new System.Drawing.Size(220, 23);
            this.textNewPassword.TabIndex = 1;

            // buttonReset
            this.buttonReset.Location = new System.Drawing.Point(30, 155);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(220, 30);
            this.buttonReset.TabIndex = 2;
            this.buttonReset.Text = "Reset Password";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Username";

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "New Password";

            // ResetPasswordForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 221);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.textNewPassword);
            this.Controls.Add(this.textUsername);
            this.Name = "ResetPasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reset Password";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox textUsername;
        private System.Windows.Forms.TextBox textNewPassword;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}