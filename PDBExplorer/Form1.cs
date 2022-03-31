using System.Text;
using RawPdbNet;

namespace PDBExplorer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        String byteToChar(Byte b)
        {
            if (Char.IsControl((char)b))
            {
                return $"\\x{b:X2}";
            }
            else
            {
                return ((char)b).ToString();
            }
        }

        private void loadPdbButton_Click(object sender, EventArgs e)
        {
            var file = File.ReadAllBytes(pdbFilePath.Text);
            ErrorCode res = Pdb.CheckFile(file);
            loadStatus.Text = res.ToString();

            var pdb = new Pdb(file);
            var superBlock = pdb.GetSuperBlock();
            var blockSize = superBlock.blockSize;

            var streamDirectoryStr = $"Stream Directory:\r\n" +
                                     $"\t(Stream Directory Block indices = [{String.Join(",", pdb.GetDirectoryStreamIndices())}])\r\n" +
                                     $"\tStream Count = {pdb.GetStreamCount()}\r\n" +
                                     $"\tStream Sizes = [{String.Join(",", pdb.GetStreamSizes())}]\r\n" +
                                     $"\tStream indices:\r\n";

            StringBuilder streamIndicesStr = new StringBuilder(streamDirectoryStr);
            var streamBlockIndices = pdb.GetStreamBlocksIndices();
            for (int i = 0; i < streamBlockIndices.Length; i++)
            {
                streamIndicesStr.Append($"\t{i}. [{String.Join(",", streamBlockIndices[i])}]\r\n");
            }

            resultTextArea.Text += streamIndicesStr.ToString();
            var superBlockNode = new TreeNode("SuperBlock")
            {
                Tag = $"SuperBlock header:\r\n" +
                      $"\tPDB Magic = '{String.Join("", superBlock.fileMagic.Select(byteToChar))}'\r\n" +
                      $"\tBlock Size = {superBlock.blockSize}\r\n" +
                      $"\tFree Block Map Index = {superBlock.freeBlockMapIndex}\r\n" +
                      $"\tBlock Count = {superBlock.blockCount}\r\n" +
                      $"\tStream Directory Size = {superBlock.directorySize} ({pdb.GetStreamDirectoryNumBlocks()} blocks)\r\n" +
                      $"\tUnknown field = {superBlock.unknown}\r\n" +
                      $"\tStream Directory Blocks indices Block indices = [{String.Join(",", superBlock.directoryStreamBlockIndices)}]\r\n"
            };

            var streamDirectoryNode = new TreeNode("Stream Directory")
            {
                Tag = streamIndicesStr.ToString()
            };

            pdbTreeView.BeginUpdate();
            pdbTreeView.Nodes.Clear();
            pdbTreeView.Nodes.Add(superBlockNode);
            pdbTreeView.Nodes.Add(streamDirectoryNode);
            pdbTreeView.SelectedNode = superBlockNode;
            pdbTreeView.EndUpdate();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            resultTextArea.Text = $"Selected {e.Node?.Tag}";
        }
    }
}