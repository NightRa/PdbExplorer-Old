
using PDB;
using RawPdbNet;

namespace PDBExplorer
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var x = new TestPdb();
            var file = File.ReadAllBytes(
                "C:\\Users\\Ilan\\Programming\\OpenSource\\raw_pdb\\bin\\x64\\Debug\\Examples.pdb");
            var res = x.CheckFile(file);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
            
        }
    }
}