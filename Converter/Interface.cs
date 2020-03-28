using System;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Converter
{
    public partial class Interface : Form
    {
        public static ConverterControl control = new ConverterControl();

        public Interface()
        {
            InitializeComponent();
            KeyDown += Interface_KeyDown;

            trackBarOriginal.Value = control.originalBase;
            trackBarResult.Value = control.resultBase;

            label2.Text += trackBarOriginal.Value.ToString();
            label3.Text += trackBarResult.Value.ToString();

            textBoxOriginal.Text = "0";
            textBoxResult.Text = "0";
            textBoxOriginal.SelectionStart = 1;

            EnableNumberButtons();
        }

        private void Interface_KeyDown(object sender, KeyEventArgs e)
        {
            control.editor.SetSelection(textBoxOriginal.SelectionStart, textBoxOriginal.SelectionLength);

            switch (e.KeyData)
            {
                case Keys key when (key >= Keys.D0 && key <= Keys.D9 && control.originalBase > e.KeyValue - 48):
                    textBoxOriginal.Text = control.EditNumber(ConverterControl.EditCommand.AddDigit, (e.KeyValue - 48).ToString());
                    break;

                case Keys key when (key >= Keys.NumPad0 && key <= Keys.NumPad9 && control.originalBase > e.KeyValue - 96):
                    textBoxOriginal.Text = control.EditNumber(ConverterControl.EditCommand.AddDigit, (e.KeyValue - 96).ToString());
                    break;

                case Keys key when (key >= Keys.A && key <= Keys.F && control.originalBase > e.KeyValue - 55):
                    textBoxOriginal.Text = control.EditNumber(ConverterControl.EditCommand.AddDigit, key.ToString());
                    break;

                case Keys.Back:
                case Keys.Delete:
                    textBoxOriginal.Text = control.EditNumber(ConverterControl.EditCommand.Erase);
                    break;

                case Keys.OemPeriod:
                case Keys.Decimal:
                    textBoxOriginal.Text = control.EditNumber(ConverterControl.EditCommand.AddDelim);
                    break;

                case Keys.Control | Keys.A:
                    textBoxOriginal.SelectAll();
                    control.editor.SetSelection(textBoxOriginal.SelectionStart, textBoxOriginal.SelectionLength);
                    return;
                default:
                    return;
            }

            textBoxOriginal.SelectionStart = control.editor.startIndex;
            textBoxOriginal.Focus();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void historyMenuItem_Click(object sender, EventArgs e)
        {
            new HistoryWindow().ShowDialog();
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            new Info().ShowDialog();
        }

        private void originalNumberSystem_Changed(object sender, EventArgs e)
        {
            control.originalBase = (sender as TrackBar).Value;
            textBoxOriginal.Text = control.EditNumber(ConverterControl.EditCommand.Clear);
            label2.Text = "Original number system: " +  control.originalBase;

            EnableNumberButtons();
            textBoxOriginal.Focus();
        }

        private void resultlNumberSystem_Changed(object sender, EventArgs e)
        {
            control.resultBase = (sender as TrackBar).Value;
            textBoxResult.Text = "0";
            label3.Text = "Result number system: " + control.resultBase;
            textBoxOriginal.Focus();
        }

        private void EnableNumberButtons()
        {
            var numberRegex = new Regex(@"[2-9]");
            var symbolRegex = new Regex(@"^[A-F]$");

            foreach (Button b in tableLayoutPanel1.Controls.OfType<Button>())
            {
                if (numberRegex.IsMatch(b.Text))
                    b.Enabled = control.originalBase > b.Text[0] - 48; 

                else if (symbolRegex.IsMatch(b.Text))
                    b.Enabled = control.originalBase > b.Text[0] - 55;
            }
        }

        private void textBoxOriginal_TextChanged(object sender, EventArgs e)
        {
            pointBtn.Enabled = !control.editor.number.Contains('.');
            convertBtn.Enabled = textBoxOriginal.Text != "";
            eraseBtn.Enabled = textBoxOriginal.Text != "";
            textBoxResult.Text = "0";
        }

        private void charBtn_Click(object sender, EventArgs e)
        {
            control.editor.SetSelection(textBoxOriginal.SelectionStart, textBoxOriginal.SelectionLength);
            textBoxOriginal.Text = control.EditNumber(ConverterControl.EditCommand.AddDigit, (sender as Button).Text);
            textBoxOriginal.SelectionStart = control.editor.startIndex;
            textBoxOriginal.Focus();
        }

        private void pointBtn_Click(object sender, EventArgs e)
        {
            control.editor.SetSelection(textBoxOriginal.SelectionStart, textBoxOriginal.SelectionLength);
            textBoxOriginal.Text = control.EditNumber(ConverterControl.EditCommand.AddDelim);
            textBoxOriginal.SelectionStart = control.editor.startIndex;
            textBoxOriginal.Focus();
        }

        private void eraseBtn_Click(object sender, EventArgs e)
        {
            control.editor.SetSelection(textBoxOriginal.SelectionStart, textBoxOriginal.SelectionLength);
            textBoxOriginal.Text = control.EditNumber(ConverterControl.EditCommand.Erase);
            textBoxOriginal.SelectionStart = control.editor.startIndex;
            textBoxOriginal.Focus();
        }

        private void convertBtn_Click(object sender, EventArgs e)
        {
            textBoxResult.Text = control.Convert();
            textBoxOriginal.Focus();
        }

    }
}