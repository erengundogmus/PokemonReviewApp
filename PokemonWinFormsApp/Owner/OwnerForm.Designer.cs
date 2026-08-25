using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace PokemonWinFormsApp
{
    partial class OwnerForm
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
            dataGridView1 = new DataGridView();
            buttonList = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(40, 40);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(700, 200);
            dataGridView1.TabIndex = 0;
            // 
            // buttonList
            // 
            buttonList.Location = new Point(40, 300);
            buttonList.Name = "buttonList";
            buttonList.Size = new Size(120, 60);
            buttonList.TabIndex = 1;
            buttonList.Text = "List";
            buttonList.UseVisualStyleBackColor = true;
            buttonList.Click += buttonList_Click;
            // 
            // button2
            // 
            button2.Location = new Point(185, 300);
            button2.Name = "button2";
            button2.Size = new Size(120, 60);
            button2.TabIndex = 2;
            button2.Text = "Detail";
            button2.UseVisualStyleBackColor = true;
            button2.Click += buttonDetail_Click;
            // 
            // button3
            // 
            button3.Location = new Point(330, 300);
            button3.Name = "button3";
            button3.Size = new Size(120, 60);
            button3.TabIndex = 3;
            button3.Text = "Create";
            button3.UseVisualStyleBackColor = true;
            button3.Click += buttonCreate_Click;
            // 
            // button4
            // 
            button4.Location = new Point(475, 300);
            button4.Name = "button4";
            button4.Size = new Size(120, 60);
            button4.TabIndex = 4;
            button4.Text = "Update";
            button4.UseVisualStyleBackColor = true;
            button4.Click += buttonUpdate_Click;
            // 
            // button5
            // 
            button5.Location = new Point(620, 300);
            button5.Name = "button5";
            button5.Size = new Size(120, 60);
            button5.TabIndex = 5;
            button5.Text = "Delete";
            button5.UseVisualStyleBackColor = true;
            button5.Click += buttonDelete_Click;
            // 
            // OwnerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 461);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(buttonList);
            Controls.Add(dataGridView1);
            Name = "OwnerForm";
            Text = "Owner";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button buttonList;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
    }
}