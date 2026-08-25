namespace PokemonWinFormsApp.Category
{
    partial class CategoryDetailForm
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
            label2 = new Label();
            textName = new TextBox();
            SuspendLayout();
            // 
            // label2
            // 
            label2.Location = new Point(20, 20);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 1;
            label2.Text = "Name :";
            // 
            // textName
            // 
            textName.Location = new Point(150, 20);
            textName.Name = "textName";
            textName.ReadOnly = true;
            textName.Size = new Size(100, 23);
            textName.TabIndex = 4;
            // 
            // CategoryDetailForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 161);
            Controls.Add(textName);
            Controls.Add(label2);
            Name = "CategoryDetailForm";
            Text = "Category Detail";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private TextBox textName;
    }
}