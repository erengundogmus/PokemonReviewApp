namespace PokemonWinFormsApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonPokemon = new Button();
            button2 = new Button();
            buttonFood = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            SuspendLayout();
            // 
            // buttonPokemon
            // 
            buttonPokemon.Location = new Point(10, 100);
            buttonPokemon.Name = "buttonPokemon";
            buttonPokemon.Size = new Size(160, 80);
            buttonPokemon.TabIndex = 0;
            buttonPokemon.Text = "Pokemon";
            buttonPokemon.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(210, 100);
            button2.Name = "button2";
            button2.Size = new Size(160, 80);
            button2.TabIndex = 1;
            button2.Text = "Category";
            button2.UseVisualStyleBackColor = true;
            // 
            // buttonFood
            // 
            buttonFood.Location = new Point(410, 100);
            buttonFood.Name = "buttonFood";
            buttonFood.Size = new Size(160, 80);
            buttonFood.TabIndex = 2;
            buttonFood.Text = "Food";
            buttonFood.UseVisualStyleBackColor = true;
            buttonFood.Click += buttonFood_Click;
            // 
            // button4
            // 
            button4.Location = new Point(610, 100);
            button4.Name = "button4";
            button4.Size = new Size(160, 80);
            button4.TabIndex = 3;
            button4.Text = "Pokemon Food";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(10, 250);
            button5.Name = "button5";
            button5.Size = new Size(160, 80);
            button5.TabIndex = 4;
            button5.Text = "Owner";
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(210, 250);
            button6.Name = "button6";
            button6.Size = new Size(160, 80);
            button6.TabIndex = 5;
            button6.Text = "Country";
            button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Location = new Point(410, 250);
            button7.Name = "button7";
            button7.Size = new Size(160, 80);
            button7.TabIndex = 6;
            button7.Text = "Review";
            button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            button8.Location = new Point(610, 250);
            button8.Name = "button8";
            button8.Size = new Size(160, 80);
            button8.TabIndex = 7;
            button8.Text = "Reviewer";
            button8.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 461);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(buttonFood);
            Controls.Add(button2);
            Controls.Add(buttonPokemon);
            Name = "MainForm";
            Text = "Main Menu";
            ResumeLayout(false);
        }

        #endregion

        private Button buttonPokemon;
        private Button button2;
        private Button buttonFood;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
    }
}
