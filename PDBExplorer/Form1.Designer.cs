namespace PDBExplorer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.loadPdbButton = new System.Windows.Forms.Button();
            this.pdbFilePath = new System.Windows.Forms.TextBox();
            this.loadStatus = new System.Windows.Forms.Label();
            this.resultTextArea = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // loadPdbButton
            // 
            this.loadPdbButton.Location = new System.Drawing.Point(655, 22);
            this.loadPdbButton.Name = "loadPdbButton";
            this.loadPdbButton.Size = new System.Drawing.Size(94, 29);
            this.loadPdbButton.TabIndex = 0;
            this.loadPdbButton.Text = "Load PDB";
            this.loadPdbButton.UseVisualStyleBackColor = true;
            this.loadPdbButton.Click += new System.EventHandler(this.loadPdbButton_Click);
            // 
            // pdbFilePath
            // 
            this.pdbFilePath.Location = new System.Drawing.Point(49, 24);
            this.pdbFilePath.Name = "pdbFilePath";
            this.pdbFilePath.Size = new System.Drawing.Size(600, 27);
            this.pdbFilePath.TabIndex = 1;
            this.pdbFilePath.Text = "C:\\Users\\Ilan\\Programming\\OpenSource\\raw_pdb\\bin\\x64\\Debug\\Examples.pdb";
            // 
            // loadStatus
            // 
            this.loadStatus.AutoSize = true;
            this.loadStatus.Location = new System.Drawing.Point(655, 54);
            this.loadStatus.Name = "loadStatus";
            this.loadStatus.Size = new System.Drawing.Size(86, 20);
            this.loadStatus.TabIndex = 2;
            this.loadStatus.Text = "Load Status";
            // 
            // resultTextArea
            // 
            this.resultTextArea.Location = new System.Drawing.Point(49, 77);
            this.resultTextArea.Multiline = true;
            this.resultTextArea.Name = "resultTextArea";
            this.resultTextArea.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resultTextArea.Size = new System.Drawing.Size(739, 361);
            this.resultTextArea.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.resultTextArea);
            this.Controls.Add(this.loadStatus);
            this.Controls.Add(this.pdbFilePath);
            this.Controls.Add(this.loadPdbButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button loadPdbButton;
        private TextBox pdbFilePath;
        private Label loadStatus;
        private TextBox resultTextArea;
    }
}