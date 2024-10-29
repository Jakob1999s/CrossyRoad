namespace CrossyRoad
{
    partial class FrmCrossyRoad
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCrossyRoad));
            tmrCrossyRoad = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // tmrCrossyRoad
            // 
            tmrCrossyRoad.Interval = 20;
            tmrCrossyRoad.Tick += tmrCrossyRoad_Tick;
            // 
            // FrmCrossyRoad
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1581, 891);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmCrossyRoad";
            Text = "Crossy Road";
            Load += FrmCrossyRoad_Load;
            SizeChanged += FrmCrossyRoad_SizeChanged;
            Paint += FrmCrossyRoad_Paint;
            KeyDown += FrmCrossyRoad_KeyDown;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer tmrCrossyRoad;
    }
}
