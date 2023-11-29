using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TV_Rename_Missing_XML_Parser
{
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
        }

        public void add(List<string> logLines)
        {
            foreach (string logLine in logLines)
            {
                lvLog.Items.Add(logLine);
            }
        }
    }
}
