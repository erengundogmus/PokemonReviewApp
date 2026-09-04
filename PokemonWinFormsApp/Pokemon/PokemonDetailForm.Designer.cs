namespace PokemonWinFormsApp.Pokemon
{
    partial class PokemonDetailForm
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
            textId = new TextBox();
            textName = new TextBox();
            textBirthDate = new TextBox();
            textOwnerId = new TextBox();
            textOwnerName = new TextBox();
            textCategoryId = new TextBox();
            textCategoryName = new TextBox();
            pictureBoxPhoto = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).BeginInit();
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
            label2.Text = "Name :";
            // 
            // label3
            // 
            label3.Location = new Point(20, 100);
            label3.Name = "label3";
            label3.Size = new Size(130, 23);
            label3.TabIndex = 2;
            label3.Text = "Birth Date :";
            // 
            // label4
            // 
            label4.Location = new Point(20, 140);
            label4.Name = "label4";
            label4.Size = new Size(130, 23);
            label4.TabIndex = 3;
            label4.Text = "Owner Id :";
            // 
            // label5
            // 
            label5.Location = new Point(20, 180);
            label5.Name = "label5";
            label5.Size = new Size(130, 23);
            label5.TabIndex = 4;
            label5.Text = "Owner Name :";
            // 
            // label6
            // 
            label6.Location = new Point(20, 220);
            label6.Name = "label6";
            label6.Size = new Size(130, 23);
            label6.TabIndex = 5;
            label6.Text = "Category Id :";
            // 
            // label7
            // 
            label7.Location = new Point(20, 260);
            label7.Name = "label7";
            label7.Size = new Size(130, 23);
            label7.TabIndex = 6;
            label7.Text = "Category Name :";
            // 
            // textId
            // 
            textId.Location = new Point(160, 17);
            textId.Name = "textId";
            textId.ReadOnly = true;
            textId.Size = new Size(150, 23);
            textId.TabIndex = 7;
            // 
            // textName
            // 
            textName.Location = new Point(160, 57);
            textName.Name = "textName";
            textName.ReadOnly = true;
            textName.Size = new Size(150, 23);
            textName.TabIndex = 8;
            // 
            // textBirthDate
            // 
            textBirthDate.Location = new Point(160, 97);
            textBirthDate.Name = "textBirthDate";
            textBirthDate.ReadOnly = true;
            textBirthDate.Size = new Size(150, 23);
            textBirthDate.TabIndex = 9;
            // 
            // textOwnerId
            // 
            textOwnerId.Location = new Point(160, 137);
            textOwnerId.Name = "textOwnerId";
            textOwnerId.ReadOnly = true;
            textOwnerId.Size = new Size(150, 23);
            textOwnerId.TabIndex = 10;
            // 
            // textOwnerName
            // 
            textOwnerName.Location = new Point(160, 177);
            textOwnerName.Name = "textOwnerName";
            textOwnerName.ReadOnly = true;
            textOwnerName.Size = new Size(150, 23);
            textOwnerName.TabIndex = 11;
            // 
            // textCategoryId
            // 
            textCategoryId.Location = new Point(160, 217);
            textCategoryId.Name = "textCategoryId";
            textCategoryId.ReadOnly = true;
            textCategoryId.Size = new Size(150, 23);
            textCategoryId.TabIndex = 12;
            // 
            // textCategoryName
            // 
            textCategoryName.Location = new Point(160, 257);
            textCategoryName.Name = "textCategoryName";
            textCategoryName.ReadOnly = true;
            textCategoryName.Size = new Size(150, 23);
            textCategoryName.TabIndex = 13;
            // 
            // pictureBoxPhoto
            // 
            pictureBoxPhoto.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxPhoto.Location = new Point(340, 17);
            pictureBoxPhoto.Name = "pictureBoxPhoto";
            pictureBoxPhoto.Size = new Size(180, 180);
            pictureBoxPhoto.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxPhoto.TabIndex = 14;
            pictureBoxPhoto.TabStop = false;
            // 
            // PokemonDetailForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(550, 311);
            Controls.Add(pictureBoxPhoto);
            Controls.Add(textCategoryName);
            Controls.Add(textCategoryId);
            Controls.Add(textOwnerName);
            Controls.Add(textOwnerId);
            Controls.Add(textBirthDate);
            Controls.Add(textName);
            Controls.Add(textId);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "PokemonDetailForm";
            Text = "Pokemon Detail";
            ((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
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
        private TextBox textId;
        private TextBox textName;
        private TextBox textBirthDate;
        private TextBox textOwnerId;
        private TextBox textOwnerName;
        private TextBox textCategoryId;
        private TextBox textCategoryName;
        private PictureBox pictureBoxPhoto;
    }
}