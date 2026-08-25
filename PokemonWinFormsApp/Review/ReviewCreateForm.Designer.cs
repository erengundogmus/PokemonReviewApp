namespace PokemonWinFormsApp.Review
{
    partial class ReviewCreateForm
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
            label4 = new Label();
            label5 = new Label();
            textTitle = new TextBox();
            textText = new TextBox();
            textRating = new TextBox();
            textReviewerId = new TextBox();
            textPokemonId = new TextBox();
            buttonCreate = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(20, 25);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 0;
            label1.Text = "Title :";
            // 
            // label2
            // 
            label2.Location = new Point(20, 65);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 1;
            label2.Text = "Text :";
            // 
            // label3
            // 
            label3.Location = new Point(20, 105);
            label3.Name = "label3";
            label3.Size = new Size(100, 23);
            label3.TabIndex = 2;
            label3.Text = "Rating :";
            // 
            // label4
            // 
            label4.Location = new Point(20, 145);
            label4.Name = "label4";
            label4.Size = new Size(100, 23);
            label4.TabIndex = 3;
            label4.Text = "Reviewer Id :";
            // 
            // label5
            // 
            label5.Location = new Point(20, 185);
            label5.Name = "label5";
            label5.Size = new Size(100, 23);
            label5.TabIndex = 4;
            label5.Text = "Pokemon Id :";
            // 
            // textTitle
            // 
            textTitle.Location = new Point(130, 22);
            textTitle.Name = "textTitle";
            textTitle.Size = new Size(130, 23);
            textTitle.TabIndex = 5;
            // 
            // textText
            // 
            textText.Location = new Point(130, 62);
            textText.Name = "textText";
            textText.Size = new Size(130, 23);
            textText.TabIndex = 6;
            // 
            // textRating
            // 
            textRating.Location = new Point(130, 102);
            textRating.Name = "textRating";
            textRating.Size = new Size(130, 23);
            textRating.TabIndex = 7;
            // 
            // textReviewerId
            // 
            textReviewerId.Location = new Point(130, 142);
            textReviewerId.Name = "textReviewerId";
            textReviewerId.Size = new Size(130, 23);
            textReviewerId.TabIndex = 8;
            // 
            // textPokemonId
            // 
            textPokemonId.Location = new Point(130, 182);
            textPokemonId.Name = "textPokemonId";
            textPokemonId.Size = new Size(130, 23);
            textPokemonId.TabIndex = 9;
            // 
            // buttonCreate
            // 
            buttonCreate.Location = new Point(130, 230);
            buttonCreate.Name = "buttonCreate";
            buttonCreate.Size = new Size(130, 30);
            buttonCreate.TabIndex = 10;
            buttonCreate.Text = "Create";
            buttonCreate.UseVisualStyleBackColor = true;
            buttonCreate.Click += buttonCreate_Click;
            // 
            // ReviewCreateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 281);
            Controls.Add(buttonCreate);
            Controls.Add(textPokemonId);
            Controls.Add(textReviewerId);
            Controls.Add(textRating);
            Controls.Add(textText);
            Controls.Add(textTitle);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ReviewCreateForm";
            Text = "Create New Review";
            Load += ReviewCreateForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textTitle;
        private System.Windows.Forms.TextBox textText;
        private System.Windows.Forms.TextBox textRating;
        private System.Windows.Forms.TextBox textReviewerId;
        private System.Windows.Forms.TextBox textPokemonId;
        private System.Windows.Forms.Button buttonCreate;
    }
}