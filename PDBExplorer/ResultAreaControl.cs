using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDBExplorer
{
    public partial class ResultAreaControl : UserControl
    {
        public ResultAreaControl()
        {
            InitializeComponent();
        }

        public ResultAreaControl(String content): this()
        {
            resultTextArea.Text = content;
        }
    }
}
