namespace PokemonWinFormsApp.Review
{
    partial class ReviewDetailForm
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
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            textId = new TextBox();
            textTitle = new TextBox();
            textText = new TextBox();
            textRating = new TextBox();
            textPokemonId = new TextBox();
            textPokemonName = new TextBox();
            textReviewerId = new TextBox();
            textReviewerFirstName = new TextBox();
            textReviewerLastName = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(20, 20);
            label1.Name = "label1";
            label1.Size = new Size(130, 23);
            label1.TabIndex = 0;
            label1.Text = "Id :";
            // 
            // label2
            // 
            label2.Location = new Point(20, 60);
            label2.Name = "label2";
            label2.Size = new Size(130, 23);
            label2.TabIndex = 1;
            label2.Text = "Title :";
            // 
            // label3
            // 
            label3.Location = new Point(20, 100);
            label3.Name = "label3";
            label3.Size = new Size(130, 23);
            label3.TabIndex = 2;
            label3.Text = "Text :";
            // 
            // label4
            // 
            label4.Location = new Point(20, 140);
            label4.Name = "label4";
            label4.Size = new Size(130, 23);
            label4.TabIndex = 3;
            label4.Text = "Rating :";
            // 
            // label5
            // 
            label5.Location = new Point(20, 180);
            label5.Name = "label5";
            label5.Size = new Size(130, 23);
            label5.TabIndex = 4;
            label5.Text = "Pokemon Id :";
            // 
            // label6
            // 
            label6.Location = new Point(20, 220);
            label6.Name = "label6";
            label6.Size = new Size(130, 23);
            label6.TabIndex = 5;
            label6.Text = "Pokemon Name :";
            // 
            // label7
            // 
            label7.Location = new Point(20, 260);
            label7.Name = "label7";
            label7.Size = new Size(130, 23);
            label7.TabIndex = 6;
            label7.Text = "Reviewer Id :";
            // 
            // label8
            // 
            label8.Location = new Point(20, 300);
            label8.Name = "label8";
            label8.Size = new Size(130, 23);
            label8.TabIndex = 7;
            label8.Text = "Reviewer First Name :";
            // 
            // label9
            // 
            label9.Location = new Point(20, 340);
            label9.Name = "label9";
            label9.Size = new Size(130, 23);
            label9.TabIndex = 8;
            label9.Text = "Reviewer Last Name :";
            // 
            // textId
            // 
            textId.Location = new Point(160, 17);
            textId.Name = "textId";
            textId.ReadOnly = true;
            textId.Size = new Size(150, 23);
            textId.TabIndex = 9;
            // 
            // textTitle
            // 
            textTitle.Location = new Point(160, 57);
            textTitle.Name = "textTitle";
            textTitle.ReadOnly = true;
            textTitle.Size = new Size(150, 23);
            textTitle.TabIndex = 10;
            // 
            // textText
            // 
            textText.Location = new Point(160, 97);
            textText.Name = "textText";
            textText.ReadOnly = true;
            textText.Size = new Size(150, 23);
            textText.TabIndex = 11;
            // 
            // textRating
            // 
            textRating.Location = new Point(160, 137);
            textRating.Name = "textRating";
            textRating.ReadOnly = true;
            textRating.Size = new Size(150, 23);
            textRating.TabIndex = 12;
            // 
            // textPokemonId
            // 
            textPokemonId.Location = new Point(160, 177);
            textPokemonId.Name = "textPokemonId";
            textPokemonId.ReadOnly = true;
            textPokemonId.Size = new Size(150, 23);
            textPokemonId.TabIndex = 13;
            // 
            // textPokemonName
            // 
            textPokemonName.Location = new Point(160, 217);
            textPokemonName.Name = "textPokemonName";
            textPokemonName.ReadOnly = true;
            textPokemonName.Size = new Size(150, 23);
            textPokemonName.TabIndex = 14;
            // 
            // textReviewerId
            // 
            textReviewerId.Location = new Point(160, 257);
            textReviewerId.Name = "textReviewerId";
            textReviewerId.ReadOnly = true;
            textReviewerId.Size = new Size(150, 23);
            textReviewerId.TabIndex = 15;
            // 
            // textReviewerFirstName
            // 
            textReviewerFirstName.Location = new Point(160, 297);
            textReviewerFirstName.Name = "textReviewerFirstName";
            textReviewerFirstName.ReadOnly = true;
            textReviewerFirstName.Size = new Size(150, 23);
            textReviewerFirstName.TabIndex = 16;
            // 
            // textReviewerLastName
            // 
            textReviewerLastName.Location = new Point(160, 337);
            textReviewerLastName.Name = "textReviewerLastName";
            textReviewerLastName.ReadOnly = true;
            textReviewerLastName.Size = new Size(150, 23);
            textReviewerLastName.TabIndex = 17;
            // 
            // ReviewDetailForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 391);
            Controls.Add(textReviewerLastName);
            Controls.Add(textReviewerFirstName);
            Controls.Add(textReviewerId);
            Controls.Add(textPokemonName);
            Controls.Add(textPokemonId);
            Controls.Add(textRating);
            Controls.Add(textText);
            Controls.Add(textTitle);
            Controls.Add(textId);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ReviewDetailForm";
            Text = "Review Detail";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox textId;
        private TextBox textTitle;
        private TextBox textText;
        private TextBox textRating;
        private TextBox textPokemonId;
        private TextBox textPokemonName;
        private TextBox textReviewerId;
        private TextBox textReviewerFirstName;
        private TextBox textReviewerLastName;
    }
}