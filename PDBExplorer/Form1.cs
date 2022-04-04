using System.Runtime.InteropServices;
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

            // Super Block
            var superBlockNode = new TreeNode("SuperBlock")
            {
                Tag = () => new ResultAreaControl(
                      $"SuperBlock header:\r\n" +
                      $"  PDB Magic = '{String.Join("", superBlock.fileMagic.Select(byteToChar))}'\r\n" +
                      $"  Block Size = {superBlock.blockSize}\r\n" +
                      $"  Free Block Map Index = {superBlock.freeBlockMapIndex}\r\n" +
                      $"  Block Count = {superBlock.blockCount}\r\n" +
                      $"  Stream Directory Size = {superBlock.directorySize} ({pdb.GetStreamDirectoryNumBlocks()} blocks)\r\n" +
                      $"  Unknown field = {superBlock.unknown}\r\n" +
                      $"  Stream Directory Blocks indices Block indices = [{String.Join(", ", superBlock.directoryStreamBlockIndices)}]\r\n")
            };

            var streamBlockIndices = pdb.GetStreamBlocksIndices();
            var streamSizes = pdb.GetStreamSizes();

            // Stream Directory
            var streamDirectoryNode = new TreeNode("Stream Directory")
            {
                Tag = () => new ResultAreaControl(
                         $"Stream Directory:\r\n" +
                         $"  (Stream Directory Block indices = [{String.Join(",", pdb.GetDirectoryStreamIndices())}])\r\n" +
                         $"  Stream Count = {pdb.GetStreamCount()}\r\n" +
                         $"  Stream Sizes[{streamSizes.Length}]\r\n" +
                         $"  Stream Blocks[{streamBlockIndices.Length}][#Stream i Blocks]")
            };

            // Individual Streams

            var streamNodes = new TreeNode[streamBlockIndices.Length];
            for (UInt32 i = 0; i < streamNodes.Length; i++)
            {
                var streamIndex = i;
                var streamNode = new TreeNode($"Stream {i}");
                streamNodes[i] = streamNode;
                streamNode.Tag = () => new StreamControl(pdb.GetStream(streamIndex));
            }

            var pdbInfoHeader = pdb.GetPdbInfoStream();
            var pdbStreamNode = new TreeNode("PDB Info Header");
            pdbStreamNode.Tag = () => new ResultAreaControl($"PDB Info Header:\r\n" +
                                                            $"  Version = {pdbInfoHeader.version} = {(UInt32)pdbInfoHeader.version}\r\n" +
                                                            $"  Signature = Timestamp = {pdbInfoHeader.signature} = {new DateTime(1970, 1, 1).AddSeconds(pdbInfoHeader.signature)}\r\n" +
                                                            $"  PDB Age = {pdbInfoHeader.age}\r\n" +
                                                            $"  PDB Guid = {(Guid)pdbInfoHeader.guid}");
            streamNodes[1].Nodes.Add(pdbStreamNode);

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
            var contentFunc = (Func<Control>?)e.Node?.Tag;
            Control resultArea = contentFunc == null ? new ResultAreaControl("Error: No content for this item!") : contentFunc();

            resultPanel.SuspendDrawing();

            resultPanel.Controls.Clear();
            resultPanel.Controls.Add(resultArea);

            resultPanel.ResumeDrawing();
        }
    }

    public static class DrawingControl
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

        private const int WM_SETREDRAW = 11;

        public static void SuspendDrawing(this Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, false, 0);
        }

        public static void ResumeDrawing(this Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, true, 0);
            parent.Refresh();
        }
    }
}