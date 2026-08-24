namespace PokemonWinFormsApp.Food
{
    partial class FoodCreateForm
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
            textName = new TextBox();
            textHp = new TextBox();
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
            label2.Text = "Hp :";
            // 
            // textName
            // 
            textName.Location = new Point(130, 22);
            textName.Name = "textName";
            textName.Size = new Size(130, 23);
            textName.TabIndex = 2;
            // 
            // textHp
            // 
            textHp.Location = new Point(130, 72);
            textHp.Name = "textHp";
            textHp.Size = new Size(130, 23);
            textHp.TabIndex = 1;
            // 
            // buttonCreate
            // 
            buttonCreate.Location = new Point(130, 120);
            buttonCreate.Name = "buttonCreate";
            buttonCreate.Size = new Size(130, 30);
            buttonCreate.TabIndex = 0;
            buttonCreate.Text = "Create";
            buttonCreate.UseVisualStyleBackColor = true;
            buttonCreate.Click += buttonCreate_Click;
            // 
            // FoodCreateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 161);
            Controls.Add(buttonCreate);
            Controls.Add(textHp);
            Controls.Add(textName);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FoodCreateForm";
            Text = "Create New Food";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.TextBox textHp;
        private System.Windows.Forms.Button buttonCreate;
    }
}