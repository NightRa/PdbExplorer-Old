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
            this.pdbTreeView = new System.Windows.Forms.TreeView();
            this.resultPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // loadPdbButton
            // 
            this.loadPdbButton.Location = new System.Drawing.Point(573, 16);
            this.loadPdbButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loadPdbButton.Name = "loadPdbButton";
            this.loadPdbButton.Size = new System.Drawing.Size(82, 22);
            this.loadPdbButton.TabIndex = 0;
            this.loadPdbButton.Text = "Load PDB";
            this.loadPdbButton.UseVisualStyleBackColor = true;
            this.loadPdbButton.Click += new System.EventHandler(this.loadPdbButton_Click);
            // 
            // pdbFilePath
            // 
            this.pdbFilePath.Location = new System.Drawing.Point(43, 18);
            this.pdbFilePath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pdbFilePath.Name = "pdbFilePath";
            this.pdbFilePath.Size = new System.Drawing.Size(526, 23);
            this.pdbFilePath.TabIndex = 1;
            // 
            // loadStatus
            // 
            this.loadStatus.AutoSize = true;
            this.loadStatus.Location = new System.Drawing.Point(573, 40);
            this.loadStatus.Name = "loadStatus";
            this.loadStatus.Size = new System.Drawing.Size(68, 15);
            this.loadStatus.TabIndex = 2;
            this.loadStatus.Text = "Load Status";
            // 
            // pdbTreeView
            // 
            this.pdbTreeView.Location = new System.Drawing.Point(13, 58);
            this.pdbTreeView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pdbTreeView.Name = "pdbTreeView";
            this.pdbTreeView.Size = new System.Drawing.Size(144, 272);
            this.pdbTreeView.TabIndex = 4;
            this.pdbTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // resultPanel
            // 
            this.resultPanel.Location = new System.Drawing.Point(178, 58);
            this.resultPanel.Name = "resultPanel";
            this.resultPanel.Size = new System.Drawing.Size(512, 272);
            this.resultPanel.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 338);
            this.Controls.Add(this.resultPanel);
            this.Controls.Add(this.pdbTreeView);
            this.Controls.Add(this.loadStatus);
            this.Controls.Add(this.pdbFilePath);
            this.Controls.Add(this.loadPdbButton);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button loadPdbButton;
        private TextBox pdbFilePath;
        private Label loadStatus;
        private TreeView pdbTreeView;
        private Panel resultPanel;
    }
}