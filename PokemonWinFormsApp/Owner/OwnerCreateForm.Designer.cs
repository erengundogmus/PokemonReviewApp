namespace PokemonWinFormsApp.Owner
{
    partial class OwnerCreateForm
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
            label3 = new Label();
            textName = new TextBox();
            textGym = new TextBox();
            textCountryId = new TextBox();
            buttonCreate = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(20, 25);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 4;
            label1.Text = "Name :";
            // 
            // label2
            // 
            label2.Location = new Point(20, 75);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 3;
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
            textName.Location = new Point(130, 20);
            textName.Name = "textName";
            textName.Size = new Size(130, 23);
            textName.TabIndex = 2;
            // 
            // textGym
            // 
            textGym.Location = new Point(130, 70);
            textGym.Name = "textGym";
            textGym.Size = new Size(130, 23);
            textGym.TabIndex = 1;
            // 
            // textCountryId
            // 
            textCountryId.Location = new Point(130, 120);
            textCountryId.Name = "textCountryId";
            textCountryId.Size = new Size(130, 23);
            textCountryId.TabIndex = 6;
            // 
            // buttonCreate
            // 
            buttonCreate.Location = new Point(130, 170);
            buttonCreate.Name = "buttonCreate";
            buttonCreate.Size = new Size(130, 30);
            buttonCreate.TabIndex = 0;
            buttonCreate.Text = "Create";
            buttonCreate.UseVisualStyleBackColor = true;
            buttonCreate.Click += buttonCreate_Click;
            // 
            // OwnerCreateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 221);
            Controls.Add(buttonCreate);
            Controls.Add(textCountryId);
            Controls.Add(textGym);
            Controls.Add(textName);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "OwnerCreateForm";
            Text = "Create New Owner";
            Load += OwnerCreateForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.TextBox textGym;
        private System.Windows.Forms.TextBox textCountryId;
        private System.Windows.Forms.Button buttonCreate;
    }
}