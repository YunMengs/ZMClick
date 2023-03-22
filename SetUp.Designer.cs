namespace ZMClick
{
    partial class SetUp
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBoxStartUp = new System.Windows.Forms.CheckBox();
            this.checkBoxMinimize = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBoxStartUp
            // 
            this.checkBoxStartUp.AutoSize = true;
            this.checkBoxStartUp.Location = new System.Drawing.Point(13, 13);
            this.checkBoxStartUp.Name = "checkBoxStartUp";
            this.checkBoxStartUp.Size = new System.Drawing.Size(89, 19);
            this.checkBoxStartUp.TabIndex = 0;
            this.checkBoxStartUp.Text = "开机启动";
            this.checkBoxStartUp.UseVisualStyleBackColor = true;
            this.checkBoxStartUp.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBoxMinimize
            // 
            this.checkBoxMinimize.AutoSize = true;
            this.checkBoxMinimize.Location = new System.Drawing.Point(12, 50);
            this.checkBoxMinimize.Name = "checkBoxMinimize";
            this.checkBoxMinimize.Size = new System.Drawing.Size(119, 19);
            this.checkBoxMinimize.TabIndex = 1;
            this.checkBoxMinimize.Text = "启动后最小化";
            this.checkBoxMinimize.UseVisualStyleBackColor = true;
            this.checkBoxMinimize.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // SetUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.checkBoxMinimize);
            this.Controls.Add(this.checkBoxStartUp);
            this.Name = "SetUp";
            this.Text = "SetUp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxStartUp;
        private System.Windows.Forms.CheckBox checkBoxMinimize;
    }
}