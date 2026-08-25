namespace PokemonWinFormsApp.Reviewer
{
    partial class ReviewerDetailForm
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
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textId = new TextBox();
            textFirstName = new TextBox();
            textLastName = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(20, 20);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 0;
            label1.Text = "Id :";
            // 
            // label2
            // 
            label2.Location = new Point(20, 70);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 1;
            label2.Text = "First Name :";
            // 
            // label3
            // 
            label3.Location = new Point(20, 120);
            label3.Name = "label3";
            label3.Size = new Size(100, 23);
            label3.TabIndex = 2;
            label3.Text = "Last Name :";
            // 
            // textId
            // 
            textId.Location = new Point(130, 17);
            textId.Name = "textId";
            textId.ReadOnly = true;
            textId.Size = new Size(130, 23);
            textId.TabIndex = 3;
            // 
            // textFirstName
            // 
            textFirstName.Location = new Point(130, 67);
            textFirstName.Name = "textFirstName";
            textFirstName.ReadOnly = true;
            textFirstName.Size = new Size(130, 23);
            textFirstName.TabIndex = 4;
            // 
            // textLastName
            // 
            textLastName.Location = new Point(130, 117);
            textLastName.Name = "textLastName";
            textLastName.ReadOnly = true;
            textLastName.Size = new Size(130, 23);
            textLastName.TabIndex = 5;
            // 
            // ReviewerDetailForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 161);
            Controls.Add(textLastName);
            Controls.Add(textFirstName);
            Controls.Add(textId);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ReviewerDetailForm";
            Text = "Reviewer Detail";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textId;
        private TextBox textFirstName;
        private TextBox textLastName;
    }
}