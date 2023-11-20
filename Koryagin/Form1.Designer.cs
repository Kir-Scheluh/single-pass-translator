
namespace Koryagin
{
    partial class Form1
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

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
            this.output = new System.Windows.Forms.TextBox();
            this.input = new System.Windows.Forms.RichTextBox();
            this.linesCounter = new System.Windows.Forms.RichTextBox();
            this.bnfText = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // output
            // 
            this.output.BackColor = System.Drawing.SystemColors.Menu;
            this.output.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.output.Cursor = System.Windows.Forms.Cursors.No;
            this.output.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.output.Location = new System.Drawing.Point(12, 451);
            this.output.Multiline = true;
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.Size = new System.Drawing.Size(1339, 150);
            this.output.TabIndex = 1;
            // 
            // input
            // 
            this.input.AcceptsTab = true;
            this.input.AutoWordSelection = true;
            this.input.BackColor = System.Drawing.SystemColors.Menu;
            this.input.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.input.DetectUrls = false;
            this.input.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.input.Location = new System.Drawing.Point(65, 15);
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(740, 350);
            this.input.TabIndex = 2;
            this.input.Text = "";
            this.input.TextChanged += new System.EventHandler(this.InputTextChanged);
            // 
            // linesCounter
            // 
            this.linesCounter.BackColor = System.Drawing.SystemColors.Menu;
            this.linesCounter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.linesCounter.Cursor = System.Windows.Forms.Cursors.No;
            this.linesCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.linesCounter.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.linesCounter.Location = new System.Drawing.Point(12, 15);
            this.linesCounter.Name = "linesCounter";
            this.linesCounter.ReadOnly = true;
            this.linesCounter.Size = new System.Drawing.Size(47, 350);
            this.linesCounter.TabIndex = 3;
            this.linesCounter.Text = "";
            this.linesCounter.WordWrap = false;
            // 
            // bnfText
            // 
            this.bnfText.BackColor = System.Drawing.SystemColors.MenuBar;
            this.bnfText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bnfText.Cursor = System.Windows.Forms.Cursors.No;
            this.bnfText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bnfText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bnfText.Location = new System.Drawing.Point(811, 15);
            this.bnfText.Multiline = true;
            this.bnfText.Name = "bnfText";
            this.bnfText.ReadOnly = true;
            this.bnfText.Size = new System.Drawing.Size(540, 350);
            this.bnfText.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(729, 383);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(178, 48);
            this.button1.TabIndex = 5;
            this.button1.Text = "Выполнить";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.ButtonClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1362, 612);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bnfText);
            this.Controls.Add(this.linesCounter);
            this.Controls.Add(this.input);
            this.Controls.Add(this.output);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Щелухин Кирилл БСБО-01-20 Вариант 27";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox output;
		private System.Windows.Forms.RichTextBox input;
		private System.Windows.Forms.RichTextBox linesCounter;
		private System.Windows.Forms.TextBox bnfText;
		private System.Windows.Forms.Button button1;
	}
}
