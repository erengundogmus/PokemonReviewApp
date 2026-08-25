namespace PokemonWinFormsApp.PokemonFood
{
    partial class PokemonFoodForm
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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            dataGridView2 = new DataGridView();
            dataGridView3 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(40, 40);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(700, 130);
            dataGridView1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(40, 480);
            button1.Name = "button1";
            button1.Size = new Size(120, 60);
            button1.TabIndex = 1;
            button1.Text = "List";
            button1.UseVisualStyleBackColor = true;
            button1.Click += buttonList_Click;
            // 
            // button2
            // 
            button2.Location = new Point(230, 480);
            button2.Name = "button2";
            button2.Size = new Size(120, 60);
            button2.TabIndex = 2;
            button2.Text = "Pokemon's Menu";
            button2.UseVisualStyleBackColor = true;
            button2.Click += buttonPokemonsMenu_Click;
            // 
            // button3
            // 
            button3.Location = new Point(430, 480);
            button3.Name = "button3";
            button3.Size = new Size(120, 60);
            button3.TabIndex = 3;
            button3.Text = "Add To Menu";
            button3.UseVisualStyleBackColor = true;
            button3.Click += buttonAddToMenu_Click;
            // 
            // button4
            // 
            button4.Location = new Point(620, 480);
            button4.Name = "button4";
            button4.Size = new Size(120, 60);
            button4.TabIndex = 4;
            button4.Text = "Remove From Menu";
            button4.UseVisualStyleBackColor = true;
            button4.Click += buttonRemoveFromMenu_Click;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(40, 190);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(700, 130);
            dataGridView2.TabIndex = 5;
            // 
            // dataGridView3
            // 
            dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView3.Location = new Point(40, 340);
            dataGridView3.Name = "dataGridView3";
            dataGridView3.Size = new Size(700, 130);
            dataGridView3.TabIndex = 6;
            // 
            // PokemonFoodForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(dataGridView3);
            Controls.Add(dataGridView2);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Name = "PokemonFoodForm";
            Text = "PokemonFoodForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private DataGridView dataGridView2;
        private DataGridView dataGridView3;
    }
}