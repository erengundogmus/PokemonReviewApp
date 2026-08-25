namespace PokemonWinFormsApp.Reviewer
{
    partial class ReviewerCreateForm
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
            label1 = new Label();
            label2 = new Label();
            textFirstName = new TextBox();
            textLastName = new TextBox();
            buttonCreate = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(20, 25);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 2;
            label1.Text = "First Name :";
            // 
            // label2
            // 
            label2.Location = new Point(20, 75);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 3;
            label2.Text = "Last Name :";
            // 
            // textFirstName
            // 
            textFirstName.Location = new Point(130, 20);
            textFirstName.Name = "textFirstName";
            textFirstName.Size = new Size(130, 23);
            textFirstName.TabIndex = 0;
            // 
            // textLastName
            // 
            textLastName.Location = new Point(130, 70);
            textLastName.Name = "textLastName";
            textLastName.Size = new Size(130, 23);
            textLastName.TabIndex = 1;
            // 
            // buttonCreate
            // 
            buttonCreate.Location = new Point(130, 120);
            buttonCreate.Name = "buttonCreate";
            buttonCreate.Size = new Size(130, 30);
            buttonCreate.TabIndex = 4;
            buttonCreate.Text = "Create";
            buttonCreate.UseVisualStyleBackColor = true;
            buttonCreate.Click += buttonCreate_Click;
            // 
            // ReviewerCreateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 161);
            Controls.Add(buttonCreate);
            Controls.Add(textLastName);
            Controls.Add(textFirstName);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ReviewerCreateForm";
            Text = "Create New Reviewer";
            Load += ReviewerCreateForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textFirstName;
        private System.Windows.Forms.TextBox textLastName;
        private System.Windows.Forms.Button buttonCreate;
    }
}