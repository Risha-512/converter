using System;
using System.Linq;
using System.Windows.Forms;

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

                case Keys key when (key >= Keys.A && key <= Keys.F && control.originalBase > e.KeyValue - 55):
                    textBoxOriginal.Text = control.EditNumber(ConverterControl.EditCommand.AddDigit, key.ToString());
                    break;

                case Keys.Back:
                case Keys.Delete:
                    textBoxOriginal.Text = control.EditNumber(ConverterControl.EditCommand.Erase);
                    break;

                case Keys.OemPeriod:
                    var numLength = control.editor.number.Length;
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
            new Ref().ShowDialog();
        }

        private string AppendNumberToLabel(string str, int number)
        {
            str = str.Remove(str.Length - (str[str.Length - 2] == '1' ? 2 : 1));
            return str + number.ToString();
        }

        private void originalNumberSystem_Changed(object sender, EventArgs e)
        {
            control.originalBase = (sender as TrackBar).Value;
            textBoxOriginal.Text = control.EditNumber(ConverterControl.EditCommand.Clear);
            label2.Text = AppendNumberToLabel(label2.Text, control.originalBase);
            
            EnableNumberButtons();
        }

        private void resultlNumberSystem_Changed(object sender, EventArgs e)
        {
            control.resultBase = (sender as TrackBar).Value;
            textBoxResult.Text = "0";
            label3.Text = AppendNumberToLabel(label3.Text, control.resultBase);
        }

        private void EnableNumberButtons()
        {
            int numberSystemValue = control.originalBase;

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
            convertBtn.Enabled = control.editor.number != "";
            eraseBtn.Enabled = convertBtn.Enabled;
            pointBtn.Enabled = !control.editor.number.Contains('.');

            textBoxResult.Text = "0";
        }

        private void charBtn_Click(object sender, EventArgs e)
        {
            control.editor.SetSelection(textBoxOriginal.SelectionStart, textBoxOriginal.SelectionLength);
            textBoxOriginal.Text = control.EditNumber(ConverterControl.EditCommand.AddDigit, (sender as Button).Text);
            textBoxOriginal.SelectionStart = control.editor.startIndex;
        }

        private void pointBtn_Click(object sender, EventArgs e)
        {
            control.editor.SetSelection(textBoxOriginal.SelectionStart, textBoxOriginal.SelectionLength);
            textBoxOriginal.Text = control.EditNumber(ConverterControl.EditCommand.AddDelim);
            textBoxOriginal.SelectionStart = control.editor.startIndex;
        }

        private void eraseBtn_Click(object sender, EventArgs e)
        {
            control.editor.SetSelection(textBoxOriginal.SelectionStart, textBoxOriginal.SelectionLength);
            textBoxOriginal.Text = control.EditNumber(ConverterControl.EditCommand.Erase);
            textBoxOriginal.SelectionStart = control.editor.startIndex;
        }

        private void convertBtn_Click(object sender, EventArgs e)
        {
            textBoxResult.Text = control.Convert();
        }

    }
}