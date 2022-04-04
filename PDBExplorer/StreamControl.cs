using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using RawPdbNet;

namespace PDBExplorer
{
    public partial class StreamControl : UserControl
    {
        private PdbStream _pdbStream;

        // Only for editor
        public StreamControl()
        {
            InitializeComponent();
        }

        public StreamControl(PdbStream stream): this()
        {
            _pdbStream = stream;

            resultTextArea.Text = $"Stream {_pdbStream.GetStreamIndex()}\r\n" +
                    $"  Stream size = {_pdbStream.GetStreamSize()}\r\n" +
                    $"  Stream blocks = [{String.Join(", ", _pdbStream.GetStreamBlockIndices())}]";
        }

        private void saveStreamButton_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = $"Stream{_pdbStream.GetStreamIndex()}.bin";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(dialog.FileName, _pdbStream.GetData());
            }
        }

        private void openStreamButton_Click(object sender, EventArgs e)
        {
            var tempFilePath = Path.GetTempFileName();
            File.WriteAllBytes(tempFilePath, _pdbStream.GetData());
            Process.Start("010Editor.exe", tempFilePath);
        }
    }
}
