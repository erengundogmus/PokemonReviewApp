namespace PokemonWinFormsApp.Owner
{
    partial class OwnerUpdateForm
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textName = new TextBox();
            textGym = new TextBox();
            textCountryId = new TextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(20, 25);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 0;
            label1.Text = "Name :";
            // 
            // label2
            // 
            label2.Location = new Point(20, 75);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 1;
            label2.Text = "Gym :";
            // 
            // label3
            // 
            label3.Location = new Point(20, 125);
            label3.Name = "label3";
            label3.Size = new Size(100, 23);
            label3.TabIndex = 5;
            label3.Text = "Country Id :";
            // 
            // textName
            // 
            textName.Location = new Point(130, 22);
            textName.Name = "textName";
            textName.Size = new Size(130, 23);
            textName.TabIndex = 2;
            // 
            // textGym
            // 
            textGym.Location = new Point(130, 72);
            textGym.Name = "textGym";
            textGym.Size = new Size(130, 23);
            textGym.TabIndex = 3;
            // 
            // textCountryId
            // 
            textCountryId.Location = new Point(130, 122);
            textCountryId.Name = "textCountryId";
            textCountryId.Size = new Size(130, 23);
            textCountryId.TabIndex = 6;
            // 
            // button1
            // 
            button1.Location = new Point(130, 170);
            button1.Name = "button1";
            button1.Size = new Size(130, 30);
            button1.TabIndex = 4;
            button1.Text = "Update";
            button1.UseVisualStyleBackColor = true;
            button1.Click += buttonUpdate_Click;
            // 
            // OwnerUpdateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 221);
            Controls.Add(button1);
            Controls.Add(textCountryId);
            Controls.Add(textGym);
            Controls.Add(textName);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "OwnerUpdateForm";
            Text = "OwnerUpdateForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textName;
        private TextBox textGym;
        private TextBox textCountryId;
        private Button button1;
    }
}