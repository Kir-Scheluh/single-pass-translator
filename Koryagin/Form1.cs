using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Koryagin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            bnfText.Text = File.ReadAllText("D:\\Учеба\\Универ\\Корягин\\Программа\\winforms\\Koryagin\\lang.txt");
        }

        private void InputTextChanged(object sender, EventArgs e)
        {
            input.DeselectAll();
            linesCounter.Text = string.Empty;
            for (int i = 1; i <= input.Lines.Length; i++)
                linesCounter.Text += $"{i}.\n";
        }
        private void ButtonClicked(object sender, EventArgs e)
        {
            OrganizeIO();
            try
            {
                if (input.Text != string.Empty)
                    output.Text = new Parser(input).Parse();
                else throw new MyException(0, 0, "Передан пустой текст");
            }
            catch (MyException exc)
            {
                output.Text = exc.Message;
                input.Select(exc.Index, exc.Length);
                input.SelectionBackColor = Color.FromArgb(255, 36, 0);
            }
        }
        private void OrganizeIO()
        {
            bool rightPart = false;
            output.Text = string.Empty;
            input.Text = input.Text.Trim();
            var symbols = ":=+*/^,\t()";
            foreach (var symbol in symbols)
                input.Text = input.Text.Replace(symbol.ToString(), $" {symbol} ");            

            input.Text = RemoveMultipleSpaces(input.Text);
        }
        private string RemoveMultipleSpaces(string input)
        {
            return Regex.Replace(input, "[ ]+", " ");
        }



    }
}
