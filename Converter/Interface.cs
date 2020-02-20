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
            this.KeyDown += Interface_KeyDown;
            this.label2.Text += trackBarOriginal.Value.ToString();
            this.label3.Text += trackBarResult.Value.ToString();
            EnableNumberButtons();
        }

        private void Interface_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back && textBoxOriginal.Text != String.Empty)
                textBoxOriginal.Text = this.textBoxOriginal.Text.Remove(this.textBoxOriginal.Text.Length - 1);
            if (e.KeyValue > 47 && e.KeyValue < 58 && trackBarOriginal.Value > e.KeyValue - 48)
                textBoxOriginal.Text += e.KeyValue - 48;
            if (e.KeyValue > 64 && e.KeyValue < 71 && trackBarOriginal.Value > e.KeyValue - 55)
                textBoxOriginal.Text += e.KeyData;
            if (e.KeyData == Keys.OemPeriod)
                pointBtn_Click(this, null);
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
            this.textBoxResult.Text = "";
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
            //(sender as TextBox).Focus();
            //(sender as TextBox).SelectionStart = (sender as TextBox).Text.Length;

            this.convertBtn.Enabled = (sender as TextBox).Text != "";
            this.eraseBtn.Enabled = this.convertBtn.Enabled;
            this.pointBtn.Enabled = !(sender as TextBox).Text.Contains('.');
        }

        private void charBtn_Click(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text += (sender as Button).Text;
        }

        private void pointBtn_Click(object sender, EventArgs e)
        {
            if (textBoxOriginal.Text == String.Empty)
                textBoxOriginal.Text += "0.";
            else if (!textBoxOriginal.Text.Contains("."))
            {
                textBoxOriginal.Text += ".";
            }
        }

        private void eraseBtn_Click(object sender, EventArgs e)
        {
            this.textBoxOriginal.Text = this.textBoxOriginal.Text.Remove(this.textBoxOriginal.Text.Length - 1);
        }

        private void convertBtn_Click(object sender, EventArgs e)
        {
            var result = Converter.Convert(
                this.textBoxOriginal.Text,
                this.trackBarOriginal.Value,
                this.trackBarResult.Value
            );

            this.textBoxResult.Text = result;
        }
    }
}