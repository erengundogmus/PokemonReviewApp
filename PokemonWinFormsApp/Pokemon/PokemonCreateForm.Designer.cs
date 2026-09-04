namespace PokemonWinFormsApp.Pokemon
{
    partial class PokemonCreateForm
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
            textName = new TextBox();
            textBirthDate = new TextBox();
            textOwnerId = new TextBox();
            textCategoryId = new TextBox();
            button1 = new Button();
            pictureBoxPhoto = new PictureBox();
            btnSelectPhoto = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).BeginInit();
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
            label2.Location = new Point(20, 65);
            label2.Name = "label2";
            label2.Size = new Size(100, 40);
            label2.TabIndex = 1;
            label2.Text = "Birth Date :\r\n(dd-MM-yyyy)";
            // 
            // label3
            // 
            label3.Location = new Point(20, 105);
            label3.Name = "label3";
            label3.Size = new Size(100, 23);
            label3.TabIndex = 2;
            label3.Text = "Owner Id :";
            // 
            // label4
            // 
            label4.Location = new Point(20, 145);
            label4.Name = "label4";
            label4.Size = new Size(100, 23);
            label4.TabIndex = 3;
            label4.Text = "Category Id :";
            // 
            // textName
            // 
            textName.Location = new Point(130, 22);
            textName.Name = "textName";
            textName.Size = new Size(130, 23);
            textName.TabIndex = 4;
            // 
            // textBirthDate
            // 
            textBirthDate.Location = new Point(130, 62);
            textBirthDate.Name = "textBirthDate";
            textBirthDate.Size = new Size(130, 23);
            textBirthDate.TabIndex = 5;
            // 
            // textOwnerId
            // 
            textOwnerId.Location = new Point(130, 102);
            textOwnerId.Name = "textOwnerId";
            textOwnerId.Size = new Size(130, 23);
            textOwnerId.TabIndex = 6;
            // 
            // textCategoryId
            // 
            textCategoryId.Location = new Point(130, 142);
            textCategoryId.Name = "textCategoryId";
            textCategoryId.Size = new Size(130, 23);
            textCategoryId.TabIndex = 7;
            // 
            // button1
            // 
            button1.Location = new Point(130, 190);
            button1.Name = "button1";
            button1.Size = new Size(130, 30);
            button1.TabIndex = 8;
            button1.Text = "Create";
            button1.UseVisualStyleBackColor = true;
            button1.Click += buttonCreate_Click;
            // 
            // pictureBoxPhoto
            // 
            pictureBoxPhoto.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxPhoto.Location = new Point(290, 22);
            pictureBoxPhoto.Name = "pictureBoxPhoto";
            pictureBoxPhoto.Size = new Size(150, 150);
            pictureBoxPhoto.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxPhoto.TabIndex = 9;
            pictureBoxPhoto.TabStop = false;
            // 
            // btnSelectPhoto
            // 
            btnSelectPhoto.Location = new Point(290, 190);
            btnSelectPhoto.Name = "btnSelectPhoto";
            btnSelectPhoto.Size = new Size(150, 30);
            btnSelectPhoto.TabIndex = 10;
            btnSelectPhoto.Text = "Select Photo";
            btnSelectPhoto.UseVisualStyleBackColor = true;
            btnSelectPhoto.Click += btnSelectPhoto_Click;
            // 
            // PokemonCreateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(470, 241);
            Controls.Add(btnSelectPhoto);
            Controls.Add(pictureBoxPhoto);
            Controls.Add(button1);
            Controls.Add(textCategoryId);
            Controls.Add(textOwnerId);
            Controls.Add(textBirthDate);
            Controls.Add(textName);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "PokemonCreateForm";
            Text = "Create New Pokemon";
            ((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textName;
        private TextBox textBirthDate;
        private TextBox textOwnerId;
        private TextBox textCategoryId;
        private Button button1;
        private PictureBox pictureBoxPhoto;
        private Button btnSelectPhoto;
    }
}