using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

namespace Converter
{
    public partial class HistoryWindow : Form
    {
        public HistoryWindow()
        {
            InitializeComponent();
            historyList.DataSource = Interface.control.history.historyList;
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            Close();
            Interface.control.history.Clear();
        }
    }
}
