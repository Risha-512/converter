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
            KeyDown += Interface_KeyDown;
            label2.Text += trackBarOriginal.Value.ToString();
            label3.Text += trackBarResult.Value.ToString();
            EnableNumberButtons();
        }

        private void Interface_KeyDown(object sender, KeyEventArgs e)
        {
            var selectionStart = textBoxOriginal.SelectionStart;

            switch (e.KeyData)
            {
                case Keys key when (key >= Keys.D0 && key <= Keys.D9 && trackBarOriginal.Value > e.KeyValue - 48):
                    textBoxOriginal.SelectedText = "";
                    textBoxOriginal.Text = textBoxOriginal.Text.Insert(selectionStart, (e.KeyValue - 48).ToString());
                    textBoxOriginal.SelectionStart = selectionStart + 1;
                    break;

                case Keys key when (key >= Keys.A && key <= Keys.F && trackBarOriginal.Value > e.KeyValue - 55):
                    textBoxOriginal.SelectedText = "";
                    textBoxOriginal.Text = textBoxOriginal.Text.Insert(selectionStart, key.ToString());
                    textBoxOriginal.SelectionStart = selectionStart + 1;
                    break;

                case Keys.Back:
                case Keys.Delete:
                    eraseBtn_Click(this, null);
                    break;

                case Keys.OemPeriod:
                    pointBtn_Click(this, null);
                    break;

                case Keys.Control | Keys.A:
                    textBoxOriginal.SelectAll();
                    break;

                default:
                    break;
            }
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void historyMenuItem_Click(object sender, EventArgs e)
        {
            if (!History.isWindowOpened)
                new History().Show();
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
        }

        private string AppendNumberToLabel(string str, int number)
        {
            str = str.Remove(str.Length - (str[str.Length - 2] == '1' ? 2 : 1));
            return str + number.ToString();
        }

        private void originalNumberSystem_Changed(object sender, EventArgs e)
        {
            textBoxOriginal.Text = "";
            EnableNumberButtons();
            label2.Text = AppendNumberToLabel(label2.Text, (sender as TrackBar).Value);
        }

        private void resultlNumberSystem_Changed(object sender, EventArgs e)
        {
            textBoxResult.Text = "";
            label3.Text = AppendNumberToLabel(label3.Text, (sender as TrackBar).Value);
        }

        private void EnableNumberButtons()
        {
            int numberSystemValue = trackBarOriginal.Value;

            twoBtn.Enabled = numberSystemValue > 2;
            threeBtn.Enabled = numberSystemValue > 3;
            fourBtn.Enabled = numberSystemValue > 4;
            fiveBtn.Enabled = numberSystemValue > 5;
            sixBtn.Enabled = numberSystemValue > 6;
            sevenBtn.Enabled = numberSystemValue > 7;
            eightBtn.Enabled = numberSystemValue > 8;
            nineBtn.Enabled = numberSystemValue > 9;

            ABtn.Enabled = numberSystemValue > 10;
            BBtn.Enabled = numberSystemValue > 11;
            CBtn.Enabled = numberSystemValue > 12;
            DBtn.Enabled = numberSystemValue > 13;
            EBtn.Enabled = numberSystemValue > 14;
            FBtn.Enabled = numberSystemValue > 15;
        }

        private void textBoxOriginal_TextChanged(object sender, EventArgs e)
        {
            convertBtn.Enabled = (sender as TextBox).Text != "" && !History.isWindowOpened;
            eraseBtn.Enabled = convertBtn.Enabled;
            pointBtn.Enabled = !(sender as TextBox).Text.Contains('.');
        }

        private void charBtn_Click(object sender, EventArgs e)
        {
            var selectionStart = textBoxOriginal.SelectionStart;
            textBoxOriginal.SelectedText = "";
            textBoxOriginal.Text = textBoxOriginal.Text.Insert(selectionStart, (sender as Button).Text);
            textBoxOriginal.SelectionStart = selectionStart + 1;
        }

        private void pointBtn_Click(object sender, EventArgs e)
        {
            var selectionStart = textBoxOriginal.SelectionStart;
            if (textBoxOriginal.Text == "")
            {
                textBoxOriginal.Text += "0.";
                textBoxOriginal.SelectionStart = 2;
            }
            else if (!textBoxOriginal.Text.Contains(".") || textBoxOriginal.SelectedText.Contains("."))
            {
                textBoxOriginal.SelectedText = "";
                textBoxOriginal.Text = textBoxOriginal.Text.Insert(selectionStart, 
                    selectionStart == 0 ? "0." : ".");
                textBoxOriginal.SelectionStart = selectionStart == 0 ? 2 : selectionStart + 1;
            }
        }

        private void eraseBtn_Click(object sender, EventArgs e)
        {
            var selectionStart = textBoxOriginal.SelectionStart;
            if (textBoxOriginal.SelectedText == "" && textBoxOriginal.Text != "" && selectionStart != 0)
            {
                textBoxOriginal.Text = textBoxOriginal.Text.Remove(selectionStart - 1, 1);
                textBoxOriginal.SelectionStart = selectionStart - 1;
            }
            else
                textBoxOriginal.SelectedText = "";
        }

        private void convertBtn_Click(object sender, EventArgs e)
        {
            var result = Converter.Convert(
                textBoxOriginal.Text,
                trackBarOriginal.Value,
                trackBarResult.Value
            );

            textBoxResult.Text = result;

            History.AddConvertData(textBoxOriginal.Text, trackBarOriginal.Value,
                result, trackBarResult.Value);
        }

        private void Interface_Focus(object sender, EventArgs e)
        {
            convertBtn.Enabled = !History.isWindowOpened;
        }
    }
}