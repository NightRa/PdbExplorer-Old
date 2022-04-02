using System.Text;
using RawPdbNet;

namespace PDBExplorer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pdbFilePath.Text = Path.GetFullPath(Path.Combine(
                Directory.GetCurrentDirectory(), "../../../../bin/x64/Debug/Examples.pdb"));
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

            // Super Block

            var superBlockNode = new TreeNode("SuperBlock")
            {
                Tag = $"SuperBlock header:\r\n" +
                      $"  PDB Magic = '{String.Join("", superBlock.fileMagic.Select(byteToChar))}'\r\n" +
                      $"  Block Size = {superBlock.blockSize}\r\n" +
                      $"  Free Block Map Index = {superBlock.freeBlockMapIndex}\r\n" +
                      $"  Block Count = {superBlock.blockCount}\r\n" +
                      $"  Stream Directory Size = {superBlock.directorySize} ({pdb.GetStreamDirectoryNumBlocks()} blocks)\r\n" +
                      $"  Unknown field = {superBlock.unknown}\r\n" +
                      $"  Stream Directory Blocks indices Block indices = [{String.Join(", ", superBlock.directoryStreamBlockIndices)}]\r\n"
            };

            // Stream Directory

            var streamDirectoryStr = $"Stream Directory:\r\n" +
                         $"  (Stream Directory Block indices = [{String.Join(",", pdb.GetDirectoryStreamIndices())}])\r\n" +
                         $"  Stream Count = {pdb.GetStreamCount()}\r\n" +
                         $"  Stream Sizes = [{String.Join(", ", pdb.GetStreamSizes())}]\r\n" +
                         $"  Stream indices:\r\n";

            StringBuilder streamIndicesStr = new StringBuilder(streamDirectoryStr);
            var streamBlockIndices = pdb.GetStreamBlocksIndices();
            for (int i = 0; i < streamBlockIndices.Length; i++)
            {
                streamIndicesStr.Append($"  {i}. [{String.Join(",", streamBlockIndices[i])}]\r\n");
            }

            var streamDirectoryNode = new TreeNode("Stream Directory")
            {
                Tag = streamIndicesStr.ToString()
            };

            // Individual Streams

            var streamNodes = new TreeNode[streamBlockIndices.Length];
            for (int i = 0; i < streamNodes.Length; i++)
            {
                var streamNode = new TreeNode($"Stream {i}");
                streamNodes[i] = streamNode;
                streamNode.Tag = 
                    $"Stream {i}\r\n" +
                    $"  Stream size = {pdb.GetStreamSizes()[i]}\r\n" + 
                    $"  Stream blocks = [{String.Join(", ", streamBlockIndices[i])}]";
            }

            pdbTreeView.BeginUpdate();
            pdbTreeView.Nodes.Clear();
            pdbTreeView.Nodes.Add(superBlockNode);
            pdbTreeView.Nodes.Add(streamDirectoryNode);
            streamDirectoryNode.Nodes.AddRange(streamNodes);
            pdbTreeView.SelectedNode = superBlockNode;
            pdbTreeView.EndUpdate();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            resultTextArea.Text = e.Node?.Tag.ToString();
        }
    }
}