﻿using System;
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
    public partial class Converter : Form
    {
        public Converter()
        {
            InitializeComponent();

            this.label2.Text += trackBarOriginal.Value.ToString();
            this.label3.Text += trackBarResult.Value.ToString();
            EnableNumberButtons();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void historyMenuItem_Click(object sender, EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            //throw new System.NotImplementedException();
        }


        private void Converter_Load(object sender, EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private string AppendNumberSystemToLabel(string str)
        {
            str = str.Remove(str.Length - (str[str.Length - 2] == '1' ? 2 : 1));
            return str + this.trackBarOriginal.Value.ToString();
        }

        private void originalNumberSystem_Changed(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text = "";
            EnableNumberButtons();
            this.label2.Text = AppendNumberSystemToLabel(this.label2.Text);
        }

        private void resultlNumberSystem_Changed(object sender, EventArgs e)
        {
            this.label3.Text = AppendNumberSystemToLabel(this.label3.Text);
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
            this.convertBtn.Enabled = this.textBoxOriginal.Text != "";
            this.eraseBtn.Enabled = this.convertBtn.Enabled;
            this.pointBtn.Enabled = this.textBoxOriginal.Text != "" && this.textBoxOriginal.Text.IndexOf('.') == -1;
            this.textBoxOriginal.Focus();
            this.textBoxOriginal.SelectionStart = this.textBoxOriginal.Text.Length;
        }

        private void zeroBtn_Click(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text += '0';
        }

        private void oneBtn_Click(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text += '1';
        }

        private void twoBtn_Click(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text += '2';
        }

        private void threeBtn_Click(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text += '3';
        }

        private void fourBtn_Click(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text += '4';
        }

        private void fiveBtn_Click(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text += '5';
        }

        private void sixBtn_Click(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text += '6';
        }

        private void sevenBtn_Click(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text += '7';
        }

        private void eightBtn_Click(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text += '8';
        }

        private void nineBtn_Click(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text += '9';
        }

        private void ABtn_Click(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text += 'A';
        }

        private void BBtn_Click(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text += 'B';
        }

        private void CBtn_Click(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text += 'C';
        }

        private void DBtn_Click(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text += 'D';
        }

        private void EBtn_Click(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text += 'E';
        }

        private void FBtn_Click(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text += 'F';
        }

        private void pointBtn_Click(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text += '.';
        }

        private void eraseBtn_Click(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text = this.textBoxOriginal.Text.Remove(this.textBoxOriginal.Text.Length - 1);
        }

        private void CheckEnteredSymbols(object sender, KeyPressEventArgs e)
        {
            int numberSystemValue = this.trackBarOriginal.Value;

            // if backspace is pressed
            if (e.KeyChar == 8)
            {
                return;
            }

            if (e.KeyChar == '.')
            {
                // string shouldn't be empty or contain point
                if (Regex.IsMatch(this.textBoxOriginal.Text, @"^\s*$|\."))
                    e.KeyChar = '\0';
                return;
            }

            // 'a'-'f' to 'A'-'F' 
            if (Regex.IsMatch(e.KeyChar.ToString(), @"[a-f]") )
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