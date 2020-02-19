using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Converter
{
    public partial class Interface : Form
    {
        public Interface()
        {
            InitializeComponent();

            this.label2.Text += trackBarOriginal.Value.ToString();
            this.label3.Text += trackBarResult.Value.ToString();
            EnableNumberButtons();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void historyMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
        }


        private void Interface_Load(object sender, EventArgs e)
        {
        }

        private string AppendNumberToLabel(string str, int number)
        {
            str = str.Remove(str.Length - (str[str.Length - 2] == '1' ? 2 : 1));
            return str + number.ToString();
        }

        private void originalNumberSystem_Changed(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text = "";
            EnableNumberButtons();
            this.label2.Text = AppendNumberToLabel(this.label2.Text, (sender as TrackBar).Value);
        }

        private void resultlNumberSystem_Changed(object sender, EventArgs e)
        {
            this.label3.Text = AppendNumberToLabel(this.label3.Text, (sender as TrackBar).Value);
        }

        private void EnableNumberButtons()
        {
            int numberSystemValue = this.trackBarOriginal.Value;

            this.twoBtn.Enabled = numberSystemValue > 2;
            this.threeBtn.Enabled = numberSystemValue > 3;
            this.fourBtn.Enabled = numberSystemValue > 4;
            this.fiveBtn.Enabled = numberSystemValue > 5;
            this.sixBtn.Enabled = numberSystemValue > 6;
            this.sevenBtn.Enabled = numberSystemValue > 7;
            this.eightBtn.Enabled = numberSystemValue > 8;
            this.nineBtn.Enabled = numberSystemValue > 9;

            this.ABtn.Enabled = numberSystemValue > 10;
            this.BBtn.Enabled = numberSystemValue > 11;
            this.CBtn.Enabled = numberSystemValue > 12;
            this.DBtn.Enabled = numberSystemValue > 13;
            this.EBtn.Enabled = numberSystemValue > 14;
            this.FBtn.Enabled = numberSystemValue > 15;
        }

        private void textBoxOriginal_TextChanged(object sender, EventArgs e)
        {
            (sender as TextBox).Focus();
            (sender as TextBox).SelectionStart = (sender as TextBox).Text.Length;

            this.convertBtn.Enabled = (sender as TextBox).Text != "";
            this.eraseBtn.Enabled = this.convertBtn.Enabled;
            this.pointBtn.Enabled = !(sender as TextBox).Text.Contains('.');
        }

        private void charBtn_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(this.textBoxOriginal.Text, @"^0{1}$") && (sender as Button).Text != ".")
                return;
            if (this.textBoxOriginal.Text == "" && (sender as Button).Text == ".")
                this.textBoxOriginal.Text += '0';

            this.textBoxOriginal.Text += (sender as Button).Text;
        }

        private void eraseBtn_Click(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text = this.textBoxOriginal.Text.Remove(this.textBoxOriginal.Text.Length - 1);
        }

        private void textBoxOriginal_KeyPress(object sender, KeyPressEventArgs e)
        {
            int numberSystemValue = this.trackBarOriginal.Value;

            // if backspace is pressed
            if (e.KeyChar == 8)
            {
                return;
            }

            if (e.KeyChar == '.')
            {
                // if string is empty and the pressed button is '.' 
                // then add '0' to the string (string will be equal to "0.")
                if ((sender as TextBox).Text == "")    // Regex.IsMatch(this.textBoxOriginal.Text, @"^\s*$")
                    (sender as TextBox).Text += '0';

                // string shouldn't contain more than one point
                else if ((sender as TextBox).Text.Contains('.'))   // Regex.IsMatch(this.textBoxOriginal.Text, @"\.")
                    e.KeyChar = '\0';

                return;
            }

            // not allow to write incorrect number (for example 001011 or 000.1)
            if (Regex.IsMatch((sender as TextBox).Text, @"^0{1}$") && e.KeyChar != '.')
            {
                e.KeyChar = '\0';
                return;
            }

            // 'a'-'f' to 'A'-'F' 
            if (Regex.IsMatch(e.KeyChar.ToString(), @"[a-f]"))
            {
                e.KeyChar = Convert.ToChar(e.KeyChar - 32);
            }

            // allow only '0'-'9' and 'A'-'F' symbols
            else if (Regex.IsMatch(e.KeyChar.ToString(), @"[^0-9A-F]"))
            {
                e.KeyChar = '\0';
                return;
            }

            // check correspondence of the entered symbol with original number system
            // '0' = 48, '9' = 57, 'A' = 65, 'F' = 70
            if (numberSystemValue <= 10 && e.KeyChar > 47 + numberSystemValue ||
                numberSystemValue > 10 && e.KeyChar > 54 + numberSystemValue)
            {
                e.KeyChar = '\0';
                return;
            }

        }
    }
}