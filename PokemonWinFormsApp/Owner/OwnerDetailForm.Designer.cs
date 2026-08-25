namespace PokemonWinFormsApp.Owner
{
    partial class OwnerDetailForm
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
            textName = new TextBox();
            textGym = new TextBox();
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
            label2.Text = "Name :";
            // 
            // label3
            // 
            label3.Location = new Point(20, 120);
            label3.Name = "label3";
            label3.Size = new Size(100, 23);
            label3.TabIndex = 2;
            label3.Text = "Gym :";
            // 
            // textId
            // 
            textId.Location = new Point(150, 17);
            textId.Name = "textId";
            textId.ReadOnly = true;
            textId.Size = new Size(100, 23);
            textId.TabIndex = 3;
            // 
            // textName
            // 
            textName.Location = new Point(150, 70);
            textName.Name = "textName";
            textName.ReadOnly = true;
            textName.Size = new Size(100, 23);
            textName.TabIndex = 4;
            // 
            // textGym
            // 
            textGym.Location = new Point(150, 120);
            textGym.Name = "textGym";
            textGym.ReadOnly = true;
            textGym.Size = new Size(100, 23);
            textGym.TabIndex = 5;
            // 
            // OwnerDetailForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 161);
            Controls.Add(textGym);
            Controls.Add(textName);
            Controls.Add(textId);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "OwnerDetailForm";
            Text = "Owner Detail";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textId;
        private TextBox textName;
        private TextBox textGym;
    }
}