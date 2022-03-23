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
            resultTextArea.Text = $"SuperBlock header:\r\n" +
                                  $"\tPDB Magic = '{String.Join("", superBlock.fileMagic.Select(byteToChar))}'\r\n" +
                                  $"\tBlock Size = {superBlock.blockSize}\r\n" +
                                  $"\tFree Block Map Index = {superBlock.freeBlockMapIndex}\r\n" +
                                  $"\tBlock Count = {superBlock.blockCount}\r\n" +
                                  $"\tStream Directory Size = {superBlock.directorySize}\r\n" +
                                  $"\tUnknown field = {superBlock.unknown}\r\n" +
                                  $"\t(Stream Directory num blocks = {pdb.GetStreamDirectoryNumBlocks()})\r\n" +
                                  $"\tStream Directory Blocks indices Block indices = [{String.Join(",", superBlock.directoryStreamBlockIndices)}]\r\n" +
                                  $"\tStream Directory:\r\n" +
                                  $"\t\t(Stream Directory Block indices = [{String.Join(",", pdb.GetDirectoryStreamIndices())}])\r\n" +
                                  $"\t\tStream Count = {pdb.GetStreamCount()}\r\n" +
                                  $"\t\tStream Sizes = [{String.Join(",", pdb.GetStreamSizes())}]\r\n" +
                                  $"\t\tStream indices:\r\n";

            StringBuilder streamIndicesStr = new StringBuilder();
            var streamBlockIndices = pdb.GetStreamBlocksIndices();
            for (int i = 0; i < streamBlockIndices.Length; i++)
            {
                streamIndicesStr.Append($"\t\t\t{i}. [{String.Join(",", streamBlockIndices[i])}]\r\n");
            }

            resultTextArea.Text += streamIndicesStr.ToString();
        }
    }
}