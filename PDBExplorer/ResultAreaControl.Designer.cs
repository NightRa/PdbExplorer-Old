namespace PDBExplorer
{
    partial class ResultAreaControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.resultTextArea = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // resultTextArea
            // 
            this.resultTextArea.Location = new System.Drawing.Point(0, 0);
            this.resultTextArea.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.resultTextArea.Multiline = true;
            this.resultTextArea.Name = "resultTextArea";
            this.resultTextArea.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resultTextArea.Size = new System.Drawing.Size(512, 272);
            this.resultTextArea.TabIndex = 5;
            // 
            // ResultAreaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.resultTextArea);
            this.Name = "ResultAreaControl";
            this.Size = new System.Drawing.Size(512, 272);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public TextBox resultTextArea;
    }
}
