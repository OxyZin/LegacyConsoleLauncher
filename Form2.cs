using System;
using System.Windows.Forms;

namespace LegacyConsoleLauncher
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public void SetStatus(string text)
        {
            label1.Text = text;
            label1.Refresh();
        }

        public void SetProgress(int value)
        {
            if (value < 0) value = 0;
            if (value > 100) value = 100;

            progressBar1.Value = value;
            progressBar1.Refresh();
        }
    }
}