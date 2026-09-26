using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TV_Rename_Missing_XML_Parser.Forms;

namespace TV_Rename_Missing_XML_Parser.Controllers
{
    public class LogController
    {
        private readonly List<string> logs;

        public LogController()
        {
            this.logs = new List<string>();
        }

        public void Add(string message)
        {
            this.logs.Add(message);
        }

        public List<string> GetLogs()
        {
            return this.logs;
        }

        public bool HasLogs()
        {
            return this.logs.Count > 0;
        }

        public void ShowLogForm(Form parent)
        {
            LogForm logForm = new LogForm();
            logForm.add(this.logs);
            logForm.ShowDialog(parent);
        }
    }
}
